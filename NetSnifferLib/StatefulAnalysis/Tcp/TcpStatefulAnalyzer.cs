using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using NetSnifferLib.Analysis;
using NetSnifferLib.Analysis.Network;
using NetSnifferLib.StatefulAnalysis;
using NetSnifferLib.General;
using NetSnifferLib.Topology;

using PcapDotNet.Packets.Transport;

namespace NetSnifferLib.StatefulAnalysis.Tcp
{
    class TcpStatefulAnalyzer
    {
        public static TcpStatefulAnalyzer Analyzer { get; set; } = new();

        readonly List<TcpConnection> allConnections = new();

        readonly Dictionary<TcpConnection, List<int>> connectionsPacketIds = new();

        //bool isAnalyzig = false;

        //Timer synFloodTimer;

        int synTimeout = 1000;

        int synFloodInterval = 2000;

        int synFloodSynCount = 10;

        int timeBetweenSeperateSynFloods = 10000;

        Dictionary<Attack, List<TcpConnection>> synFloods = new();
        Dictionary<TcpConnection, Attack> synFloodsByConncetion = new();

        bool isDetectionRunning = false;

        void DetectSynFlood()
        {
            isDetectionRunning = true;

            IEnumerable<TcpConnection> notInAttack;
            
            lock (allConnections)
                notInAttack = allConnections.Where(connection => !synFloodsByConncetion.ContainsKey(connection));

            var onlySynConnections = notInAttack.Where(connection => connection.Status == TcpConnectionStatus.Syn);
            var timedOut = onlySynConnections.Where(connection => (IdManager.GetPacketTimestamp(connectionsPacketIds[connection][0]) - DateTime.Now).Milliseconds > synTimeout);

            var onlySynConnectionsByHost = onlySynConnections.GroupBy(connection => connection.ListenerEndPoint.Address);


            foreach (var group in onlySynConnectionsByHost)     
            {
                //List<IGrouping<IPAddress, TcpConnection>> groups = new();

                if (group.Count() > synFloodSynCount)
                {
                    DateTime firstPacketTime = group.Min(connection => IdManager.GetPacketTimestamp(connectionsPacketIds[connection][0]));
                    DateTime lastPacketTime = group.Max(connection => IdManager.GetPacketTimestamp(connectionsPacketIds[connection][0]));

                    if ((lastPacketTime - firstPacketTime).Milliseconds < timeBetweenSeperateSynFloods)
                    {
                        var synFlood = new Attack(
                             name: "Syn Flood",
                             packetIds: group.Select(connection => connectionsPacketIds[connection][0]).ToArray(),
                             attackers: group.Select(connection => new IpAddressContainer(connection.ConnectorEndPoint.Address)).ToArray(),
                             targets: new[] { new IpAddressContainer(group.Key) });

                        synFloods.Add(synFlood, group.ToList());

                        foreach (var connection in group)
                        {
                            synFloodsByConncetion.Add(connection, synFlood);
                            var packetData = PacketData.GetPacketDataByPacketId(connectionsPacketIds[connection][0]);
                            packetData.AddAttack(synFlood);
                        }

                        return;
                    }

                    var connectionGroupsByTime = group.GroupBy(connection => (IdManager.GetPacketTimestamp(connectionsPacketIds[connection][0]) - firstPacketTime).TotalMilliseconds / timeBetweenSeperateSynFloods);
                        
                    foreach (var timeGroup in connectionGroupsByTime)
                    {
                        if (timeGroup.Count() > synFloodSynCount)
                        {
                            var synFlood = new Attack(
                                name: "Syn Flood",
                                packetIds: timeGroup.Select(connection => connectionsPacketIds[connection][0]).ToArray(),
                                attackers: timeGroup.Select(connection => new IpAddressContainer(connection.ConnectorEndPoint.Address)).ToArray(),
                                targets: new[] { new IpAddressContainer(group.Key) });

                            synFloods.Add(synFlood, timeGroup.ToList());

                            foreach (var connection in timeGroup)
                            {
                                synFloodsByConncetion.Add(connection, synFlood);
                                var packetData = PacketData.GetPacketDataByPacketId(connectionsPacketIds[connection][0]);
                                packetData.AddAttack(synFlood);
                            }
                        }
                    }
                }
            }

            isDetectionRunning = false;
        }

        int n = 0;
        public void AnalyzeDatagram(TcpDatagram datagram, NetworkContext context, int packetId)
        {
            n++;
            if (n == 100)
            {
                n = 0;
                
                if (!isDetectionRunning)
                    Task.Run(() => DetectSynFlood());
            }
/*            if (!isAnalyzig)
            {
                isAnalyzig = true;
                synFloodTimer = new Timer(new TimerCallback(obj => DetectSynFlood()), null, 0, 2000);
            }*/

            var sourceAddress = context.Source.IPAddress;
            var sourcePort = datagram.SourcePort;

            var destinationAddress = context.Destination.IPAddress;
            var destinationPort = datagram.DestinationPort;

            var flags = datagram.ControlBits;
            
            var payloadLength = (uint)datagram.PayloadLength;

            var sequenceNumber = datagram.SequenceNumber;
            var acknowledgementNumber = datagram.AcknowledgmentNumber;

            var sourceEndPoint = new IPEndPoint(sourceAddress, sourcePort);
            var destinationEndPoint = new IPEndPoint(destinationAddress, destinationPort);

            var connection = GetConnectionByIPEndPoints(sourceEndPoint, destinationEndPoint);
            // sender is connector
            if (connection != null)
            {
                connection.AnalyzeConnectorPacket(flags, sequenceNumber, acknowledgementNumber, payloadLength);

                if (connection.Status == TcpConnectionStatus.Closed)
                    lock (allConnections)
                        allConnections.Remove(connection);

                return;
            }

            connection = GetConnectionByIPEndPoints(destinationEndPoint, sourceEndPoint);
            // sender is listener
            if (connection != null)
            {
                connection.AnalyzeListenerPacket(flags, sequenceNumber, acknowledgementNumber, payloadLength);

                if (connection.Status == TcpConnectionStatus.Closed)
                {
                    lock (allConnections) 
                        allConnections.Remove(connection);
                    //connectionsPacketIds.Remove(connection);
                }

                return;
            }
            // the connection does not exist yet
            else
            {
                connection = TcpConnection.CreateConnection(sourceEndPoint, destinationEndPoint, flags, sequenceNumber, acknowledgementNumber, payloadLength);


                List<WanHost> hosts;
                WanHost sourceHost, destinationHost;

                do
                {
                    hosts = PacketAnalyzer.Analyzer.GetWanHosts();
                    destinationHost = hosts.Find(host => host.IPAddress.Equals(destinationAddress));
                    sourceHost = hosts.Find(host => host.IPAddress.Equals(sourceAddress));
                } while (destinationHost == null || sourceHost == null);

                sourceHost.TcpConnections.Add(connection);
                destinationHost.TcpConnections.Add(connection);

                lock (allConnections)
                    allConnections.Add(connection);

                lock (connectionsPacketIds)
                    connectionsPacketIds.Add(connection, new List<int>());
            }

            connectionsPacketIds[connection].Add(packetId);
        }

        TcpConnection GetConnectionByIPEndPoints(IPEndPoint connector, IPEndPoint listener)
        {
            lock (allConnections)
                return allConnections.FirstOrDefault(
                    connection => 
                    connection.ListenerEndPoint.Equals(listener) && 
                    connection.ConnectorEndPoint.Equals(connector));
        }
    }
}
