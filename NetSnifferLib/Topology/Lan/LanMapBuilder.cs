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

        private static bool IsBroadcast(PhysicalAddress address)
        {
            return MulticastAddress.Equals(address);
        }

        private static bool IsMulticast(PhysicalAddress address)
        {
            return IsIPv6Multicast(address);
            //return IsIPV4Multicast(address) || IsIPv6Multicast(address);
        }

        private static bool IsIPV4Multicast(PhysicalAddress address)
        {
            byte[] bytes = address.GetAddressBytes();
            return bytes[3] < 0xF7;
        }

        private static bool IsIPv6Multicast(PhysicalAddress address)
        {
            byte[] bytes = address.GetAddressBytes();
            return bytes[0] == 0x33 && bytes[1] == 0x33; 
        }

        public void AddHost(PhysicalAddress physicalAddress)
        {
            if (IsBroadcast(physicalAddress) || IsMulticast(physicalAddress))
                return;

            if (!ContainsHostWithoutIP(physicalAddress) && !ContainsHostWithIP(physicalAddress))
            {
                _hostsWithoutIP.Add(new LanHost(physicalAddress));
            }           
        }

        public void AddHost(IPAddress iPAddress, PhysicalAddress physicalAddress)
        {
            if (IsBroadcast(physicalAddress) || IsMulticast(physicalAddress))
                return;

            if (!ContainsHostWithIP(physicalAddress))
            {
                if (ContainsHostWithoutIP(physicalAddress))
                {
                    RemoveHostWithoutIP(physicalAddress);
                }

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

        //private IPAddress GetHostIPAddress(PhysicalAddress physicalAddress)
        //{
        //    return _hostsWithIP.Find((host) => host.PhysicalAddress.Equals(physicalAddress))?.IpAddress;
        //}

        public bool ContainsRouter(PhysicalAddress physicalAddress)
        {
            return _routers.Any((router) => router.PhysicalAddress.Equals(physicalAddress));
        }

        public void AddRouter(PhysicalAddress physicalAddress)
        {
            if (IsBroadcast(physicalAddress) || IsMulticast(physicalAddress))
                return;

            if (!ContainsRouter(physicalAddress))
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
