using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetSnifferLib.Topology
{
    public class WanConnection : IEquatable<WanConnection>
    {
        public IPAddress Address1 { get; }

        public IPAddress Address2 { get; }

        public WanConnection(IPAddress address1, IPAddress address2)
        {
            Address1 = address1;
            Address2 = address2;
        }

        public bool Equals(WanConnection other)
        {
            return
                (Address1.Equals(other.Address1) && Address2.Equals(other.Address2)) ||
                (Address1.Equals(other.Address2) && Address1.Equals(other.Address2));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as WanConnection);
        }

        public override int GetHashCode()
        {
            return Address1.GetHashCode() ^ Address2.GetHashCode();
        }
    }

    public class WanMap
    {
        public static WanMap Empty => new(new List<WanHost>(), new List<WanHost>(), new List<WanHost>(), new List<WanHost>());

        internal List<WanHost> Hosts { get; } = new();

        public ReadOnlyCollection<WanHost> HostsAsReadOnly => Hosts.AsReadOnly();

        internal List<WanHost> LanRouters { get; } = new();

        internal List<WanHost> WanRouters { get; } = new();

        internal List<WanHost> DnsServers { get; } = new();

        public WanHost GetHostByIPAddress(IPAddress address)
        {
            lock (Hosts)
                return Hosts.FirstOrDefault(host => host.IPAddress.Equals(address));
        }

        public WanMap(List<WanHost> hosts, List<WanHost> lanRouters, List<WanHost> wanRouters, List<WanHost> dnsServers)
        {
            Hosts = new List<WanHost>(hosts); //hosts.Select((host) => (WanHost)host.Clone()).ToList();

            LanRouters = new List<WanHost>(lanRouters); //lanRouters.Select((router) => hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList();

            WanRouters = new List<WanHost>(wanRouters); //wanRouters.Select((router) => hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList();

            DnsServers = new List<WanHost>(dnsServers); //dnsServers.Select((server) => hosts.Find((host) => host.IPAddress.Equals(server.IPAddress))).ToList();
        }

        public WanMapDiff GetDiff(WanMap previousMap)
        {
            List<WanHost> hostsAdded, hostsRemoved;
            //List<WanConnection> connectionsAdded = new(), connectionsRemoved = new();
            List<WanHost> lanRoutersAdded, lanRoutersRemoved;
            List<WanHost> wanRoutersAdded, wanRoutersRemoved;
            List<WanHost> dnsServersAdded, dnsServersRemoved;

            /*            foreach (var host in Hosts)
                        {
                            var hostAddr = host.IPAddress;
                            var prevHost = previousMap.Hosts.FirstOrDefault((aHost) => hostAddr.Equals(aHost.IPAddress));

                            if (prevHost == null)
                            {
                                hostsAdded.Add(host);
                            }
                            else
                            {
                                var connectedAddrs = host.ConnectedHosts.Select((connectedHost) => connectedHost.IPAddress);
                                var prevConnectedAddrs = prevHost.ConnectedHosts.Select((connectedHost) => connectedHost.IPAddress);

                                var allAddedConnsToHost = connectedAddrs.Except(
                                    prevConnectedAddrs, IPAddressHelper.EqulityComparer).Select(
                                        (addr) => new WanConnection(hostAddr, addr));

                                var addedConnsNotRegisteredYet = allAddedConnsToHost.Where((conn) => !connectionsAdded.Contains(conn));
                                connectionsAdded.AddRange(addedConnsNotRegisteredYet);

                                var allRemovedConnsFromHost = connectedAddrs.Except(
                                    prevConnectedAddrs, IPAddressHelper.EqulityComparer).Select(
                                        (addr) => new WanConnection(hostAddr, addr));

                                var removedConnsNotRegisteredYet = allRemovedConnsFromHost.Where((conn) => !connectionsRemoved.Contains(conn));
                                connectionsRemoved.AddRange(removedConnsNotRegisteredYet);
                            }
                        }*/

            hostsAdded = Hosts.Except(previousMap.Hosts).ToList();
            hostsRemoved = previousMap.Hosts.Except(Hosts/*, WanHost.IPAddressComparer*/).ToList();

            lanRoutersAdded = LanRouters.Except(previousMap.LanRouters/*, WanHost.IPAddressComparer */).ToList();
            lanRoutersRemoved = previousMap.LanRouters.Except(LanRouters/*, WanHost.IPAddressComparer */).ToList();

            wanRoutersAdded = WanRouters.Except(previousMap.WanRouters/*, WanHost.IPAddressComparer */).ToList();
            wanRoutersRemoved = previousMap.WanRouters.Except(WanRouters/*, WanHost.IPAddressComparer */).ToList();

            dnsServersAdded = DnsServers.Except(previousMap.DnsServers/*, WanHost.IPAddressComparer */).ToList();
            dnsServersRemoved = previousMap.DnsServers.Except(DnsServers/*, WanHost.IPAddressComparer */).ToList();

            return new WanMapDiff
            {
                HostsAdded = hostsAdded.AsReadOnly(), //.Select((host) => (WanHost)host.Clone()).ToList(),
                HostRemoved = hostsRemoved.AsReadOnly(), //.Select((host) => (WanHost)host.Clone()).ToList(),

                //ConnectionsAdded = connectionsAdded.AsReadOnly(),
                //ConnectionsRemoved = connectionsRemoved.AsReadOnly(),

                LanRouterAdded = lanRoutersAdded.AsReadOnly(), //.Select((router) => Hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList(),
                LanRouterRemoved = lanRoutersRemoved.AsReadOnly(), //.Select((router) => previousMap.Hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList(),

                WanRoutersAdded = wanRoutersAdded.AsReadOnly(), //.Select((router) => Hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList(),
                WanRouterRemoved = wanRoutersRemoved.AsReadOnly(), //.Select((router) => previousMap.Hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList(),

                DnsServersAdded = dnsServersAdded.AsReadOnly(), //.Where((server) => server != null).Select((server) => Hosts.Find((host) => host.IPAddress.Equals(server.IPAddress))).Where((server) => server != null).ToList(),
                DnsServersRemoved = dnsServersRemoved.AsReadOnly() //.Select((server) => previousMap.Hosts.Find((host) => host.IPAddress.Equals(server.IPAddress))).ToList()
            };
        }

        public Task<WanMapDiff> GetDiffAsync(WanMap previousMap)
        {
            return Task.Run(() => GetDiff(previousMap));
        }

        public void Update(WanMapDiff mapDiff)
        {
            Hosts.AddRange(mapDiff.HostsAdded);
            //Hosts.Remove(mapDiff.HostRemoved);

            LanRouters.AddRange(mapDiff.LanRouterAdded);

            WanRouters.AddRange(mapDiff.WanRoutersAdded);

            DnsServers.AddRange(mapDiff.DnsServersAdded);
        }
    }
}
