using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace NetSnifferLib.Topology
{ 
    public class LanMapBuilder
    {
        readonly List<LanHost> hostsWithoutIP = new();

        readonly List<LanHost> hostsWithIP = new();

        readonly List<Router> routers = new();

        readonly List<DhcpServer> dhcpServers = new();

        static readonly PhysicalAddress MulticastAddress;

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
            return PhysicalAddressHelper.GetOUI(address).IsMulticast();
        }

        public void AddHost(PhysicalAddress physicalAddress)
        {
            if (IsBroadcast(physicalAddress) || IsMulticast(physicalAddress))
                return;

            if (!ContainsHostWithoutIP(physicalAddress) && !ContainsHostWithIP(physicalAddress))
            {
                hostsWithoutIP.Add(new LanHost(physicalAddress));
            }           
        }

        public void AddHost(PhysicalAddress physicalAddress, IPAddress ipAddress, ref bool? isRemote, ref IPAddress remoteIPAddress)
        {
            if (IsBroadcast(physicalAddress) || IsMulticast(physicalAddress))
                return;

            if (ContainsHostWithoutIP(physicalAddress))
            {
                RemoveHostsWithoutIP(physicalAddress);
            }

            IPAddress otherIPAddress = null;
            if (TryGetHostIP(physicalAddress, ref otherIPAddress))
            {
                if (otherIPAddress != null)
                {
                    if (!ipAddress.Equals(otherIPAddress))
                    {
                        RemoveHostsWithIP(otherIPAddress);
                        isRemote = true;
                        remoteIPAddress = otherIPAddress;
                        AddRouter(physicalAddress);
                    }
                }
            }
            else
            {
                hostsWithIP.Add(new LanHost(physicalAddress, ipAddress));
            }
        }

        private bool RemoveHostsWithoutIP(PhysicalAddress physicalAddress)
        {
            return hostsWithoutIP.Remove(
                hostsWithoutIP.Find(
                    (host) => 
                    host.PhysicalAddress.Equals(physicalAddress)));
        }

        private bool RemoveHostsWithIP(PhysicalAddress physicalAddress)
        {
            return hostsWithIP.Remove(
                hostsWithIP.Find(
                    (host) => 
                    host.PhysicalAddress.Equals(physicalAddress)));
        }

        private bool RemoveHostsWithIP(IPAddress ipAddress)
        {
            return hostsWithIP.Remove(
                hostsWithIP.Find(
                    (host) =>
                    host.IPAddress.Equals(ipAddress)));
        }

        private bool ContainsHostWithoutIP(PhysicalAddress physicalAddress)
        {
            return hostsWithoutIP.Any((host) => host.PhysicalAddress.Equals(physicalAddress));
        }

        private bool ContainsHostWithIP(PhysicalAddress physicalAddress)
        {
            return hostsWithIP.Any((host) => host.PhysicalAddress.Equals(physicalAddress));
        }

        private bool TryGetHostIP(PhysicalAddress physicalAddress, ref IPAddress ipAddress)
        {
            var tempAdd = hostsWithIP.Find((host) => host.PhysicalAddress.Equals(physicalAddress))?.IPAddresses?[0];

            if (tempAdd != null)
            {
                ipAddress = tempAdd;
                return true;
            }

            return false;
        }

        //private IPAddress GetHostIPAddress(PhysicalAddress physicalAddress)
        //{
        //    return _hostsWithIP.Find((host) => host.PhysicalAddress.Equals(physicalAddress))?.IpAddress;
        //}

        public bool ContainsRouter(PhysicalAddress physicalAddress)
        {
            return routers.Any((router) => router.PhysicalAddress.Equals(physicalAddress));
        }

        public void AddRouter(PhysicalAddress physicalAddress)
        {
            if (IsBroadcast(physicalAddress) || IsMulticast(physicalAddress))
                return;

            if (!ContainsRouter(physicalAddress))
                routers.Add(new Router(physicalAddress));
        }

        public void AddRouter(IPAddress ipAddress, PhysicalAddress physicalAddress)
        {
            if (IsBroadcast(physicalAddress) || IsMulticast(physicalAddress))
                return;

            if (!ContainsRouter(physicalAddress))
                routers.Add(new Router(physicalAddress));
        }

        public bool ContainsDhcpServer(PhysicalAddress physicalAddress)
        {
            return dhcpServers.Any((dhcpServer) => dhcpServer.PhysicalAddress.Equals(physicalAddress));
        }

        public void AddDhcpServer(PhysicalAddress physicalAddress, IPAddress iPAddress)
        {
            dhcpServers.Add(new DhcpServer(physicalAddress, iPAddress));
        }

        public LanMap LanMap => new(hostsWithIP.Concat(hostsWithoutIP).ToList(), routers, dhcpServers);
    }
}
