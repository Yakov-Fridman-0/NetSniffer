using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.IpV6;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib
{

    public static class PacketHelper
    {
        public static string ToString(PhysicalAddress physicalAddress)
        {
            var addrress = string.Empty;
            byte[] bytes = physicalAddress.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                // Display the physical address in hexadecimal.
                addrress += bytes[i].ToString("X2");

                // Insert a hyphen after each byte, unless we are at the end of the  address.
                if (i != bytes.Length - 1)
                {
                    addrress += ("-");
                }
            }

            return addrress;

        }

        public static string ToString(MacAddress macAddress)
        {
            var physicalAddress = PhysicalAddress.Parse(macAddress.ToString());

            return ToString(physicalAddress);
        }
    }
   
    public class ArpAnalyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.Arp;
        }

        public string GetProtocol(Packet packet)
        {
            return EthernetType.Arp.ToString();
        }

        static PhysicalAddress GetPhysicalAddress(ReadOnlyCollection<byte> address)
        {
            byte[] byteArray = new byte[address.Count];
            address.CopyTo(byteArray, 0);
            var physicalAddress = new PhysicalAddress(byteArray);

            return physicalAddress;
        }

        public string GetPacketSource(Packet packet)
        {
            var address = GetPhysicalAddress(packet.Ethernet.Arp.SenderHardwareAddress);

            return PacketHelper.ToString(address);
        }

        public string GetPacketDestination(Packet packet)
        {
            var address = GetPhysicalAddress(packet.Ethernet.Arp.TargetHardwareAddress);

            return PacketHelper.ToString(address);
        }

        public ushort GetLength(Packet packet)
        {
            return 0;
        }

        static string GetPacketSourceIpV4(Packet packet)
        {
            var ip = packet.Ethernet.Arp.SenderProtocolIpV4Address;

            return ip.ToString();
        }

        static string GetPacketDestinationIpV4(Packet packet)
        {
            var ip = packet.Ethernet.Arp.TargetProtocolIpV4Address;

            return ip.ToString();
        }

        public string GetPacketInfo(Packet packet)
        {
            return packet.Ethernet.Arp.Operation switch
            {
                PcapDotNet.Packets.Arp.ArpOperation.Request => $"Who has {GetPacketDestinationIpV4(packet)}? Tell {GetPacketSourceIpV4(packet)}",

                PcapDotNet.Packets.Arp.ArpOperation.Reply => $"{GetPacketSourceIpV4(packet)} is at {GetPacketSource(packet)}",

                _ => string.Empty
            };
        }

    }

    public abstract class IpAnalyzer : IPacketAnalyzer
    {
        public abstract bool IsMatch(Packet packet);

        public abstract string GetProtocol(Packet packet);

        public abstract string GetPacketSource(Packet packet);

        public abstract string GetPacketDestination(Packet packet);

        public abstract ushort GetLength(Packet packet);

        public abstract string GetPacketInfo(Packet packet);
    }

    public class IpV4Analyzer : IpAnalyzer
    {
        public override bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.IpV4;
        }

        public override string GetProtocol(Packet packet)
        {
            return EthernetType.IpV4.ToString();
        }

        public override string GetPacketSource(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Source;

            return ip.ToString();
        }

        public override string GetPacketDestination(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Destination;

            return ip.ToString();
        }

        public override ushort GetLength(Packet packet)
        {
            var ethernetLayer = packet.Ethernet.ExtractLayer() as EthernetLayer;
            var length = (ushort)ethernetLayer.Length;

            return length;
        }

        public override string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public class IpV6Analyzer : IpAnalyzer
    {
        public override bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.IpV6;
        }

        public override string GetProtocol(Packet packet)
        {
            return EthernetType.IpV6.ToString();
        }

        public override string GetPacketDestination(Packet packet)
        {
            //TODO: Check this
            var ip = packet.Ethernet.IpV6.CurrentDestination;

            return ip.ToString();
        }

        public override string GetPacketSource(Packet packet)
        {
            var ip = packet.Ethernet.IpV6.Source;

            return ip.ToString();
        }

        public override ushort GetLength(Packet packet)
        {
            var ethernetLayer = packet.Ethernet.ExtractLayer() as EthernetLayer;
            var length = (ushort)ethernetLayer.Length;

            return length;
        }

        public override string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public abstract class UdpAnalyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return IpAnalyzer.IsMatch(packet) && ProtocolMatches(packet);
        }

        public string GetProtocol(Packet packet)
        {
            return IpV4Protocol.Udp.ToString();
        }

        public string GetPacketSource(Packet packet)
        {
            return IpAnalyzer.GetPacketSource(packet);
        }
        
        public string GetPacketDestination(Packet packet)
        {
            return IpAnalyzer.GetPacketDestination(packet);
        }

        public ushort GetLength(Packet packet)
        {
            return (ushort)GetUdpDatagram(packet).Length;
        }

        protected ushort GetPacketSourcePort(Packet pakcet)
        {
            return GetUdpDatagram(pakcet).SourcePort;
        }

        protected ushort GetPacketDestinationPort(Packet pakcet)
        {
            return GetUdpDatagram(pakcet).DestinationPort;
        }

        public string GetPacketInfo(Packet packet)
        {
            return $"{GetUdpDatagram(packet)} → {GetPacketDestinationPort(packet)}, Len={GetLength(packet)}";
        }

        protected abstract bool ProtocolMatches(Packet packet);

        protected abstract IpAnalyzer IpAnalyzer
        {
            get;
        }

        protected abstract PcapDotNet.Packets.Transport.UdpDatagram GetUdpDatagram(Packet packet);
    }

    public class UdpIpV4Analyzer : UdpAnalyzer
    {
        protected override bool ProtocolMatches(Packet packet)
        {
            return packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp;
        }

        protected override IpAnalyzer IpAnalyzer => PacketAnalyzer.IpV4Analyzer;

        protected override PcapDotNet.Packets.Transport.UdpDatagram GetUdpDatagram(Packet packet)
        {
            return packet.Ethernet.IpV4.Udp;
        }
    }

    public class UdpIpV6Analyzer : UdpAnalyzer
    {
        protected override bool ProtocolMatches(Packet packet)
        {
            return packet.Ethernet.IpV6.Protocol == IpV4Protocol.Udp;
        }

        protected override IpAnalyzer IpAnalyzer => PacketAnalyzer.IpV6Analyzer;

        protected override PcapDotNet.Packets.Transport.UdpDatagram GetUdpDatagram(Packet packet)
        {
            return packet.Ethernet.IpV6.Udp;
        }

    }

    public class DHCPv4Analyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            /*
             DHCP uses User Datagram Protocol (UDP), RFC 768, as its transport protocol. 
             DHCP messages that a client sends to a server are sent to well-known port 67 (UDP—Bootstrap Protocol and DHCP). 
             DHCP Messages that a server sends to a client are sent to port 68.
             */
            return Pack
                && (packet.Ethernet.IpV4.Udp.DestinationPort == 67 || packet.Ethernet.IpV4.Udp.DestinationPort == 68)
                && (packet.Ethernet.IpV4.Udp.SourcePort == 67 || packet.Ethernet.IpV4.Udp.SourcePort == 68);
        }

        public string GetProtocol(Packet packet)
        {
            return "DHCPv4";
        }

        public string GetPacketDestination(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Destination;
            var port = packet.Ethernet.IpV4.Udp.DestinationPort;

            return $"{ip}:{port}";
        }

        public string GetPacketSource(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Source;
            var port = packet.Ethernet.IpV4.Udp.SourcePort;
            var length = packet.Ethernet.IpV4.Udp.Length;

            return $"{ip}:{port}";
        }

        public ushort GetLength(Packet packet)
        {
            return (ushort)packet.Ethernet.IpV4.Udp.Length;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public class DHCPv6V6Analyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            /*
            DHCP uses User Datagram Protocol (UDP), RFC 768, as its transport protocol. 
            DHCP messages that a client sends to a server are sent to well-known port 67 (UDP—Bootstrap Protocol and DHCP). 
            DHCP Messages that a server sends to a client are sent to port 68.
            */
            return packet.Ethernet.EtherType == EthernetType.IpV6
                && (packet.Ethernet.IpV4.Udp.DestinationPort == 67 || packet.Ethernet.IpV4.Udp.DestinationPort == 68)
                && (packet.Ethernet.IpV4.Udp.SourcePort == 67 || packet.Ethernet.IpV4.Udp.SourcePort == 68);
        }

        public string GetProtocol(Packet packet)
        {
            return "DHCPv6";
        }

        public string GetPacketDestination(Packet packet)
        {
            var ethernetLayer = packet.Ethernet.ExtractLayer() as EthernetLayer;
            var ip = ethernetLayer.Destination;

            return $"{ip}";
        }

        public string GetPacketSource(Packet packet)
        {
            var ethernetLayer = packet.Ethernet.ExtractLayer() as EthernetLayer;
            var ip = ethernetLayer.Source;

            return $"{ip}";
        }

        public ushort GetLength(Packet packet)
        {
            var ethernetLayer = packet.Ethernet.ExtractLayer() as EthernetLayer;
            var length = (ushort)ethernetLayer.Length;

            return length;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public class HTTP80Analyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.IpV4
                && packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp
                && (packet.Ethernet.IpV4.Tcp.SourcePort == 80 || packet.Ethernet.IpV4.Tcp.DestinationPort == 80);
        }

        public string GetProtocol(Packet packet)
        {
            return "HTTPS";
        }

        public string GetPacketDestination(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Destination;
            var port = packet.Ethernet.IpV4.Tcp.DestinationPort;

            return $"{ip}:{port}";
        }

        public string GetPacketSource(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Source;
            var port = packet.Ethernet.IpV4.Tcp.SourcePort;

            return $"{ip}:{port}";
        }

        public ushort GetLength(Packet packet)
        {
            return (ushort)packet.Ethernet.IpV4.Tcp.Length;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public class HTTPS443Analyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.IpV4 
                && packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp
                && (packet.Ethernet.IpV4.Tcp.SourcePort == 443 || packet.Ethernet.IpV4.Tcp.DestinationPort == 443);
        }

        public string GetProtocol(Packet packet)
        {
            return "HTTPS";
        }

        public string GetPacketDestination(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Destination;
            var port = packet.Ethernet.IpV4.Tcp.DestinationPort;

            return $"{ip}:{port}";
        }

        public string GetPacketSource(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Source;
            var port = packet.Ethernet.IpV4.Tcp.SourcePort;

            return $"{ip}:{port}";
        }

        public ushort GetLength(Packet packet)
        {
            return (ushort)packet.Ethernet.IpV4.Tcp.Length;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }
  
    public class TcpIpV4Analyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.IpV4 && packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp;
        }

        public string GetProtocol(Packet packet)
        {
            return IpV4Protocol.Tcp.ToString();
        }

        public string GetPacketDestination(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Destination;
            var port = packet.Ethernet.IpV4.Tcp.DestinationPort;
          
            return $"{ip}:{port}";
        }

        public string GetPacketSource(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Source;
            var port = packet.Ethernet.IpV4.Tcp.SourcePort;
          
            return $"{ip}:{port}";
        }

        public ushort GetLength(Packet packet)
        {
            return (ushort)packet.Ethernet.IpV4.Tcp.Length;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public class TcpIpV6Analyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.IpV4 && packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp;
        }

        public string GetProtocol(Packet packet)
        {
            return IpV4Protocol.Tcp.ToString();
        }

        public string GetPacketDestination(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Destination;
            var port = packet.Ethernet.IpV4.Tcp.DestinationPort;

            return $"{ip}:{port}";
        }

        public string GetPacketSource(Packet packet)
        {
            var ip = packet.Ethernet.IpV4.Source;
            var port = packet.Ethernet.IpV4.Tcp.SourcePort;

            return $"{ip}:{port}";
        }

        public ushort GetLength(Packet packet)
        {
            return (ushort)packet.Ethernet.IpV4.Tcp.Length;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public class GeneralAnalyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return true;
        }

        public string GetProtocol(Packet packet)
        {
            return packet.Ethernet.EtherType.ToString();
        }


        public string GetPacketDestination(Packet packet)
        {
            return PacketHelper.ToString(packet.Ethernet.Destination);
        }

        public string GetPacketSource(Packet packet)
        {
            return PacketHelper.ToString(packet.Ethernet.Source);
        }

        public ushort GetLength(Packet packet)
        {
            var ethernetLayer = packet.Ethernet.ExtractLayer() as EthernetLayer;
            var length = (ushort)ethernetLayer.Length;

            return length;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public static class PacketAnalyzer2
    {
        static readonly IPacketAnalyzer[] _analyzers;

        static readonly IpV4Analyzer _ipV4Analyzer;
        static readonly IpV6Analyzer _ipV6Analyzer;

        static readonly UdpIpV4Analyzer _udpV4Analyzer;
        static readonly UdpIpV6Analyzer _udpV6Analyzer;

        static PacketAnalyzer()
        {
            _ipV4Analyzer = new IpV4Analyzer();
            _ipV6Analyzer = new IpV6Analyzer();

            _udpV4Analyzer = new UdpIpV4Analyzer();
            _udpV6Analyzer = new UdpIpV6Analyzer();

            _analyzers = new IPacketAnalyzer[] {
                new ArpAnalyzer(),

                new HTTP80Analyzer(),
                new HTTPS443Analyzer(),

                new DHCPv6V6Analyzer(),
                _udpV4Analyzer,

                new TcpIpV6Analyzer(),
                _ipV6Analyzer,

                new DHCPv4Analyzer(),
                new UdpIpV4Analyzer(),

                new TcpIpV4Analyzer(),
                _ipV4Analyzer,

                new GeneralAnalyzer(),
            };
        }

        public static IpV4Analyzer IpV4Analyzer
        {
            get => _ipV4Analyzer;
        }

        public static IpV6Analyzer IpV6Analyzer
        {
            get => _ipV6Analyzer;
        }

        public static IPacketAnalyzer GetAnalyzer(Packet packet)
        {
            var analyzer = _analyzers.FirstOrDefault(item => item.IsMatch(packet));
            return analyzer;
        }
    }
}
