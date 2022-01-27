using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib
{
    class VendorAssigned : PhysicalAddressComponent
    {
        public VendorAssigned(byte[] address) : base(address)
        {

        }

        public bool IsInRange(VendorAssigned lower, VendorAssigned higher)
        {
            var lowerBytes = lower.GetIdentifiersBytes();
            var higherBytes = higher.GetIdentifiersBytes();

            return
                lowerBytes[0] <= bytes[0] && bytes[0] <= higherBytes[0] &&
                lowerBytes[1] <= bytes[1] && bytes[1] <= higherBytes[1] &&
                lowerBytes[2] <= bytes[2] && bytes[2] <= higherBytes[2];
        }
    }
}
