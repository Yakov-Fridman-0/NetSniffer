using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib
{
    public class PackeStatisticsLogger
    {
        public int TotalPacketNumber { get; private set; }

        public int TotalBytes { get; private set; }

        public int EthernetPacketNumber { get; private set; }

        public int EthernetTotalPayloadBytes { get; private set; }

        public int IpV4PacketNumber { get; private set; }

        public int IpV4TotalPayloadBytes { get; private set; }

        public int IpV6TotalPayloadBytes { get; private set; }

        public int IpV6PacketNumber { get; private set; }

        public int UdpPacketNumber { get; private set; }

        public int TcpPacketNumber { get; private set; }

        public int UdpTotalPayloadBytes { get; private set; }

        public int TcpTotalPayloadBytes { get; private set; }

        public void LogPacket(ushort packetLenght)
        {
            TotalPacketNumber++;
            TotalBytes += packetLenght;
        }

        public void LogEthernetPacket(ushort payloadLength)
        {
            EthernetPacketNumber++;
            EthernetTotalPayloadBytes += payloadLength;
        }

        public void LogIpV4Packet(ushort payloadLength)
        {
            IpV4PacketNumber++;
            IpV4TotalPayloadBytes += payloadLength;
        }

        public void LogIpV6Packet(ushort payloadLength)
        {
            IpV6PacketNumber++;
            IpV6TotalPayloadBytes += payloadLength;
        }

        public void LogTcpPacket(ushort payloadLength)
        {
            TcpPacketNumber++;
            TcpTotalPayloadBytes += payloadLength;
        }

        public void LogUdpPacket(ushort payloadLength)
        {
            UdpPacketNumber++;
            UdpTotalPayloadBytes += payloadLength;
        }
    }
}
