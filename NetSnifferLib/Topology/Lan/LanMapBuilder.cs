using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace NetSnifferLib.Topology
{
    public class LanMapBuilder
    {
        private readonly List<LanHost> _hostsWithoutIP = new();

        private readonly List<LanHost> _hostsWithIP = new();

        private readonly List<Router> _routers = new();

        private readonly List<DhcpServer> _dhcpServers = new();

        private static readonly PhysicalAddress MulticastAddress;
        
        static LanMapBuilder()
        {
            MulticastAddress = PhysicalAddress.Parse("FF:FF:FF:FF:FF:FF");
        }

        public void AddHostWithoutIP(PhysicalAddress physicalAddress)
        {
            if (physicalAddress.Equals(MulticastAddress))
                return;

            if (!ContainsHostWithoutIP(physicalAddress) && !ContainsHostWithIP(physicalAddress) &&
                !ContainsRouter(physicalAddress) && !ContainsDhcpServer(physicalAddress))
            {
                _hostsWithoutIP.Add(new LanHost(physicalAddress));
            }           
        }

        public void AddHostWithIP(PhysicalAddress physicalAddress, IPAddress iPAddress)
        {
            if (physicalAddress.Equals(MulticastAddress))
                return;

            if (ContainsDhcpServer(physicalAddress))
                return;

            if (ContainsHostWithoutIP(physicalAddress))
            {
                RemoveHostWithoutIP(physicalAddress);
            }

            if(ContainsHostWithIP(physicalAddress))
            {
                if (!GetHostIPAddress(physicalAddress).Equals(iPAddress))
                {
                    RemoveHostWithIP(physicalAddress);
                    AddRouter(physicalAddress);
                }
            }
            else
            {
                _hostsWithIP.Add(new LanHost(physicalAddress, iPAddress));
            }
        }

        private bool RemoveHostWithoutIP(PhysicalAddress physicalAddress)
        {
            return _hostsWithoutIP.Remove(
                _hostsWithoutIP.Find(
                    (host) => 
                    host.PhysicalAddress.Equals(physicalAddress)));
        }

        private bool RemoveHostWithIP(PhysicalAddress physicalAddress)
        {
            return _hostsWithIP.Remove(
                _hostsWithIP.Find(
                    (host) => 
                    host.PhysicalAddress.Equals(physicalAddress)));
        }

        private bool ContainsHostWithoutIP(PhysicalAddress physicalAddress)
        {
            return _hostsWithoutIP.Any((host) => host.PhysicalAddress.Equals(physicalAddress));
        }

        private bool ContainsHostWithIP(PhysicalAddress physicalAddress)
        {
            return _hostsWithIP.Any((host) => host.PhysicalAddress.Equals(physicalAddress));
        }

        private IPAddress GetHostIPAddress(PhysicalAddress physicalAddress)
        {
            return _hostsWithIP.Find((host) => host.PhysicalAddress.Equals(physicalAddress))?.IpAddress;
        }

        public bool ContainsRouter(PhysicalAddress physicalAddress)
        {
            return _routers.Any((router) => router.PhysicalAddress.Equals(physicalAddress));
        }

        private void AddRouter(PhysicalAddress physicalAddress)
        {
            if(!ContainsRouter(physicalAddress))
                _routers.Add(new Router(physicalAddress));
        }

        public bool ContainsDhcpServer(PhysicalAddress physicalAddress)
        {
            return _dhcpServers.Any((dhcpServer) => dhcpServer.PhysicalAddress.Equals(physicalAddress));
        }

        public void AddDhcpServer(PhysicalAddress physicalAddress, IPAddress iPAddress)
        {
            _dhcpServers.Add(new DhcpServer(physicalAddress, iPAddress));
        }

        public LanMap LanMap => new(_hostsWithIP.Concat(_hostsWithoutIP).ToList(), _routers, _dhcpServers);
    }
}
