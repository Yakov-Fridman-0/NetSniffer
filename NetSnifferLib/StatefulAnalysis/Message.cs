using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PcapDotNet.Packets;

namespace NetSnifferLib.StatefulAnalysis
{
    /// <summary>
    /// Represents a set of packets that should are combined togethers, e.g. IP fragmented TCP datgram or an HTTP request
    /// </summary>
    public abstract class Message
    {
        public int[] PacketIds { get; protected init; }

        public int PacketsCount { get; init; }

        protected Message(int[] packetIds)
        {
            PacketIds = packetIds;
            PacketsCount = packetIds.Length;
        }

        readonly List<AddittionalInfo> addittionalInfoList = new();
        public AddittionalInfo[] AddittionalInfo => addittionalInfoList.ToArray();

        public void AddAdditionInfo(AddittionalInfo info)
        {
            lock (AddittionalInfo)
                addittionalInfoList.Add(info);
        }

        readonly List<Attack> associatedThreatsList = new();
        public Attack[] AssociatedThreats => associatedThreatsList.ToArray();

        internal void AddThreat(Attack threat)
        {
            lock (associatedThreatsList)
                associatedThreatsList.Add(threat);
        }
    }
}
