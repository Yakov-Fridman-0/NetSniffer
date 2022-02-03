using System;
using System.Net;

using PcapDotNet.Packets;

using NetSnifferLib.General;
using NetSnifferLib.Topology;
using NetSnifferLib.Statistics;

namespace NetSnifferLib.Analysis
{
    public static class PacketAnalyzer
    {
        static readonly TopologyBuilder topologyBuilder = new();

        static readonly AnalyzerEventHandler analyzerEventHandler = new();

        class AnalyzerEventHandler
        {
            public void EthernetAnalyzer_PacketCaptured(object sender, DataLinkPacketEventArgs e)
            {
                var source = e.Source;
                var destination = e.Destination;

                topologyBuilder.AddHost(source);
                topologyBuilder.AddHost(destination);
            }

            public void IpV4Analyzer_PacketCaptured(object sender, NetworkPacketEventArgs e)
            {
                var sourceIPAddress = e.SourceIPAddress;
                var sourcePhysicalAddress = e.SourcePhysicalAddress;

                var destinationIPAddress = e.DestinationIPAddress;
                var destinationPhysicalAddress = e.DestinationPhysicalAddress;

                topologyBuilder.AddHost(sourcePhysicalAddress, sourceIPAddress);
                topologyBuilder.AddHost(destinationPhysicalAddress, destinationIPAddress);
            }

            public void ArpAnalyzer_PayloadIndicatesHost(object sender, PayloadIndicatesHostEventArgs e)
            {
                var ipAddress = e.IPAddress;
                var physicalAddress = e.PhysicalAddress;
                topologyBuilder.AddHostInLan(physicalAddress, ipAddress);
            }

            public void DhcpAnalyzer_ServerDetected(object sender, IPAddress e)
            {
                topologyBuilder.AddDhcpServer(e);
            }

            public void DnsAnalyzer_ServerDetected(object sender, IPAddress e)
            {
                topologyBuilder.AddDnsServer(e);
            }
        }

        static PacketAnalyzer()
        {
            DatagramAnalyzer.EthernetAnalyzer.PacketCaptured += analyzerEventHandler.EthernetAnalyzer_PacketCaptured;
            DatagramAnalyzer.IpV4Analyzer.PacketCaptured += analyzerEventHandler.IpV4Analyzer_PacketCaptured;

            DatagramAnalyzer.DhcpAnalyzer.ServerDetected += analyzerEventHandler.DhcpAnalyzer_ServerDetected;
            DatagramAnalyzer.DnsAnalyzer.ServerDetected += analyzerEventHandler.DnsAnalyzer_ServerDetected;

            DatagramAnalyzer.ArpAnalyzer.PayloadIndicatesHost += analyzerEventHandler.ArpAnalyzer_PayloadIndicatesHost;
        }



        public static int TransmittedPackets { get; private set; } = 0;

        public static int TransmittedBytes { get; private set; } = 0;

        public static bool IsEthernet(Packet packet) => packet.DataLink.Kind == DataLinkKind.Ethernet;

        public static PacketDescription AnalyzePacket(Packet packet)
        {
            TransmittedPackets++;
            TransmittedBytes += packet.Length;

            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");


            var timeStamp = packet.Timestamp;
            var length = packet.Length;

            string protocol, info;

            IAddress source = null, destination = null;

            IAnalysis analysis;

            Datagram datagram = packet.Ethernet;
            IContext context = null;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;


            do
            {
                analysis = analyzer.AnalyzeDatagram(datagram, context);

                info = analysis.Info;
                protocol = analyzer.Protocol;

                if (analysis.HasAddresses)
                {
                    source = analysis.Source;
                    destination = analysis.Destination;
                }

                if (analysis.HasPayload)
                {
                    datagram = analysis.Payload;
                    context = analysis.PayloadContext;
                    analyzer = analysis.PayloadAnalyzer;
                }

            } while (analysis.HasPayload && analyzer != null);


            return new PacketDescription() 
            { 
                TimeStamp = timeStamp,
                Protocol = protocol,
                Source = source,
                Destination = destination,
                Length = length,
                Info = info
            };
        }

        public static GeneralStatistics GetGeneralStatistics()
        {
            return new GeneralStatistics()
            {
                TransmittedPackets = TransmittedPackets,
                TransmittedBytes = TransmittedBytes,

                EthernetPackets = DatagramAnalyzer.EthernetAnalyzer.SentPackets,
                EthernetPayloadBytes = DatagramAnalyzer.EthernetAnalyzer.SentBytes,

                IpV4Packets = DatagramAnalyzer.IpV4Analyzer.SentPackets,
                IpV4PayloadBytes = DatagramAnalyzer.IpV4Analyzer.SentBytes,

                IpV6Packets = DatagramAnalyzer.IpV6Analyzer.SentPackets,
                IpV6PayloadBytes = DatagramAnalyzer.IpV6Analyzer.SentBytes,

                UdpPackets = DatagramAnalyzer.UdpAnalyzer.SentPackets,
                UdpPayloadBytes = DatagramAnalyzer.UdpAnalyzer.SentBytes,

                TcpPackets = DatagramAnalyzer.TcpAnalyzer.SentPackets,
                TcpPayloadBytes = DatagramAnalyzer.TcpAnalyzer.SentBytes,
            };
        }
    }
}
