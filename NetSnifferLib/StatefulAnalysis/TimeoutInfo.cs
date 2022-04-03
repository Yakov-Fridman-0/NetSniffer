using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis
{
    public readonly struct TimeoutInfo
    {
        public DateTime TimeoutStarted { get; init; }

        public DateTime TimeoutExpiered { get; init; }
    }
}
