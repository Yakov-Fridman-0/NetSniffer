using System;
using System.Net;
using System.Linq;
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

        //public WanHost LocalComputer { get; }

        public List<WanHost> Hosts { get; } = new();

        public List<WanHost> LanRouters { get; } = new();

        public List<WanHost> WanRouters { get; } = new();

        public List<WanHost> DnsServers { get; } = new();

        public WanMap(List<WanHost> hosts, List<WanHost> lanRouters, List<WanHost> wanRouters, List<WanHost> dnsServers)
        {
            //LocalComputer = localComputer;

            //Hosts = hosts;
            Hosts = hosts.Select((host) => (WanHost)host.Clone()).ToList();

            //LanRouters = lanRouters;
            LanRouters = lanRouters.Select((router) => hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList();

            //WanRouters = wanRouters;
            WanRouters = wanRouters.Select((router) => hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList();

            //DnsServers = dnsServers;
            DnsServers = dnsServers.Select((server) => hosts.Find((host) => host.IPAddress.Equals(server.IPAddress))).ToList();
        }

        public WanMapDiff GetDiff(WanMap previousMap)
        {
            List<WanHost> hostsAdded = new(), hostsRemoved;
            List<WanConnection> connectionsAdded = new(), connectionsRemoved = new();
            List<WanHost> lanRoutersAdded, lanRoutersRemoved;
            List<WanHost> wanRoutersAdded, wanRoutersRemoved;
            List<WanHost> dnsServersAdded, dnsServersRemoved;

            foreach (var host in Hosts)
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
            }

            hostsRemoved = previousMap.Hosts.Except(Hosts, WanHost.IPAddressComparer).ToList();

            lanRoutersAdded = LanRouters.Except(previousMap.LanRouters, WanHost.IPAddressComparer).ToList();
            lanRoutersRemoved = previousMap.LanRouters.Except(LanRouters, WanHost.IPAddressComparer).ToList();

            wanRoutersAdded = WanRouters.Except(previousMap.WanRouters, WanHost.IPAddressComparer).ToList();
            wanRoutersRemoved = previousMap.WanRouters.Except(WanRouters, WanHost.IPAddressComparer).ToList();

            dnsServersAdded = DnsServers.Except(previousMap.DnsServers, WanHost.IPAddressComparer).ToList();
            dnsServersRemoved = previousMap.DnsServers.Except(DnsServers, WanHost.IPAddressComparer).ToList();

            return new WanMapDiff
            {
                HostsAdded = hostsAdded.Select((host) => (WanHost)host.Clone()).ToList(),
                HostRemoved = hostsRemoved.Select((host) => (WanHost)host.Clone()).ToList(),

                ConnectionsAdded = connectionsAdded,
                ConnectionsRemoved = connectionsRemoved,

                LanRouterAdded = lanRoutersAdded.Select((router) => Hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList(),
                LanRouterRemoved = lanRoutersRemoved.Select((router) => previousMap.Hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList(),

                WanRouterAdded = wanRoutersAdded.Select((router) => Hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList(),
                WanRouterRemoved = wanRoutersRemoved.Select((router) => previousMap.Hosts.Find((host) => host.IPAddress.Equals(router.IPAddress))).ToList(),

                DnsServersAdded = dnsServersAdded.Where(
                    (server) => server != null).Select((server) => Hosts.Find((host) => host.IPAddress.Equals(server.IPAddress))).Where((server) => server != null).ToList(),
                DnsServersRemoved = dnsServersRemoved.Select((server) =>
                previousMap.Hosts.Find((host) => host.IPAddress.Equals(server.IPAddress))).ToList()
            };
        }
    }
}
