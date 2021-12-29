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
        private static string ConvertToString(DnsType type) => type switch {
            DnsType.A => "A",
            DnsType.Aaaa => "AAAA",
            _ => string.Empty
        };

        public override string ProtocolString => "DNS";

        public override string GetDatagramInfo(Datagram datagram)
        {
            //TODO: Finish
            var sb = new StringBuilder("");

            var dnsDatagram = (DnsDatagram)datagram;

            bool isResponse = dnsDatagram.IsResponse;

            if (dnsDatagram.IsRecursionDesired)
                sb.Append("Standard query");

            if (isResponse)
                sb.Append(" response");

            sb.AppendFormat(" 0x{0, 4}", dnsDatagram.Id.ToString("X4"));

            foreach(DnsQueryResourceRecord queryResourceRecord in dnsDatagram.Queries)
            {
                sb.AppendFormat(" {0}", ConvertToString(queryResourceRecord.DnsType));
                sb.AppendFormat(" {0}", queryResourceRecord.DomainName);
            }

            if (isResponse)
            {
                foreach (DnsDataResourceRecord dataResourceRecord in dnsDatagram.Answers)
                {
                    sb.AppendFormat(" {0}", ConvertToString(dataResourceRecord.DnsType));
                    sb.AppendFormat(" {0}", dataResourceRecord.ToString()); //TODO: Find
                }
            }

            return sb.ToString();
        }
    }
}
