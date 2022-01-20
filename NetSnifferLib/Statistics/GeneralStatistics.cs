namespace NetSnifferLib.Statistics
{
    public struct GeneralStatistics
    {
        public int Packets { get; internal set; }

        public int Bytes { get; internal set; }

        public int EthernetPackets { get; internal set; }

        public int EthernetPayloadBytes { get; internal set; }

        public int IpV4Packets { get; internal set; }

        public int IpV4PayloadBytes { get; internal set; }

        public int IpV6Packets { get; internal set; }

        public int IpV6PayloadBytes { get; internal set; }

        public int UdpPackets { get; internal set; }

        public int UdpPayloadBytes { get; internal set; }

        public int TcpPackets { get; internal set; }

        public int TcpPayloadBytes { get; internal set; }

        public static GeneralStatistics operator - (GeneralStatistics stat1, GeneralStatistics stat2)
        {
            var result = (GeneralStatistics)stat1.MemberwiseClone();
            result.Packets = stat1.Packets - stat2.Packets;
            result.Bytes = stat1.Bytes - stat2.Bytes;

            result.EthernetPackets = stat1.EthernetPackets - stat2.EthernetPackets;
            result.EthernetPayloadBytes = stat1.EthernetPayloadBytes - stat2.EthernetPayloadBytes;

            result.IpV4Packets = stat1.IpV4Packets - stat2.IpV4Packets;
            result.IpV4PayloadBytes = stat1.IpV4PayloadBytes - stat2.IpV4PayloadBytes;

            result.IpV6Packets = stat1.IpV6Packets - stat2.IpV6Packets;
            result.IpV6PayloadBytes = stat1.IpV6PayloadBytes - stat2.IpV6PayloadBytes;

            result.UdpPackets = stat1.UdpPackets - stat2.UdpPackets;
            result.UdpPayloadBytes = stat1.UdpPayloadBytes - stat2.UdpPayloadBytes;

            result.TcpPackets = stat1.TcpPackets - stat2.TcpPackets;
            result.TcpPayloadBytes = stat1.TcpPayloadBytes - stat2.TcpPayloadBytes;

            return result;
        }
    }
}
