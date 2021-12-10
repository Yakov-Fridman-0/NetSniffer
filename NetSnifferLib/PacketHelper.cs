using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib
{
    public static class PacketHelper
    {
        public enum OSILayer
        {
            PhysicalLayer = 0,
            DataLinkLayer = 1,
            BetweenDataLinkAndNetwork = 2,
            NetworkLayer = 3,
            TransportLayer = 4,
            SessionLayer = 5,
            PresentationLayer = 6,
            ApplicationLayer = 7
        }

        public static OSILayer GetLayer(Packet packet)
        {
            var ethernet = packet.Ethernet;

            if (packet.Ethernet.EtherType == PcapDotNet.Packets.Ethernet.EthernetType.Arp)
            {
                return OSILayer.BetweenDataLinkAndNetwork;
            }

            var ip = ethernet.IpV4;

            if (ip == null)
                return OSILayer.DataLinkLayer;
            if (ip.Udp != null || ip.Tcp != null)
                return OSILayer.TransportLayer;
            return OSILayer.NetworkLayer;
        }

        public static DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp; ;
        }

        public static bool GetArpData(Packet packet, out PhysicalAddress source, out PhysicalAddress dest)
        {
            var arp = packet.Ethernet.Arp;

            byte[] byteArray = new byte[6];

            var hardwareAddress = arp.SenderHardwareAddress;
            hardwareAddress.CopyTo(byteArray, 0);
            source = new PhysicalAddress(byteArray);

            hardwareAddress = arp.TargetHardwareAddress;
            hardwareAddress.CopyTo(byteArray, 0);
            dest = new PhysicalAddress(byteArray);

            return true;
        }

        public static bool GetIpData(Packet packet, out int payloadLength, out IPAddress source, out IPAddress destination)
        {
            var ip = packet.Ethernet.IpV4;

            payloadLength = ip.Payload.Length;
            source = IPAddress.Parse(ip.Source.ToString());
            destination = IPAddress.Parse(ip.Destination.ToString());

            return true;
        }

        public static bool GetTransportData(
            Packet packet,
            out IpV4Protocol protocol,
            out int payloadLength,
            out ushort sourcePort,
            out ushort dstPort)
        {
            var ip = packet.Ethernet.IpV4;
            protocol = ip.Protocol;

            if (protocol == IpV4Protocol.Tcp)
            {
                var tcp = ip.Tcp;
                payloadLength = tcp.Payload.Length;
                sourcePort = tcp.SourcePort;
                dstPort = tcp.DestinationPort;
            }
            else if (protocol == IpV4Protocol.Udp)
            {
                var udp = ip.Udp;
                payloadLength = udp.Payload.Length;
                sourcePort = udp.SourcePort;
                dstPort = udp.DestinationPort;
            }
            else
            {
                payloadLength = 0;
                sourcePort = 0;
                dstPort = 0;
            }

            return true;
        }
    }
}
