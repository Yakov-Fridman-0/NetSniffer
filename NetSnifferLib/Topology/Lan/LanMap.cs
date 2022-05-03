using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    public class LanMap //: IEquatable<LanMap>
    {
        public static LanMap Empty => new(new List<LanHost>(), new List<LanHost>(), new List<LanHost>());

        internal List<LanHost> Hosts { get; }

        public ReadOnlyCollection<LanHost> ReadOnlyHosts => Hosts.AsReadOnly();

        internal List<LanHost> Routers { get; }

        internal List<LanHost> DhcpServers { get; }

        internal LanMap(List<LanHost> hosts, List<LanHost> routers, List<LanHost> dhcpServers)
        {
            Hosts = new List<LanHost>(hosts); //hosts.Select((host) => (LanHost)host.Clone()).ToList();
            Routers = new List<LanHost>(routers); //routers.Select((router) => Hosts.Find((host) => LanHost.PhysicalAddressComparer.Equals(host, router))).ToList();
            DhcpServers = new List<LanHost>(dhcpServers); //dhcpServers.Select((dhcpServers) => Hosts.Find((host) => LanHost.PhysicalAddressComparer.Equals(host, dhcpServers))).ToList();
        }

/*        public bool Equals(LanMap other)
        {
            if (other == null)
                return false;

            return false;
        }*/

/*        public override bool Equals(object obj)
        {
            return Equals(obj as LanMap);
        }

        public override int GetHashCode()
        {
            return 
                Hosts.Aggregate(0, (int hash, LanHost host) => host.GetHashCode() ^ hash) ^ 
                Routers.Aggregate(0, (int hash, LanHost router) => router.GetHashCode() ^ hash) ^ 
                DhcpServers.Aggregate(0, (int hash, LanHost dhcpServer) => dhcpServer.GetHashCode() ^ hash);
        }*/

/*        public LanMap Except(LanMap other)
        {
            return new LanMap(
                other.Hosts.Except(Hosts).ToList(),
                other.Routers.Except(Routers).ToList(),
                other.DhcpServers.Except(DhcpServers).ToList());
        }*/

        public LanMapDiff GetDiff(LanMap previousMap)
        {
            List<LanHost> hostsAdded, hostsRemoved;
            //List<AddressMapping> addressMappingsModified;
            List<LanHost> routersAdded, routersRemoved;
            List<LanHost> dhcpServersAdded, dhcpServersRemoved;

            var prevHosts = previousMap.Hosts;
            var prevRouters = previousMap.Routers;
            var prevDhcpServers = previousMap.DhcpServers;

            /*            foreach (var host in Hosts)
                        {
                            var prevHost = prevHosts.FirstOrDefault((aPrevHost) => LanHost.PhysicalAddressComparer.Equals(host, aPrevHost));

                            if (prevHost == null)
                            {
                                hostsAdded.Add(host);
                            }
                            else
                            {
                                if (!(host.IPAddress == null && prevHost.IPAddress == null))
                                {
                                    if ((host.IPAddress == null && prevHost.IPAddress != null) ||
                                        (host.IPAddress != null) && (prevHost.IPAddress == null) ||
                                        !host.IPAddress.Equals(prevHost.IPAddress))
                                    {
                                        addressMappingsModified.Add(
                                            new AddressMapping()
                                            {
                                                PhysicalAddress = host.PhysicalAddress,
                                                IPAddress = host.IPAddress
                                            });
                                    }
                                }
                            }
                        }*/

            hostsAdded = Hosts.Except(prevHosts).ToList();
            hostsRemoved = prevHosts.Except(Hosts).ToList();

            routersAdded = Routers.Except(prevRouters).ToList();
            routersRemoved = prevRouters.Except(Routers).ToList();

            dhcpServersAdded = DhcpServers.Except(prevDhcpServers).ToList();
            dhcpServersRemoved = prevDhcpServers.Except(DhcpServers).ToList();

            return new LanMapDiff
            {
                HostsAdded = hostsAdded.AsReadOnly(),
                HostsRemoved = hostsRemoved.AsReadOnly(),

                //PhysicalAddressIPAddressMappingModified = addressMappingsModified.AsReadOnly(),

                RoutersAdded = routersAdded.AsReadOnly(),
                RoutersRemoved = routersRemoved.AsReadOnly(),

                DhcpServersAdded = dhcpServersAdded.AsReadOnly(),
                DhcpServersRemoved = dhcpServersRemoved.AsReadOnly()
            };
        }

        public Task<LanMapDiff> GetDiffAsync(LanMap previousMap)
        {
            return Task.Run(() => GetDiff(previousMap));
        }

        public Task UpdateAsync(LanMapDiff diff)
        {
            return Task.Run(() => Update(diff));
        }

        public void Update(LanMapDiff diff)
        {
            foreach (var host in diff.HostsAdded)
                Hosts.Add(host);

            foreach (var host in diff.HostsRemoved)
                Hosts.Remove(host);

            foreach (var router in diff.RoutersAdded)
                Routers.Add(router);

            foreach (var router in diff.RoutersRemoved)
                Routers.Remove(router);

            foreach (var server in diff.DhcpServersAdded)
                DhcpServers.Add(server);

            foreach (var server in diff.DhcpServersRemoved)
                DhcpServers.Remove(server);
        }

        public LanHost GetHostByPhysicalAddress(PhysicalAddress address)
        {
            lock (Hosts)
                return Hosts.FirstOrDefault(host => host.PhysicalAddress.Equals(address));
        }

        public LanHost GetHostByIPAddress(IPAddress address)
        {
            lock (Hosts)
                return Hosts.FirstOrDefault(host => host.IPAddress.Equals(address));
        }
    }
}
