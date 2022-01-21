using System.Text;

using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets.Dns;


using NetSnifferLib.Analysis.Transport;

namespace NetSnifferLib.Analysis.Application
{
    class DnsAnalyzer : BaseApplicationAnalyzer<DnsDatagram, UdpDatagram>
    {
        public override string Protocol => "DNS";

        private static string ConvertToString(DnsType type) => type switch {
            DnsType.A => "A",
            DnsType.Aaaa => "AAAA",
            _ => string.Empty
        };

        protected override string GetInfo(DnsDatagram datagram, TransportContext context)
        {
            //TODO: Finish
            var sb = new StringBuilder("");

            bool isResponse = datagram.IsResponse;

            if (datagram.IsRecursionDesired)
                sb.Append("Standard query");

            if (isResponse)
                sb.Append(" response");

            sb.AppendFormat(" 0x{0, 4}", datagram.Id.ToString("X4"));

            foreach(DnsQueryResourceRecord queryResourceRecord in datagram.Queries)
            {
                sb.AppendFormat(" {0}", ConvertToString(queryResourceRecord.DnsType));
                sb.AppendFormat(" {0}", queryResourceRecord.DomainName);
            }

            if (isResponse)
            {
                foreach (DnsDataResourceRecord dataResourceRecord in datagram.Answers)
                {
                    sb.AppendFormat(" {0}", ConvertToString(dataResourceRecord.DnsType));
                    sb.AppendFormat(" {0}", dataResourceRecord.ToString()); //TODO: Find
                }
            }

            return sb.ToString();
        }

        protected override bool TryGetDatagramCore(UdpDatagram transportDatagram, ref DnsDatagram datagram)
        {
            if (transportDatagram.Dns.IsValid)
            {
                datagram = transportDatagram.Dns;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
