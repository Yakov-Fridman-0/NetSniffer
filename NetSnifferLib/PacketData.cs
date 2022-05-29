using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
            { "tcp", new[] { "srcport", "dstport" } },
            { "dns", Array.Empty<string>() }
        };

        static readonly List<Attack> allAttacks = new();

        public static bool AreAllPacketsAnalyzed
        {
            get
            {
                lock (allPacketDatas)
                    return allPacketDatas.Values.All(packetData => packetData.Description != null);
            }
        }


        public static void CancelAllAnalysis()
        {
            foreach (var packetData in allPacketDatas.Values)
            {
                if (!packetData.task.IsCompleted)
                {
                    packetData.CancelAnalysis();
                }
            }
        }

        public static Attack[] AllAttacks
        {
            get
            {
                lock (allAttacks) 
                    return allAttacks.ToArray();
            }
        }

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
            lock (allPacketDatas)
                return allPacketDatas[packetId];
        }

        public static void Reset()
        {
            lock (allPacketDatas)
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

        public string[] Protocols => _protocols.Keys.ToArray();

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
        
        private PacketData(int packetId, Packet packet)
        {
            PacketId = packetId;
            Packet = packet;

            lock (allPacketDatas)
                allPacketDatas.Add(packetId, this);
        }

        public static PacketData Create(Packet packet)
        {
            int id = IdManager.GetNewPacketId(packet);

            return new PacketData(id, packet);
        }

        public void Analyze()
        {
            PacketDescription description;

            if (PacketAnalyzer.Analyzer.IsEthernet(Packet))
            {
                description = PacketAnalyzer.Analyzer.AnalyzePacket(Packet, PacketId);
            }
            else
            {
                description = new PacketDescription(Packet.Timestamp, "N/A", null, null, Packet.Length, "N/A");
            }

            Description = description;

            DescriptionReady.Invoke(PacketId, description);
        }


        public void CancelAnalysis()
        {
            tokenSource.Cancel();
        }

        Task task = null;
        readonly CancellationTokenSource tokenSource = new();

        public void AnalyzeAsync()
        {
            task = Task.Run(() => Analyze(), tokenSource.Token);
        }

        public PacketDescription Description { get; private set; } = null;

        internal void AddAttack(Attack attack)
        {
            lock (allAttacks)
            {
                if (!allAttacks.Contains(attack))
                    allAttacks.Add(attack);
            }

            lock (_attackList)
                _attackList.Add(attack);

            AttackDetected.Invoke(PacketId, attack);
        }

        internal void RemoveAttack(Attack attack)
        {
            _attackList.Remove(attack);

            AttackRuledOut.Invoke(PacketId, attack);
        }

        public event PacketDesciptionHandler DescriptionReady = delegate { };

        public event PacketAttackHandler AttackDetected = delegate { };

        public event PacketAttackHandler AttackRuledOut = delegate { };
    }
}
