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
    public class LanMap : IEquatable<LanMap>
    {
        public static LanMap Empty => new(new List<LanHost>(), new List<LanHost>(), new List<LanHost>());

        public List<LanHost> Hosts { get; }

        public List<LanHost> Routers { get; }

        public List<LanHost> DhcpServers { get; }

        public LanMap(List<LanHost> hosts, List<LanHost> routers, List<LanHost> dhcpServers)
        {
            Hosts = hosts.Select((host) => (LanHost)host.Clone()).ToList();
            Routers = routers.Select((router) => Hosts.Find((host) => LanHost.PhysicalAddressComparer.Equals(host, router))).ToList();
            DhcpServers = dhcpServers.Select((dhcpServers) => Hosts.Find((host) => LanHost.PhysicalAddressComparer.Equals(host, dhcpServers))).ToList();
        }

        public bool Equals(LanMap other)
        {
            if (other == null)
                return false;

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LanMap);
        }

        public override int GetHashCode()
        {
            return 
                Hosts.Aggregate(0, (int hash, LanHost host) => host.GetHashCode() ^ hash) ^ 
                Routers.Aggregate(0, (int hash, LanHost router) => router.GetHashCode() ^ hash) ^ 
                DhcpServers.Aggregate(0, (int hash, LanHost dhcpServer) => dhcpServer.GetHashCode() ^ hash);
        }

        public LanMap Except(LanMap other)
        {
            return new LanMap(
                other.Hosts.Except(Hosts).ToList(),
                other.Routers.Except(Routers).ToList(),
                other.DhcpServers.Except(DhcpServers).ToList());
        }

        public LanMapDiff GetDiff(LanMap previousMap)
        {
            List<LanHost> hostsAdded = new(), hostsRemoved;
            List<PhysicalAddressIPAddressMapping> addressMappingsModified = new();
            List<LanHost> routersAdded, routersRemoved;
            List<LanHost> dhcpServersAdded, dhcpServersRemoved;

            var prevHosts = previousMap.Hosts;
            var prevRouters = previousMap.Routers;
            var prevDhcpServers = previousMap.DhcpServers;

            foreach (var host in Hosts)
            {
                var prevHost = prevHosts.FirstOrDefault((aPrevHost) => LanHost.PhysicalAddressComparer.Equals(host, aPrevHost));
                
                if (prevHost == null)
                {
                    hostsAdded.Add(host);
                }
                else
                {
                    if (host.IPAddress?.Equals(prevHost.IPAddress) ?? (prevHost.IPAddress == null) )
                    {
                        addressMappingsModified.Add(
                            new PhysicalAddressIPAddressMapping()
                            {
                                PhysicalAddress = host.PhysicalAddress,
                                IPAddress = host.IPAddress
                            }) ;
                    }
                }
            }

            hostsRemoved = prevHosts.Except(Hosts, LanHost.PhysicalAddressComparer).ToList();

            routersAdded = Routers.Except(prevRouters, LanHost.PhysicalAddressComparer).ToList();
            routersRemoved = prevRouters.Except(Routers, LanHost.PhysicalAddressComparer).ToList();

            dhcpServersAdded = DhcpServers.Except(prevDhcpServers, LanHost.PhysicalAddressComparer).ToList();
            dhcpServersRemoved = prevDhcpServers.Except(DhcpServers, LanHost.PhysicalAddressComparer).ToList();

            return new LanMapDiff
            {
                HostsAdded = hostsAdded,
                HostsRemoved = hostsRemoved,

                PhysicalAddressIPAddressMappingModified = addressMappingsModified,

                RoutersAdded = routersAdded,
                RoutersRemoved = routersRemoved,

                DhcpServersAdded = dhcpServersAdded,
                DhcpServersRemoved = dhcpServersRemoved
            };

            //foreach (LanHost host in Hosts)
            //{
            //    PhysicalAddress physicalAdd = host.PhysicalAddress;
            //    IPAddress ipAdd = host.IPAddress;
            //    LanHost prevHost = previousMap.Hosts.FirstOrDefault((aHost) => aHost.PhysicalAddress.Equals(physicalAdd));

            //    if (prevHost != null)
            //    {
            //        if(prevHost.IPAddress == null)
            //        {
            //            if (host.IPAddress != null)
            //                hostsChanged.Add(host);
            //        }
            //        else
            //        {
            //            if (!prevHost.IPAddress.Equals(ipAdd))
            //                hostsChanged.Add(host);
            //        }
            //    }
            //    else
            //    {
            //        hostsAdded.Add(host);
            //    }
            //}

            //foreach (LanHost router in Routers)
            //{
            //    PhysicalAddress physicalAdd = router.PhysicalAddress;
            //    IPAddress ipAdd = router.IPAddress;
            //    LanHost prevRouter = previousMap.Routers.FirstOrDefault((aRouter) => aRouter.PhysicalAddress.Equals(physicalAdd));

            //    if (prevRouter != null)
            //    {
            //        if (prevRouter.IPAddress == null)
            //        {
            //            if (router.IPAddress != null)
            //                routersChanged.Add(router);
            //        }
            //        else
            //        {
            //            if (!prevRouter.IPAddress.Equals(ipAdd))
            //                routersChanged.Add(router);
            //        }
            //    }
            //    else
            //    {
            //        routersAdded.Add(router);
            //    }
            //}

            //foreach (LanHost server in DhcpServers)
            //{
            //    PhysicalAddress physicalAdd = server.PhysicalAddress;
            //    IPAddress ipAdd = server.IPAddress;
            //    LanHost prevServer = previousMap.DhcpServers.FirstOrDefault((aServer) => aServer.PhysicalAddress.Equals(physicalAdd));

            //    if (prevServer != null)
            //    {
            //        if (prevServer.IPAddress == null)
            //        {
            //            if (server.IPAddress != null)
            //                dhcpServersChanged.Add(server);
            //        }
            //        else
            //        {
            //            if (!prevServer.IPAddress.Equals(ipAdd))
            //                dhcpServersChanged.Add(server);
            //        }
            //    }
            //    else
            //    {
            //        dhcpServersAdded.Add(server);
            //    }
            //}

            //foreach (LanHost prevHost in previousMap.Hosts)
            //{
            //    PhysicalAddress physicalAdd = prevHost.PhysicalAddress;

            //    if(!Hosts.Any((host) => host.PhysicalAddress.Equals(physicalAdd)))
            //    {
            //        hostsRemoved.Add(prevHost);
            //    }
            //}

            //foreach (LanHost prevRouter in previousMap.Routers)
            //{
            //    PhysicalAddress physicalAdd = prevRouter.PhysicalAddress;

            //    if (!Routers.Any((router) => router.PhysicalAddress.Equals(physicalAdd)))
            //    {
            //        hostsRemoved.Add(prevRouter);
            //    }
            //}

            //foreach (LanHost prevServer in previousMap.DhcpServers)
            //{
            //    PhysicalAddress physicalAdd = prevServer.PhysicalAddress;

            //    if (!DhcpServers.Any((server) => server.PhysicalAddress.Equals(physicalAdd)))
            //    {
            //        hostsRemoved.Add(prevServer);
            //    }
            //}
        }
    }
}
