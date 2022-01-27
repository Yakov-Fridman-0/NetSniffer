#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib
{
    abstract class PhysicalAddressComponent
    {
        protected readonly byte[] bytes;

        public PhysicalAddressComponent(byte[] address)
        {
            bytes = address;
        }

        public byte[] GetIdentifiersBytes()
        {
            return bytes;
        }

        public override bool Equals(object? comparand)
        {
            if(comparand is PhysicalAddressComponent otherOUI)
            {
                var comparandBytes = otherOUI.GetIdentifiersBytes();
                return
                    bytes[0] == comparandBytes[0] &&
                    bytes[1] == comparandBytes[1] &&
                    bytes[2] == comparandBytes[2];
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
