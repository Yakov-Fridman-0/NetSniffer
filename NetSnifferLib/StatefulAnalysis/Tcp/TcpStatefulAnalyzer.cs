using System;
using System.Collections.Generic;
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
        public static void ReportDatagram(TcpDatagram datagram, NetworkContext context)
        {
            var sourceAddress = context.Source.IPAddress;
            var sourcePort = datagram.SourcePort;

            var destinationAddress = context.Destination.IPAddress;
            var destinationPort = datagram.DestinationPort;

            var flags = datagram.ControlBits;
            
            var payloadLength = (uint)datagram.PayloadLength;

            var hosts = PacketAnalyzer.Analyzer.GetOriginalWanHosts();

            WanHost sender, receiver;

            lock (hosts)
                sender = hosts.Find(host => host.IPAddress.Equals(sourceAddress));
                receiver = hosts.Find(host => host.IPAddress.Equals(destinationAddress));

            var sequenceNumber = datagram.SequenceNumber;
            var acknowledgementNumber = datagram.AcknowledgmentNumber;

            sender.ConnectionManager.RegisterSentData(sourcePort, destinationAddress, destinationPort, flags, sequenceNumber, acknowledgementNumber, payloadLength);
            receiver.ConnectionManager.RegisterReceivedData(sourceAddress, sourcePort, destinationPort, flags, sequenceNumber, acknowledgementNumber, payloadLength);
        }
    }
}
