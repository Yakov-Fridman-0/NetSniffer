using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSnifferLib.General;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Dns;

namespace NetSnifferLib.Miscellaneous
{
    public class DnsAnalyzer : BaseNoHostsNoPayloadAnalyzer
    {
        public override string ProtocolString => "DNS";

        public override string GetDatagramInfo(Datagram datagram)
        {
            //TODO: Finish
            var info = string.Empty;

            var dnsDatagram = (DnsDatagram)datagram;

            if (dnsDatagram.IsRecursionDesired)
                info += "Standard query";
            if (dnsDatagram.IsResponse)
                info += " response";

            info += $"{dnsDatagram.Id}";

            return info;
        }
    }
}
