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


        static readonly WanHostItem _empty = new(null);

        public static WanHostItem Empty => _empty;

        public WanHostItem(WanHost host)
        {
            Host = host;
        }

        public override string ToString()
        {
            return (this != Empty) ? Host.ToString() : "(none)";
        }
    }
}
