using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis.Tcp
{
    public enum TcpConnectionStatus : int
    {
        None,
        Syn,
        SynAck,
        Established,
        Fin,
        FinAck,
        Closed
    }
}
