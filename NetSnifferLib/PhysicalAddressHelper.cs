using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace NetSnifferLib
{
    static class PhysicalAddressHelper
    {
        private static readonly PhysicalAddress BroadcastPhysicalAddress = PhysicalAddress.Parse("FF:FF:FF:FF:FF:FF");

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

        public static OUI GetOUI(PhysicalAddress address)
        {
            var addressBytes = address.GetAddressBytes();
            return new OUI(new byte[] { addressBytes[0], addressBytes[1], addressBytes[2] });
        }

        public static VendorAssigned GetVedorAssigned(PhysicalAddress address)
        {
            var addressBytes = address.GetAddressBytes();
            return new VendorAssigned(new byte[] { addressBytes[0], addressBytes[1], addressBytes[2] });
        }
    }
}
