using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetSnifferLib.Topology;
using NetSnifferLib.Analysis;
using NetSnifferLib.General;
using NetSnifferLib.Analysis.Network;

using PcapDotNet.Packets.Transport;

namespace NetSnifferLib.StatefulAnalysis.Tcp
{
    static class TcpStatefulAnalyzer
    {
        static readonly List<TcpConnection> allConnections = new();

        public static void AnalyzeDatagram(TcpDatagram datagram, NetworkContext context)
        {
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
                    allConnections.Remove(connection);

                return;
            }

            connection = GetConnectionByIPEndPoints(destinationEndPoint, sourceEndPoint);
            // sender is listener
            if (connection != null)
            {
                connection.AnalyzeListenerPacket(flags, sequenceNumber, acknowledgementNumber, payloadLength);

                if (connection.Status == TcpConnectionStatus.Closed)
                    allConnections.Remove(connection);

                return;
            }
            // the connection does not exist yet
            else
            {
                connection = TcpConnection.CreateConnection(sourceEndPoint, destinationEndPoint, flags, sequenceNumber, acknowledgementNumber, payloadLength);

                var hosts = PacketAnalyzer.Analyzer.GetWanHosts();
                
                var sourceHost = hosts.Find(host => host.IPAddress.Equals(sourceAddress));
                var destinationHost = hosts.Find(host => host.IPAddress.Equals(destinationAddress));

                sourceHost.TcpConnections.Add(connection);
                destinationHost.TcpConnections.Add(connection);

                lock (allConnections)
                    allConnections.Add(connection);
            }
        }

        static TcpConnection GetConnectionByIPEndPoints(IPEndPoint connector, IPEndPoint listener)
        {
            lock (allConnections)
                return allConnections.FirstOrDefault(
                    connection => 
                    connection.ListenerEndPoint.Equals(listener) && 
                    connection.ConnectorEndPoint.Equals(connector));
        }
    }
}
