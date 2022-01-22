using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetSnifferLib.Topology
{
    public class WanMap
    {
        private readonly List<WanHost> _hosts = new();

        private readonly List<DnsServer> _dnsServers = new();

        public void AddHost(WanHost _hosts)
        {

        }

        public ReadOnlyCollection<WanHost> Hosts => _hosts.AsReadOnly();
    }
}
