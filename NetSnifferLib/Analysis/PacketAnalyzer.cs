using System;

using PcapDotNet.Packets;

using NetSnifferLib.General;
using NetSnifferLib.Topology;
using NetSnifferLib.Statistics;

namespace NetSnifferLib.Analysis
{
    public class PacketAnalyzer
    {
        static PacketAnalyzer()
        {
            DatagramAnalyzer.EthernetAnalyzer.HostDetected += EthernetAnalyzer_PysicalAddressDetected;
            DatagramAnalyzer.ArpAnalyzer.PayloadIndicatesHost += ArpAnalyzer_PayloadIndicatesHost;
        }

        private static void ArpAnalyzer_PayloadIndicatesHost(object sender, IPandPhysicalAddress e)
        {
            var iPandPhysicalAddress = e;
            LanMapBuilder.AddHostWithIP(iPandPhysicalAddress.PhysicalAddress, iPandPhysicalAddress.IPAddress);
        }

        private static void EthernetAnalyzer_PysicalAddressDetected(object sender, System.Net.NetworkInformation.PhysicalAddress e)
        {
            var physicalAddress = e;
            LanMapBuilder.AddHostWithoutIP(physicalAddress);
        }

        public static LanMapBuilder LanMapBuilder {get;} = new();

        public static WanMap WanMapBuilder { get; } = new();

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
