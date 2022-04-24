using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ip;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.IpV6;
using PcapDotNet.Packets.Transport;

using NetSnifferLib.Analysis;
using NetSnifferLib.StatefulAnalysis;
using NetSnifferLib.General;

namespace NetSnifferLib
{
    public class PacketData
    {
        static readonly Dictionary<int, PacketData> allPacketDatas = new();

        static readonly Dictionary<string, string[]> supportedFields = new()
        {
            { "eth", new[] { "src", "dst" } },
            { "arp", new[] { "dst", "src" } },
            { "ip", new[] { "src", "dst" } },
            { "udp", new[] { "srcport", "dstport" } },
            { "tcp", new[] { "srcport", "dstport" } }
        };

        internal static bool IsValidField(string protocol, string field)
        {
            return supportedFields[protocol].Contains(field);
        }

        internal static bool IsValidDataType(string protocol, string filed, string literal)
        {
            object obj = null;
            return TryConvert(protocol, filed, literal, ref obj);
        }

        internal static bool TryConvert(string protocol, string field, string literal, ref object value)
        {
            switch (protocol)
            {
                case "eth":
                    switch (field)
                    {
                        case "src" or "dst":
                            PhysicalAddress address;
                            if (PhysicalAddress.TryParse(literal, out address))
                            {
                                value = address;
                                return true;
                            }
                            return false;

                        default:
                            throw new ArgumentException("Invalid field");
                    }
                case "arp":
                    switch (field)
                    {
                        case "src" or "dst":
                            PhysicalAddress address;
                            if (PhysicalAddress.TryParse(literal, out address))
                            {
                                value = address;
                                return true;
                            }
                            return false;

                        default:
                            throw new ArgumentException("Invalid field");
                    }
                case "ip":
                    switch (field)
                    {
                        case "src" or "dst":
                            IPAddress address;
                            if (IPAddress.TryParse(literal, out address))
                            {
                                value = address;
                                return true;
                            }
                            return false;

                        default:
                            throw new ArgumentException("Invalid field");
                    }
                case "udp":
                    switch (field)
                    {
                        case "srcport" or "dstport":
                            ushort port;
                            if (ushort.TryParse(literal, out port))
                            {
                                value = port;
                                return true;
                            }
                            return false;

                        default:
                            throw new ArgumentException("Invalid field");
                    }
                case "tcp":
                    switch (field)
                    {
                        case "srcport" or "dstport":
                            ushort port;
                            if (ushort.TryParse(literal, out port))
                            {
                                value = port;
                                return true;
                            }
                            return false;

                        default:
                            throw new ArgumentException("Invalid field");
                    }
                default:
                    throw new ArgumentException("Invalid field");
            }
        }

        internal object GetField(string protocol, string field)
        {
            switch (protocol)
            {
                case "eth":
                    var ethernetDatagram = (EthernetDatagram)this["eth"];
                    return field switch
                    {
                        "src" => AddressConvert.ToPhysicalAddress(ethernetDatagram.Source),
                        "dst" => AddressConvert.ToPhysicalAddress(ethernetDatagram.Destination),

                        _ => throw new ArgumentException("Invalid field"),
                    };
                case "arp":
                    var arpDatagram = (ArpDatagram)this["arp"];
                    return field switch
                    {
                        "src" => AddressConvert.ToPhysicalAddress(arpDatagram.SenderHardwareAddress),
                        "dst" => AddressConvert.ToPhysicalAddress(arpDatagram.TargetHardwareAddress),

                        _ => throw new ArgumentException("Invalid field"),
                    };
                case "ip":
                    var ipDatagram = (IpV4Datagram)this["ip"];
                    return field switch
                    {
                        "src" => AddressConvert.ToIPAddress(ipDatagram.Source),
                        "dst" => AddressConvert.ToIPAddress(ipDatagram.Destination),

                        _ => throw new ArgumentException("Invalid field"),
                    };
                case "udp":
                    var udpDatagrtam = (UdpDatagram)this["udp"];

                    return field switch
                    {
                        "srcport" => udpDatagrtam.SourcePort,
                        "dstport" => udpDatagrtam.DestinationPort,

                        _ => throw new ArgumentException("Invalid field"),
                    };
                case "tcp":
                    var tcpDatagram = (TcpDatagram)this["tcp"];

                    return field switch
                    {
                        "srcport" => tcpDatagram.SourcePort,
                        "dstport" => tcpDatagram.DestinationPort,

                        _ => throw new ArgumentException("Invalid field"),
                    };

                default:
                    throw new ArgumentException("Invalid field");
            }
        }

        public static PacketData GetPacketDataByPacketId(int packetId)
        {
            return allPacketDatas[packetId];
        }

        public static void Reset()
        {
            allPacketDatas.Clear();
        }

        public class PacketDataComparer : IComparer<PacketData>
        {
            public int Compare(PacketData x, PacketData y)
            {
                return x.PacketId - y.PacketId;
            }
        }

        public static PacketDataComparer IdComparer { get; } = new();
        

        public int PacketId { get; private init; }

        public Packet Packet { get; private init; }

        readonly List<Attack> _attackList = new();

        public Attack[] Attacks => _attackList.ToArray();

        readonly SortedDictionary<string, Datagram> _protocols = new();

        public Datagram this[string protocol]
        {
            get
            {
                Datagram datagram = null;
                bool result;

                lock (_protocols)
                {
                    var simplified = ProtocolNameComparer.Simplify(protocol);
                    result = _protocols.TryGetValue(simplified, out datagram);
                }

                return result ? datagram : null;
            }
        }

        internal void AddProtocol(string protocol, Datagram datagram)
        {
            _protocols.Add(ProtocolNameComparer.Simplify(protocol), datagram);
        }

        
        public PacketData(int packetId, Packet packet)
        {
            PacketId = packetId;
            Packet = packet;

            allPacketDatas.Add(packetId, this);
        }

        public void Analyze()
        {
            if (PacketAnalyzer.Analyzer.IsEthernet(Packet))
                DescriptionReady.Invoke(PacketId, PacketAnalyzer.Analyzer.AnalyzePacket(Packet, PacketId));
            else
                DescriptionReady.Invoke(PacketId, new PacketDescription(Packet.Timestamp, "N/A", null, null, Packet.Length, "N/A"));
        }
        
        internal void AddAttack(Attack attack)
        {
            _attackList.Add(attack);
        }

        public PacketDesciptionHandler DescriptionReady;

        public PacketAttackHandler AttackDetected;
    }
}
