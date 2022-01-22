namespace NetSnifferLib
{
    public class BasicStatistics
    {
        public int PacketNumber { get; set; }

        public int PacketLength { get; set; }

        public int EthernetPacketNumber { get; set; }

        public int EthernetPayload { get; set; }

        public int IpV4PacketNumber { get; set; }

        public int IpV4Payload { get; set; }

        public int IpV4PayloadLength { get; set; }

        public int IpV6PacketNumber { get; set; }

        public int IpV6PayloadLengt { get; set; }

        public int UdpPacketNumber { get; set; }

        public int UdpPayloadLength { get; set; }

        public int TcpPacketNumber { get; set; }

        public int TcpPayloadLength { get; set; }
    }
}
