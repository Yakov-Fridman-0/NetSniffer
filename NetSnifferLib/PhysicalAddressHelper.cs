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
