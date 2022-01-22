using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    public class LanMap
    {
        public ReadOnlyCollection<LanHost> Hosts { get; }

        public ReadOnlyCollection<Router> Routers { get; }

        public ReadOnlyCollection<DhcpServer> DhcpServers { get; }

        public LanMap(List<LanHost> hosts, List<Router> routers, List<DhcpServer> dhcpServers)
        {
            Hosts = hosts.AsReadOnly();
            Routers = routers.AsReadOnly();
            DhcpServers = dhcpServers.AsReadOnly();
        }
    }
}
