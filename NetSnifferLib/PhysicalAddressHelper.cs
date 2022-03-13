using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Diagnostics.CodeAnalysis;

namespace NetSnifferLib
{
    static class PhysicalAddressHelper
    {
        private static readonly PhysicalAddress BroadcastPhysicalAddress = PhysicalAddress.Parse("FF:FF:FF:FF:FF:FF");

        public static PhysicakAddressEqulityComparer EqulityComparer { get; } = new();

        public class PhysicakAddressEqulityComparer : IEqualityComparer<PhysicalAddress>
        {
            public bool Equals(PhysicalAddress x, PhysicalAddress y)
            {
                return x.Equals(y);
            }

            public int GetHashCode([DisallowNull] PhysicalAddress obj)
            {
                return obj.GetHashCode();
            }
        }

        public static bool IsBroadcast(PhysicalAddress address)
        {
            return BroadcastPhysicalAddress.Equals(address);
        }

        public static bool IsMulticast(PhysicalAddress address)
        {
            return GetOUI(address).IsMulticast();
        }

        public static bool IsHostAddress(PhysicalAddress address)
        {
            return !IsBroadcast(address) && !IsMulticast(address);
        }

        public static Oui GetOUI(PhysicalAddress address)
        {
            var addressBytes = address.GetAddressBytes();
            return new Oui(new byte[] { addressBytes[0], addressBytes[1], addressBytes[2] });
        }

        public static PhysicalAddress CloneAddress(PhysicalAddress address)
        {
            return new PhysicalAddress(address.GetAddressBytes());
        }

        public static VendorAssigned GetVedorAssigned(PhysicalAddress address)
        {
            var addressBytes = address.GetAddressBytes();
            return new VendorAssigned(new byte[] { addressBytes[0], addressBytes[1], addressBytes[2] });
        }
    }
}
