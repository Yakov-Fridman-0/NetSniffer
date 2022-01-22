using System.Net;

namespace NetSnifferLib.Topology
{
    public class DnsServer : WanHost
    {
        public DnsServer(IPAddress iPAddress) : base(iPAddress)
        {

        }

        public DnsServer(IPAddress iPAddress, int hops) : base(iPAddress, hops)
        {

        }
    }
}
