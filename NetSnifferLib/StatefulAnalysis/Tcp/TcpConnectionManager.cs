using System;
using System.Net;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PcapDotNet.Packets.Transport;

namespace NetSnifferLib.StatefulAnalysis.Tcp
{
    public class TcpConnectionManager
    {
        IPAddress IPAddress { get; init; }

        public Dictionary<int, TcpConnection> connections = new();

        public TcpConnectionManager(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
        }

        public void RegisterSentData(
            int sourcePort, 
            IPAddress destinationAddress, 
            int destinationPort, 
            TcpControlBits flags,
            uint sequenceNumber,
            uint acknowledgementNumber,
            uint payloadLength)
        {
            if (connections.TryGetValue(sourcePort, out TcpConnection connection))
            {
                //connection.ReportSentPacket(flags, sequenceNumber, acknowledgementNumber, payloadLength);

                if (connection.Status == TcpConnectionStatus.Closed)
                    connections.Remove(sourcePort, out _);
            }
            else
            {
                var newConnection = new TcpConnection(new IPEndPoint(IPAddress, sourcePort), new IPEndPoint(destinationAddress, destinationPort));
                //newConnection.ReportSentPacket(flags, sequenceNumber, acknowledgementNumber, payloadLength);

                connections.TryAdd(sourcePort, newConnection);
            }
        }

        public void RegisterReceivedData(
            IPAddress sourceAddress, 
            int sourcePort, 
            int destinationPort,
            TcpControlBits flags,
            uint sequenceNumber,
            uint acknowledgementNumber,
            uint payloadLength)
        {
            if (connections.TryGetValue(destinationPort, out TcpConnection connection))
            {
                lock (connections)
                {
                    connection.ReportReceivedPacket(flags, sequenceNumber, acknowledgementNumber, payloadLength);
                    if (connection.Status == TcpConnectionStatus.Closed)
                        connections.Remove(sourcePort, out _);
                }
            }
            else
            {
                lock (connections)
                {
                    var newConnection = new TcpConnection(new IPEndPoint(sourceAddress, sourcePort), new IPEndPoint(IPAddress, destinationPort));
                    //newConnection.ReportSentPacket(flags, sequenceNumber, acknowledgementNumber, payloadLength);

                    connections.TryAdd(destinationPort, newConnection);
                }

            }
        }
    }
}
