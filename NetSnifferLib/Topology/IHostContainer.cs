using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    public interface IHostContainer
    {
        bool IsLanHost { get; }

        LanHost LanHost { get; }

        bool IsWanHost { get; }

        WanHost WanHost { get; }
    }
}
