using System;
using System.Net;
using System.Linq;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Threading.Tasks;

using PcapDotNet.Packets;

using NetSnifferLib.Analysis.Miscellaneous;
using NetSnifferLib.General;
using NetSnifferLib.Topology;
using NetSnifferLib.Statistics;

namespace NetSnifferLib.Analysis
{
    public class PacketAnalyzer
    {
        public static PacketAnalyzer Analyzer { get; private set; } = new();

        public static void CreateNewAnalyzer()
        {
            Analyzer = new();
        }

        public readonly TopologyBuilder topologyBuilder = new();

        readonly Dictionary<TracertResults, bool> tracertResults = new();

        public NetSniffer Sniffer { get; set; } = null;

        public IPAddress LocalComputerIPAddress { get; set; } = null;

        public PhysicalAddress LocalComputerPhysicalAddress { get; set; } = null;

        void EthernetAnalyzer_PacketCaptured(object sender, DataLinkPacketEventArgs e)
        {
            var source = e.Source;
            var destination = e.Destination;

            topologyBuilder.AddHost(source);
            topologyBuilder.AddHost(destination);
        }

        void IpV4Analyzer_PacketCaptured(object sender, NetworkPacketEventArgs e)
        {
            var sourceIPAddress = e.SourceIPAddress;
            var sourcePhysicalAddress = e.SourcePhysicalAddress;

            var destinationIPAddress = e.DestinationIPAddress;
            var destinationPhysicalAddress = e.DestinationPhysicalAddress;

            topologyBuilder.AddHost(sourcePhysicalAddress, sourceIPAddress);
            topologyBuilder.AddHost(destinationPhysicalAddress, destinationIPAddress);
        }

        void ArpAnalyzer_PayloadIndicatesHost(object sender, PayloadIndicatesHostEventArgs e)
        {
            var ipAddress = e.IPAddress;
            var physicalAddress = e.PhysicalAddress;
            topologyBuilder.AddHostInLan(physicalAddress, ipAddress);
        }

        void DhcpAnalyzer_ServerDetected(object sender, IPAddress e)
        {
            topologyBuilder.AddDhcpServer(e);
        }

        void DnsAnalyzer_ServerDetected(object sender, IPAddress e)
        {
            topologyBuilder.AddDnsServer(e);
        }

        void IcmpAnalyzer_RegisteredPingReply(object sender, PingReplyEventArgs e)
        {
            IPAddress src = e.Source;

            var tracertResult = tracertResults.FirstOrDefault((kvp) => kvp.Key.Destination.Equals(src)).Key;
            tracertResult.IsComplete = true;

            topologyBuilder.IntegrateTracertResults(tracertResult);
        }

        void IcmpAnalyzer_RegisteredPingRequestTimeToLiveExceeded(object sender, PingRequestTimeToLiveExeededEventArgs e)
        {
            IPAddress src = e.Source;
            IPAddress intendedSrc = e.ExpectedSource;

            var tracertResult = tracertResults.FirstOrDefault((kvp) => kvp.Key.Destination.Equals(src)).Key;
            tracertResults[tracertResult] = true;
            int hops = e.Hops;

            tracertResult.AddHop(src, hops);
        }

        private PacketAnalyzer()
        {
            DatagramAnalyzer.IcmpAnalyzer.RegisteredPingReply += IcmpAnalyzer_RegisteredPingReply;
            DatagramAnalyzer.IcmpAnalyzer.RegisteredPingRequestTimeToLiveExceeded += IcmpAnalyzer_RegisteredPingRequestTimeToLiveExceeded;

            DatagramAnalyzer.EthernetAnalyzer.PacketCaptured += EthernetAnalyzer_PacketCaptured;
            DatagramAnalyzer.IpV4Analyzer.PacketCaptured += IpV4Analyzer_PacketCaptured;

            DatagramAnalyzer.DhcpAnalyzer.ServerDetected += DhcpAnalyzer_ServerDetected;
            DatagramAnalyzer.DnsAnalyzer.ServerDetected += DnsAnalyzer_ServerDetected;

            DatagramAnalyzer.ArpAnalyzer.PayloadIndicatesHost += ArpAnalyzer_PayloadIndicatesHost;
        }

        public int TransmittedPackets { get; private set; } = 0;

        public int TransmittedBytes { get; private set; } = 0;

        public bool IsEthernet(Packet packet) => packet.DataLink.Kind == DataLinkKind.Ethernet;

        public PacketDescription AnalyzePacket(Packet packet, int packetId)
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
                analysis = analyzer.AnalyzeDatagram(datagram, context, packetId);

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

        public Task<PacketDescription> AnalyzePacketAsync(Packet packet, int packetId)
        {
            return Task.Run(new Func<PacketDescription>(() => AnalyzePacket(packet, packetId)));
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

        public void Ping(IPAddress destination)
        {
            ((LiveSniffer)Sniffer).Ping(destination, 128);
        }

        public Task TracertAsync(IPAddress destination)
        {
            return Task.Run(() => Tracert(destination));
        }

        public void Tracert(IPAddress destination)
        {
            int maxHops = 30;

            TracertResults results = new(LocalComputerIPAddress, destination);
            tracertResults.Add(results, false);

            for (int hops = 1; hops < maxHops; hops++)
            {
                PingReply reply = ((LiveSniffer)Sniffer).Ping(destination, (byte)hops);

                switch (reply.Status)
                {
                    case IPStatus.TimedOut:
                        results.AddHop(null, hops);
                        break;
                    case IPStatus.TtlExpired:
                        results.AddHop(reply.Address, hops);
                        break;
                    case IPStatus.Success:
                        results.IsComplete = true;
                        results.Successfull = true;
                        topologyBuilder.IntegrateTracertResults(results);
                        return;
                }
            }

            results.IsComplete = true;
            results.Successfull = false;
            topologyBuilder.IntegrateTracertResults(results);
        }

        public LanMap GetLanMap()
        {
            return topologyBuilder.LanMap;
        }

        internal List<LanHost> GetLanHosts()
        {
            return topologyBuilder.GetOriginalLanHosts();
        }

        public WanMap GetWanMap()
        {
            return topologyBuilder.WanMap;
        }

        internal List<WanHost> GetWanHosts()
        {
            return topologyBuilder.GetOriginalWanHosts();
        }
    }
}
