using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSnifferLib.General;
using PcapDotNet.Packets;

namespace NetSnifferLib.Miscellaneous
{
    public class DhcpAnalyzer : BaseNoHostsNoPayloadAnalyzer
    {
        public override string ProtocolString => "DHCP";

        public override string GetDatagramInfo(Datagram datagram)
        {
            return string.Empty;
        }
    }
}
