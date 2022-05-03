using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSnifferLib.Topology;

namespace NetSnifferApp
{
    class WanHostItem
    {
        public WanHost Host { get; init; }

        public WanHostItem(WanHost host)
        {
            Host = host;
        }

        public override string ToString()
        {
            return Host.ToString();
        }
    }
}
