using System;
using System.Net;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    public class WanMapDiff
    {
        public ReadOnlyCollection<WanHost> HostsAdded { get; init; }

        public ReadOnlyCollection<WanHost> HostRemoved { get; init; }

        //public ReadOnlyCollection<WanConnection> ConnectionsAdded { get; init; }

        //public ReadOnlyCollection<WanConnection> ConnectionsRemoved { get; init; }

        public ReadOnlyCollection<WanHost> LanRouterAdded { get; init; }

        public ReadOnlyCollection<WanHost> LanRouterRemoved { get; init; }

        public ReadOnlyCollection<WanHost> WanRoutersAdded { get; init; }

        public ReadOnlyCollection<WanHost> WanRouterRemoved { get; init; }

        public ReadOnlyCollection<WanHost> DnsServersAdded { get; init; }

        public ReadOnlyCollection<WanHost> DnsServersRemoved { get; init; }

        public bool IsEmpty => 
            HostsAdded.Count == 0 && HostRemoved.Count == 0 && 
            //ConnectionsAdded.Count == 0 && ConnectionsRemoved.Count == 0 &&
            LanRouterAdded.Count == 0 && LanRouterRemoved.Count == 0 &&
            WanRoutersAdded.Count == 0 && WanRouterRemoved.Count == 0 &&
            DnsServersAdded.Count == 0 && DnsServersRemoved.Count == 0;
    }
}
