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

        public bool ContainsHost(IPAddress ipAddress)
        {
            return hosts.Any((host) => ipAddress.Equals(host.IPAddress));
        }

        public bool RemoveHost(IPAddress ipAddress)
        {
            return hosts.Remove(hosts.Find((host) => ipAddress.Equals(host.IPAddress)));
        }

        public void AddDnsServer(IPAddress ipAddress)
        {
            dnsServers.Add(new DnsServer(ipAddress));
        }

        public bool ContainseDnsServer(IPAddress ipAddress)
        {
            return dnsServers.Any((dnsServer) => ipAddress.Equals(dnsServer.IPAddress));
        }
    }
}
