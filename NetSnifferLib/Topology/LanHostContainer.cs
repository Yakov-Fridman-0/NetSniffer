using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    public sealed class LanHostContainer : IHostContainer
    {
        public bool IsLanHost => true;

        public LanHost LanHost { get; }

        public bool IsWanHost => false;

        public WanHost WanHost => throw new InvalidOperationException();

        public LanHostContainer(LanHost host)
        {
            LanHost = host;
        }
    }
}
