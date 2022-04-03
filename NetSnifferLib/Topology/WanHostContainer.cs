using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    public sealed class WanHostContainer : IHostContainer
    {
        public bool IsLanHost => false;

        public LanHost LanHost => throw new InvalidOperationException();

        public bool IsWanHost => true;

        public WanHost WanHost { get; }

        public WanHostContainer(WanHost host)
        {
            WanHost = host;
        }
    }
}
