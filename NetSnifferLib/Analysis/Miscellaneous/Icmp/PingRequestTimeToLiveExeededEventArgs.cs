using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Analysis.Miscellaneous
{
    class PingRequestTimeToLiveExeededEventArgs : EventArgs
    {
        public IPAddress Source { get; }
        
        public IPAddress ExpectedSource { get; }

        public int Hops { get; }

        public uint Identifier { get; }

        public PingRequestTimeToLiveExeededEventArgs(IPAddress source, IPAddress expectedSoure, uint identifier, int hops)
        {
            Source = source;
            ExpectedSource = expectedSoure;
            Identifier = identifier;
            Hops = hops;
        }
    }
}
