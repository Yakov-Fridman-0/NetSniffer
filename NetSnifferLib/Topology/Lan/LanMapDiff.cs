using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    public class PhysicalAddressIPAddressMapping : IEquatable<PhysicalAddressIPAddressMapping>
    {
        public IPAddress IPAddress { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }

        public bool Equals(PhysicalAddressIPAddressMapping other)
        {
            return IPAddress.Equals(other.IPAddress) && PhysicalAddress.Equals(other.PhysicalAddress);
        }
    }

    public class LanMapDiff
    {
        public List<LanHost> HostsAdded { get; set; }

        public List<LanHost> HostsRemoved { get; set; }

        public List<PhysicalAddressIPAddressMapping> PhysicalAddressIPAddressMappingModified { get; set; }

        public List<LanHost> RoutersAdded { get; set; }

        public List<LanHost> RoutersRemoved { get; set; }

        public List<LanHost> DhcpServersAdded { get; set; }

        public List<LanHost> DhcpServersRemoved { get; set; }

        //public LanMapDiff(
        //    List<LanHost> hostsAdded, List<LanHost> hostsRemoved, List<LanHost> hostsChanged,
        //    List<LanHost> routersAdded, List<LanHost> routersRemoved, List<LanHost> routersChanged,
        //    List<LanHost> dhcpServersAdded, List<LanHost> dhcpServersRemoved, List<LanHost> dhcpServersChanged)
        //{
        //    HostsAdded = hostsAdded.Select((host) => (LanHost)host.Clone()).ToList();
        //    HostsRemoved = hostsRemoved.Select((host) => (LanHost)host.Clone()).ToList();
        //    HostsModified = hostsChanged.Select((host) => (LanHost)host.Clone()).ToList();

        //    RoutersAdded = routersAdded.Select((host) => (LanHost)host.Clone()).ToList();
        //    RoutersRemoved = routersRemoved.Select((host) => (LanHost)host.Clone()).ToList();

        //    DhcpServersAdded = dhcpServersAdded.Select((host) => (LanHost)host.Clone()).ToList();
        //    DhcpServersRemoved = dhcpServersRemoved.Select((host) => (LanHost)host.Clone()).ToList();
        //}

        public bool IsEmpty =>
            HostsAdded.Count == 0 && HostsRemoved.Count == 0 &&
            PhysicalAddressIPAddressMappingModified.Count == 0 &&
            RoutersAdded.Count == 0 && RoutersRemoved.Count == 0 &&
            DhcpServersAdded.Count == 0 && DhcpServersRemoved.Count == 0;

    }
}
