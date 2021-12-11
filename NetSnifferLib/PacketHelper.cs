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
    public interface IPacketAnalyzer
    {
        bool IsMatch(Packet packet);

        DateTime GetTimeStamp(Packet packet);

        string GetProtocol(Packet packet);

        string GetPacketSource(Packet packet);
       
        string GetPacketDestination(Packet packet);

        ushort GetPayloadLength(Packet packet);

        string GetPacketInfo(Packet packet);
    }
   
    public class ArpAnalyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.Arp;
        }
        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
        }

        public string GetProtocol(Packet packet)
        {
            return EthernetType.Arp.ToString();
        }
        PhysicalAddress GetPhysicalAddress(ReadOnlyCollection<byte> address)
        {

            byte[] byteArray = new byte[address.Count];
            address.CopyTo(byteArray, 0);
            var physicalAddress = new PhysicalAddress(byteArray);

            return physicalAddress;
        }
        public string GetPacketDestination(Packet packet)
        {
            var ip = packet.Ethernet.Arp.TargetProtocolIpV4Address;
            
            return $"{ ip }";
        }

        public string GetPacketSource(Packet packet)
        {
            var address = GetPhysicalAddress(packet.Ethernet.Arp.SenderHardwareAddress);

            return PacketHelper.ToString(address);
        }

        public ushort GetPayloadLength(Packet packet)
        {
            return 0;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
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
            return packet.Ethernet.EtherType == EthernetType.IpV4 
                && packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp
                && (packet.Ethernet.IpV4.Udp.DestinationPort == 67 || packet.Ethernet.IpV4.Udp.DestinationPort == 68)
                && (packet.Ethernet.IpV4.Udp.SourcePort == 67 || packet.Ethernet.IpV4.Udp.SourcePort == 68);
        }

        public string GetProtocol(Packet packet)
        {
            return "DHCP(v4)";
        }

        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
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

        public ushort GetPayloadLength(Packet packet)
        {
            return (ushort)packet.Ethernet.IpV4.Udp.Length;
        }

        public string GetPacketInfo(Packet packet)
        {
            return string.Empty;
        }
    }

    public class UdpIpV4Analyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == PcapDotNet.Packets.Ethernet.EthernetType.IpV4 && packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp;
        }

        public string GetProtocol(Packet packet)
        {
            return IpV4Protocol.Udp.ToString();
        }

        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
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

        public ushort GetPayloadLength(Packet packet)
        {
            return (ushort)packet.Ethernet.IpV4.Udp.Length;
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

        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
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

        public ushort GetPayloadLength(Packet packet)
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

        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
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

        public ushort GetPayloadLength(Packet packet)
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

        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
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

        public ushort GetPayloadLength(Packet packet)
        {
            return (ushort)packet.Ethernet.IpV4.Tcp.Length;
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
            return "DHCP(v6)";
        }

        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
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

        public ushort GetPayloadLength(Packet packet)
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
   
    public class IpV6Analyzer : IPacketAnalyzer
    {
        public bool IsMatch(Packet packet)
        {
            return packet.Ethernet.EtherType == EthernetType.IpV6;
        }

        public string GetProtocol(Packet packet)
        {
            return EthernetType.IpV6.ToString();
        }

        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
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

        public ushort GetPayloadLength(Packet packet)
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

        public DateTime GetTimeStamp(Packet packet)
        {
            return packet.Timestamp;
        }

        public string GetPacketDestination(Packet packet)
        {
            return PacketHelper.ToString(packet.Ethernet.Destination);
        }

        public string GetPacketSource(Packet packet)
        {
            return PacketHelper.ToString(packet.Ethernet.Source);
        }

        public ushort GetPayloadLength(Packet packet)
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
    public static class PacketAnalyzer
    {
        static IPacketAnalyzer[] _analyzers;
        static PacketAnalyzer()
        {
            _analyzers = new IPacketAnalyzer[] {
                new ArpAnalyzer(),
                new DHCPv4Analyzer(),
                new UdpIpV4Analyzer(),
                new HTTP80Analyzer(),
                new HTTPS443Analyzer(),
                new TcpIpV4Analyzer(),
                new DHCPv6V6Analyzer(),
                new IpV6Analyzer(),
                new GeneralAnalyzer(),
            };
        }
        public static IPacketAnalyzer GetAnalyzer(Packet packet)
        {
            var analyzer = _analyzers.FirstOrDefault(item => item.IsMatch(packet));
            return analyzer;
        }
    }

}
