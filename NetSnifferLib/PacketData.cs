using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PcapDotNet.Packets;

using NetSnifferLib.Analysis;
using NetSnifferLib.StatefulAnalysis;

namespace NetSnifferLib
{
    public class PacketData
    {
        static readonly Dictionary<int, PacketData> allPacketDatas = new();

        internal static PacketData GetPacketDataByPacketId(int packetId)
        {
            return allPacketDatas[packetId];
        }

        public static PacketDataComparer Comparer { get; } = new();
        
        public class PacketDataComparer : IComparer<PacketData>
        {
            public int Compare(PacketData x, PacketData y)
            {
                return x.PacketId - y.PacketId;
            }
        }

        public int PacketId { get; private init; }

        public Packet Packet { get; private init; }

        readonly List<Attack> _attackList = new();

        public Attack[] Attacks => _attackList.ToArray();

        public PacketData(int packetId, Packet packet)
        {
            PacketId = packetId;
            Packet = packet;

            allPacketDatas.Add(packetId, this);
        }

        async public void StartAnalysis()
        {
            if (PacketAnalyzer.IsEthernet(Packet))
                DescriptionReady.Invoke(PacketId, await Task.Run(() => PacketAnalyzer.AnalyzePacket(Packet, PacketId)));
            else
                DescriptionReady.Invoke(PacketId, new PacketDescription(Packet.Timestamp, "N/A", null, null, Packet.Length, "N/A"));
        }

        public void AddAttack(Attack attack)
        {
            _attackList.Add(attack);
        }

        public PacketDesciptionHandler DescriptionReady;

        public PacketAttackHandler AttackDetected;
    }
}
