using System;

using PcapDotNet.Packets;

using NetSnifferLib.General;
using NetSnifferLib.Topology;
using NetSnifferLib.Statistics;

namespace NetSnifferLib.Analysis
{
    public class PacketAnalyzer
    {
        public PacketAnalyzer()
        {
            DatagramAnalyzer.EthernetAnalyzer.PacketCaptured += EthernetAnalyzer_PacketCaptured;

            DatagramAnalyzer.IpV4Analyzer.PacketCaptured += IpV4Analyzer_PacketCaptured;
            //DatagramAnalyzer.IpV4Analyzer.PacketFromWan += IpV4Analyzer_PacketFromWan;

            DatagramAnalyzer.ArpAnalyzer.PayloadIndicatesHost += ArpAnalyzer_PayloadIndicatesHost;
        }
        private void EthernetAnalyzer_PacketCaptured(object sender, DataLinkPacketEventArgs e)
        {
            var source = e.Source;
            var destination = e.Destination;

            LanMapBuilder.AddHost(source);
            LanMapBuilder.AddHost(destination);
        }

        private void IpV4Analyzer_PacketCaptured(object sender, NetworkPacketEventArgs e)
        {
            var sourceIPAddress = e.SourceIPAddress;
            var sourcePhysicalAddress = e.SourcePhysicalAddress;

            var destinationIPAddress = e.DestinationIPAddress;
            var destinationPhysicalAddress = e.DestinationPhysicalAddress;

            LanMapBuilder.AddHost(sourceIPAddress, sourcePhysicalAddress);
            LanMapBuilder.AddHost(destinationIPAddress, destinationPhysicalAddress);
        }

        //private void IpV4Analyzer_PacketFromLan(object sender, NetworkPacketEventArgs e)
        //{
        //    var sourceIPAddress = e.SourceIPAddress;
        //    var sourcePhysicalAddress = e.SourcePhysicalAddress;

        //    LanMapBuilder.AddHost(sourceIPAddress, sourcePhysicalAddress);
        //}

        //private void IpV4Analyzer_PacketFromWan(object sender, NetworkPacketEventArgs e)
        //{
        //    var sourcePhysicalAddress = e.SourcePhysicalAddress;
        //    //LanMapBuilder.AddRouter(sourcePhysicalAddress);
        //}

        private void ArpAnalyzer_PayloadIndicatesHost(object sender, IPandPhysicalAddress e)
        {
            var iPandPhysicalAddress = e;
            LanMapBuilder.AddHost(iPandPhysicalAddress.IPAddress, iPandPhysicalAddress.PhysicalAddress);
        }


        public LanMapBuilder LanMapBuilder {get;} = new();

        public WanMap WanMapBuilder { get; } = new();

        public int TransmittedPackets { get; private set; } = 0;

        public int TransmittedBytes { get; private set; } = 0;

        public static bool IsEthernet(Packet packet) => packet.DataLink.Kind == DataLinkKind.Ethernet;

        public PacketDescription AnalyzePacket(Packet packet)
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

        public GeneralStatistics GetGeneralStatistics()
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
