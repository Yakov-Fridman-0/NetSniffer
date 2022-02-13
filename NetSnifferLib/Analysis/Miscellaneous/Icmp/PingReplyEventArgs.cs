using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Analysis.Miscellaneous
{
    class PingReplyEventArgs : EventArgs
    {
        public IPAddress Source { get; }

        public uint Identifier { get; }

        public PingReplyEventArgs(IPAddress source, uint identifier)
        {
            Source = source;
            Identifier = identifier;
        }
    }
}
