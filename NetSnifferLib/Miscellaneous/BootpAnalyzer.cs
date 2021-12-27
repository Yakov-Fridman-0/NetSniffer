using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using NetSnifferLib.General;

namespace NetSnifferLib.Miscellaneous
{
    class BootpAnalyzer : BaseNoHostsNoPayloadAnalyzer
    {
        public override string ProtocolString => "BOOTP";

        public override string GetDatagramInfo(Datagram datagram)
        {
            return string.Empty;
        }
    }
}
