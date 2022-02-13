using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    public class WanMapDiff
    {
        public List<WanHost> HostsAdded { get; set; } 

        public List<WanHost> HostRemoved { get; set; }

        public List<WanConnection> ConnectionsAdded { get; set; }

        public List<WanConnection> ConnectionsRemoved { get; set; }

        public List<WanHost> LanRouterAdded { get; set; }

        public List<WanHost> LanRouterRemoved { get; set; }

        public List<WanHost> WanRouterAdded { get; set; }

        public List<WanHost> WanRouterRemoved { get; set; }

        public List<WanHost> DnsServersAdded { get; set; }

        public List<WanHost> DnsServersRemoved { get; set; }
    }
}
