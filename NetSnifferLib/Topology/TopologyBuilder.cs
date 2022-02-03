using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    class TopologyBuilder
    {
        readonly LanMapBuilder lanMapBuilder = new();
        readonly WanMapBuilder wanMapBuilder = new();

        private readonly Dictionary<List<IPAddress>, PhysicalAddress> routerDiscoveredByWanHosts = new();

        [AttributeUsage(AttributeTargets.Method)]
        public class ChecksInputAddress : Attribute
        {
            public ChecksInputAddress()
            {
                
            }
        }

        [ChecksInputAddress]
        public void AddHost(PhysicalAddress physicalAddress)
        {
            if (!PhysicalAddressHelper.IsHostAddress(physicalAddress))
                return;
            
            if (!lanMapBuilder.ContainsHost(physicalAddress))
                lanMapBuilder.AddHost(physicalAddress);
        }

        private void AddHostInWan(IPAddress ipAddress)
        {
            if (!wanMapBuilder.ContainsHost(ipAddress))
                wanMapBuilder.AddHost(ipAddress);
        }

        [ChecksInputAddress]
        public void AddHostInLan(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            if (!PhysicalAddressHelper.IsHostAddress(physicalAddress) ||
                !IPAddressHelper.IsHostAddrress(ipAddress))
                return;

            if (lanMapBuilder.ContainsHost(physicalAddress))
            {
                IPAddress otherIPAddress = lanMapBuilder.GetIPAddress(physicalAddress);

                if (otherIPAddress == null)
                {
                    lanMapBuilder.AssignIPToHost(physicalAddress, ipAddress);
                }
                else if (!otherIPAddress.Equals(ipAddress))
                {
                    lanMapBuilder.AssignIPToHost(physicalAddress, ipAddress);

                    MakeHostRouter(physicalAddress);

                    AddHostInWan(otherIPAddress);

                    routerDiscoveredByWanHosts.Add(new List<IPAddress>() { otherIPAddress }, physicalAddress);
                }
            }
            else
            {
                lanMapBuilder.AddHost(physicalAddress, ipAddress);
            }

        }

        [ChecksInputAddress]
        public void AddHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            if (!PhysicalAddressHelper.IsHostAddress(physicalAddress) ||
                !IPAddressHelper.IsHostAddrress(ipAddress))
                return;

            if(lanMapBuilder.ContainsHost(physicalAddress))
            {
                IPAddress otherIPAddress = lanMapBuilder.GetIPAddress(physicalAddress);

                if (lanMapBuilder.ContainsRouter(physicalAddress))
                {
                    // I won't claim that it is the router's IP address unless I can proove it (with ARP or DHCP)
                    if (otherIPAddress != null && !ipAddress.Equals(otherIPAddress))
                        AddHostInWan(ipAddress);
                }
                else if (otherIPAddress == null)
                {
                    lanMapBuilder.AssignIPToHost(physicalAddress, ipAddress);
                }
                else if (!otherIPAddress.Equals(ipAddress))
                {
                    lanMapBuilder.AssignIPToHost(physicalAddress, null);

                    MakeHostRouter(physicalAddress);

                    AddHostInWan(ipAddress);
                    AddHostInWan(otherIPAddress);

                    routerDiscoveredByWanHosts.Add(new List<IPAddress>() { ipAddress, otherIPAddress }, physicalAddress);
                }
            }
            else
            {
                lanMapBuilder.AddHost(physicalAddress, ipAddress);
            }
        }

        private void AddRouter(PhysicalAddress physicalAddress)
        {
            if (!lanMapBuilder.ContainsRouter(physicalAddress))
                lanMapBuilder.AddRouter(physicalAddress);
        }

        private void MakeHostRouter(PhysicalAddress physicalAddress)
        {
            if (!lanMapBuilder.ContainsRouter(physicalAddress))
                lanMapBuilder.MakeHostRouter(physicalAddress);
        }

        private void MakeHostDhcpServer(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            if (!lanMapBuilder.ContainsDhcpServer(physicalAddress, ipAddress))
                lanMapBuilder.MakeHostDhcpServer(physicalAddress, ipAddress);
        }

        //private void AddRouter(PhysicalAddress physicalAddress, IPAddress ipAddress)
        //{
        //    if (lanMapBuilder.ContainsRouter(physicalAddress))
        //    {
        //        if (!lanMapBuilder.ContainsRouter(physicalAddress, ipAddress))
        //            lanMapBuilder.AssignIPtoRouter(physicalAddress, ipAddress);
        //    }           
        //    else
        //    {
        //        lanMapBuilder.AddRouter(physicalAddress);
        //    }    
        //}

        [ChecksInputAddress]
        public void AddDhcpServer(IPAddress ipAddress)
        {
            if (!IPAddressHelper.IsHostAddrress(ipAddress))
                return;

            if (lanMapBuilder.ContainsDhcpServer(ipAddress))
                return;

            PhysicalAddress physicalAddress = null;
            //IP address is in LAN
            if (lanMapBuilder.ContainsHost(ipAddress))
            {
                physicalAddress = lanMapBuilder.GetPhysicalAddress(ipAddress);
            }          
            //IP address was added to WAN
            else
            {
                wanMapBuilder.RemoveHost(ipAddress);

                var kvp = routerDiscoveredByWanHosts.First((kvp) => kvp.Key.Contains(ipAddress));
                physicalAddress = kvp.Value;
                //routerDiscoveredByWanHosts.Remove(kvp.Key);
                //lanMapBuilder.AddDhcpServer(kvp.Value, ipAddress);
            }
            
            lanMapBuilder.MakeHostDhcpServer(physicalAddress, ipAddress);
        }

        [ChecksInputAddress]
        public void AddDnsServer(IPAddress ipAddress)
        {
            if (!IPAddressHelper.IsHostAddrress(ipAddress))
                return;

            if(!wanMapBuilder.ContainseDnsServer(ipAddress))
                wanMapBuilder.AddDnsServer(ipAddress);
        }
    }
}
