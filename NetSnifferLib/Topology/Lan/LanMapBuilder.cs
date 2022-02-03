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

        public void AddHost(PhysicalAddress physicalAddress)
        {
            hosts.Add(new LanHost(physicalAddress));                       
        }

        public void AddHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            hosts.Add(new LanHost(physicalAddress, ipAddress));
        }

        public void AssignIPToHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            hosts.Find((host) => physicalAddress.Equals(host.PhysicalAddress)).IPAddress = ipAddress;
        }

        public bool ContainsHost(PhysicalAddress physicalAddress)
        {
            return hosts.Any((host) => physicalAddress.Equals(host.PhysicalAddress));
        }

        public bool ContainsHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            return hosts.Any((host) => 
            physicalAddress.Equals(host.PhysicalAddress) && 
            ipAddress.Equals(host.IPAddress));
        }

        public bool ContainsHost(IPAddress ipAddress)
        {
            return hosts.Any((host) => ipAddress.Equals(host.IPAddress));
        }

        public bool RemoveHost(PhysicalAddress physicalAddress)
        {
            return hosts.Remove(
                hosts.Find(
                    (host) => 
                    host.PhysicalAddress.Equals(physicalAddress)));
        }

        public bool RemoveHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            return hosts.Remove(
                hosts.Find(
                    (host) =>
                    physicalAddress.Equals(host.PhysicalAddress) &&
                    ipAddress.Equals(host.IPAddress)));
        }

        public IPAddress GetIPAddress(PhysicalAddress physicalAddress)
        {
            return hosts.Find((host) => host.PhysicalAddress.Equals(physicalAddress))?.IPAddress;
        }

        public PhysicalAddress GetPhysicalAddress(IPAddress ipAddress)
        {
            return hosts.Find((host) => ipAddress.Equals(host.IPAddress))?.PhysicalAddress;
        }

        public bool ContainsRouter(PhysicalAddress physicalAddress)
        {
            return routers.Any((router) => router.PhysicalAddress.Equals(physicalAddress));
        }

        public bool ContainsRouter(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            return routers.Any(
                (router) =>
                physicalAddress.Equals(router.PhysicalAddress) &&
                ipAddress.Equals(router.IPAddress));
        }

        public void AddRouter(PhysicalAddress physicalAddress)
        {
            routers.Add(new Router(physicalAddress));
        }

        public void AddRouter(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            routers.Add(new Router(physicalAddress, ipAddress));
        }

        public void MakeHostRouter(PhysicalAddress physicalAddress)
        {
            var host = hosts.Find((host) => physicalAddress.Equals(host.PhysicalAddress));

            Router newRouter = host.IPAddress != null ? new Router(physicalAddress, host.IPAddress) : new Router(physicalAddress);
            
            hosts.Remove(host);
            hosts.Add(newRouter);

            if (ContainsDhcpServer(physicalAddress, host.IPAddress))
            {
                dhcpServers.Remove(host);
                dhcpServers.Add(newRouter);
            }

            routers.Add(newRouter);
        }

        public void AssignIPtoRouter(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            routers.Find((router) => physicalAddress.Equals(router.PhysicalAddress)).IPAddress = ipAddress;
        }

        public void AddDhcpServer(PhysicalAddress physicalAddress, IPAddress iPAddress)
        {
            dhcpServers.Add(new DhcpServer(physicalAddress, iPAddress));
        }


        public bool ContainsDhcpServer(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            return dhcpServers.Any(
                (dhcpServer) =>
                physicalAddress.Equals(dhcpServer.PhysicalAddress) &&
                ipAddress.Equals(dhcpServer.IPAddress));
        }

        public void MakeHostDhcpServer(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            var host = hosts.Find((host) => physicalAddress.Equals(host.PhysicalAddress));

            DhcpServer newDhcpServer = new(physicalAddress, ipAddress);

            hosts.Remove(host);
            hosts.Add(newDhcpServer);

            if (ContainsRouter(physicalAddress))
            {
                routers.Remove(host);
                routers.Add(newDhcpServer);
            }

            dhcpServers.Add(newDhcpServer);
        }

        public bool ContainsDhcpServer(IPAddress ipAddress)
        {
            return dhcpServers.Any((dhcpServer) => ipAddress.Equals(dhcpServer.IPAddress));
        }

        //public LanMap LanMap => new(hosts, routers, dhcpServers);
    }
}
