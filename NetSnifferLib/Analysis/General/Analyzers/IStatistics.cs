using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.General
{
    interface IStatistics
    {
        public int SentPackets { get; }

        public int SentBytes { get; }
    }
}
