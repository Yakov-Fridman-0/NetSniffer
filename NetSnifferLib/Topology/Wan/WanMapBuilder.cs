using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    class WanMapBuilder
    {
        readonly List<WanHost> hosts = new();

        readonly List<DnsServer> dnsServers = new();

        public void AddHost(IPAddress ipAddress)
        {
            hosts.Add(new WanHost(ipAddress));
        }

        public void AddDNSServer(IPAddress ipAddress)
        {
            dnsServers.Add(new DnsServer(ipAddress));
        }
    }
}
