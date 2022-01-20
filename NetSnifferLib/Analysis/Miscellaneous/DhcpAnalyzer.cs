using NetSnifferLib.General;
using NetSnifferLib.Analysis.Transport;
using NetSnifferLib.Packets.Dhcp;

namespace NetSnifferLib.Analysis.Miscellaneous
{
    public class DhcpAnalyzer : BaseAnalyzer<DhcpDatagram, TranportContext>
    {
        public override string ProtocolString => "DHCP";

        public override string GetDatagramInfo(Datagram datagram)
        {
            return string.Empty;
        }
    }
}
