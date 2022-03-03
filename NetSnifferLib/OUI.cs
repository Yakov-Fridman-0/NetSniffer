using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib
{
    class Oui : PhysicalAddressComponent
    {
        public Oui(byte[] address) : base(address)
        {
            if (address.Length != 3)
                throw new ArgumentException("The OUI must be 3 bytes long");
        }

        //public static OUI IANAMulticast { get; } = new(new byte[] { 0x01, 0x00, 0x5E });

        //public static OUI IPv6MulticastLower { get; } = new(new byte[] { 0x33, 0x33, 0x00 });

        //public static OUI IPv6MulticastHigher { get; } = new(new byte[] { 0x33, 0x33, 0xFF });

        public bool IsMulticast()
        {
            return (bytes[0] & 1) == 1;
        }

        public bool IsInRange(Oui lower, Oui higher)
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
