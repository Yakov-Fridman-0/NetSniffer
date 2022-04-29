using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace NetSnifferLib.Topology
{ 
    public class LanMapBuilder
    {
        readonly List<LanHost> hosts = new();

        readonly List<LanHost> routers = new();

        readonly List<LanHost> dhcpServers = new();

        public void AddHost(PhysicalAddress address)
        {
            lock (hosts)
            {
                if (!hosts.Exists(host => host.PhysicalAddress.Equals(address)))
                    hosts.Add(new LanHost(address));                       
            }
        }

        public void AddHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            lock (hosts)
            {
                if (hosts.Exists(host => host.PhysicalAddress.Equals(physicalAddress)))
                    AssignIPToHost(physicalAddress, ipAddress);
                else
                    hosts.Add(new LanHost(physicalAddress, ipAddress));
            }
        }

        public void AssignIPToHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            lock (hosts)
                hosts.Find((host) => physicalAddress.Equals(host.PhysicalAddress)).IPAddress = ipAddress;
        }

        public bool ContainsHost(PhysicalAddress physicalAddress)
        {
            lock (hosts)
                return hosts.Any((host) => physicalAddress.Equals(host.PhysicalAddress));
        }

        public bool ContainsHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            lock (hosts)
                return hosts.Any((host) => physicalAddress.Equals(host.PhysicalAddress) && ipAddress.Equals(host.IPAddress));
        }

        public bool ContainsHost(IPAddress ipAddress)
        {
            lock (hosts)
                return hosts.Any((host) => ipAddress.Equals(host.IPAddress));
        }

/*        public bool RemoveHost(PhysicalAddress physicalAddress)
        {
            lock (hosts)
                return hosts.Remove(
                    hosts.Find(
                        (host) => 
                        host.PhysicalAddress.Equals(physicalAddress)));
        }*/

/*        public bool RemoveHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            return hosts.Remove(
                hosts.Find(
                    (host) =>
                    physicalAddress.Equals(host.PhysicalAddress) &&
                    ipAddress.Equals(host.IPAddress)));
        }*/

        public IPAddress GetIPAddress(PhysicalAddress physicalAddress)
        {
            lock (hosts)
                return hosts.Find((host) => host.PhysicalAddress.Equals(physicalAddress))?.IPAddress;
        }

        public PhysicalAddress GetPhysicalAddress(IPAddress ipAddress)
        {
            lock (hosts)
                return hosts.Find((host) => ipAddress.Equals(host.IPAddress))?.PhysicalAddress;
        }

        public bool ContainsRouter(PhysicalAddress physicalAddress)
        {
            lock (routers)
                return routers.Any((router) => router.PhysicalAddress.Equals(physicalAddress));
        }

        public bool ContainsRouter(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            lock (routers)
                return routers.Any(
                    (router) =>
                    physicalAddress.Equals(router.PhysicalAddress) &&
                    ipAddress.Equals(router.IPAddress));
        }

/*        public void AddRouter(PhysicalAddress physicalAddress)
        {
            routers.Add(new Router(physicalAddress));
        }*/

/*        public void AddRouter(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            routers.Add(new Router(physicalAddress, ipAddress));
        }*/

        public void MakeHostRouter(PhysicalAddress physicalAddress)
        {
            LanHost host = null;

            lock (hosts)
                host = hosts.Find((host) => physicalAddress.Equals(host.PhysicalAddress));

            /*            Router newRouter = host.IPAddress != IPAddress.Any ? new Router(physicalAddress, host.IPAddress) : new Router(physicalAddress);

                        lock (hosts)
                        {
                            hosts.Remove(host);
                            hosts.Add(newRouter);

                            if (ContainsDhcpServer(physicalAddress, host.IPAddress))
                            {
                                dhcpServers.Remove(host);
                                dhcpServers.Add(newRouter);
                            }
                        }*/

            //routers.Add(newRouter);
            lock (routers)
                routers.Add(host);
        }

/*        public void AssignIPtoRouter(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            routers.Find((router) => physicalAddress.Equals(router.PhysicalAddress)).IPAddress = ipAddress;
        }

        public void AddDhcpServer(PhysicalAddress physicalAddress, IPAddress iPAddress)
        {
            dhcpServers.Add(new DhcpServer(physicalAddress, iPAddress));
        }*/


        public bool ContainsDhcpServer(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            if (ipAddress != null)
                return dhcpServers.Any(
                    (dhcpServer) =>
                    physicalAddress.Equals(dhcpServer.PhysicalAddress) &&
                    ipAddress.Equals(dhcpServer.IPAddress));
            else
                return false;
        }

        public void MakeHostDhcpServer(PhysicalAddress physicalAddress)
        {
            LanHost host = null;
            
            lock (hosts)
                 host = hosts.Find((h) => physicalAddress.Equals(h.PhysicalAddress));

            /*            DhcpServer newDhcpServer = new(physicalAddress, ipAddress);

                        lock (hosts)
                        {
                            hosts.Remove(host);
                            hosts.Add(newDhcpServer);

                            if (ContainsRouter(physicalAddress))
                            {
                                routers.Remove(host);
                                routers.Add(newDhcpServer);
                            }
                        }*/

            //dhcpServers.Add(newDhcpServer);
            dhcpServers.Add(host);
        }

        public bool ContainsDhcpServer(IPAddress ipAddress)
        {
            lock (dhcpServers)
                return dhcpServers.Any((dhcpServer) => ipAddress.Equals(dhcpServer.IPAddress));
        }

        internal LanMap LanMap => new(hosts, routers, dhcpServers);

        internal List<LanHost> GetOriginalLanHosts()
        {
            return hosts;
        }
    }
}
