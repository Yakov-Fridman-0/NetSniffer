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
    public class AddressMapping : IEquatable<AddressMapping>
    {
        public IPAddress IPAddress { get; init; }
        public PhysicalAddress PhysicalAddress { get; init; }

        public bool Equals(AddressMapping other)
        {
            return IPAddress.Equals(other.IPAddress) && PhysicalAddress.Equals(other.PhysicalAddress);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AddressMapping);
        }

        public override int GetHashCode()
        {
            return IPAddress.GetHashCode() ^ PhysicalAddress.GetHashCode();
        }
    }

    public class LanMapDiff
    {
        public ReadOnlyCollection<LanHost> HostsAdded { get; init; }

        public ReadOnlyCollection<LanHost> HostsRemoved { get; init; }

        //public ReadOnlyCollection<AddressMapping> PhysicalAddressIPAddressMappingModified { get; init; }

        public ReadOnlyCollection<LanHost> RoutersAdded { get; init; }

        public ReadOnlyCollection<LanHost> RoutersRemoved { get; init; }

        public ReadOnlyCollection<LanHost> DhcpServersAdded { get; init; }

        public ReadOnlyCollection<LanHost> DhcpServersRemoved { get; init; }

        public bool IsEmpty =>
            HostsAdded.Count == 0 && HostsRemoved.Count == 0 &&
            //PhysicalAddressIPAddressMappingModified.Count == 0 &&
            RoutersAdded.Count == 0 && RoutersRemoved.Count == 0 &&
            DhcpServersAdded.Count == 0 && DhcpServersRemoved.Count == 0;
    }
}
