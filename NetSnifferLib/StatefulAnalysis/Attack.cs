using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetSnifferLib.General;
using NetSnifferLib.Topology;

namespace NetSnifferLib.StatefulAnalysis
{
    public class Attack
    {
        public string Name { get; init; }

        public int[] PacketIds { get; init; }

        public IAddress[] Attackers { get; init; }

        public IAddress[] Targets { get; init; }


        public Attack(string name, int[] packetIds, IAddress[] attackers, IAddress[] targets)
        {
            Name = name;

            PacketIds = new int[packetIds.Length];
            packetIds.CopyTo(PacketIds, 0);

            Attackers = new IAddress[attackers.Length];
            attackers.CopyTo(Attackers, 0);

            Targets = new IAddress[targets.Length];
            targets.CopyTo(Targets, 0);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
