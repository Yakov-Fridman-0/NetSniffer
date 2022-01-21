using System.Linq;

using PcapDotNet.Packets.Transport;

using NetSnifferLib.Packets.Dhcp;
using NetSnifferLib.Analysis.Transport;

namespace NetSnifferLib.Analysis.Application
{
    class DhcpAnalyzer : BaseApplicationAnalyzer<DhcpDatagram, UdpDatagram>
    {
        public override string Protocol => "DHCP";

        protected override string GetInfo(DhcpDatagram datagram, TransportContext context)
        {
            return string.Empty;
        }

        protected override bool TryGetDatagramCore(UdpDatagram transportDatagram, ref DhcpDatagram datagram)
        {
            var dhcpDatagram = new DhcpDatagram(transportDatagram.Payload.ToArray());

            if (dhcpDatagram.IsValid)
            {
                datagram = dhcpDatagram;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
