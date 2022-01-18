using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.IpV6;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.General
{
    public static class AddressConvert
    {
        private const uint PhysicalAddressStringLength = 12;
        private const uint PhysicalAddressStringPartLenght = 2;

        public static PhysicalAddress ToPhysicalAddress(MacAddress address)
        {
            return PhysicalAddress.Parse(address.ToString());
        }

        public static PhysicalAddress ToPhysicalAddress(ReadOnlyCollection<byte> address)
        {
            byte[] byteArray = new byte[address.Count];
            address.CopyTo(byteArray, 0);
            PhysicalAddress physicalAddress = new(byteArray);
            return physicalAddress;
        }

        public static IPAddress ToIPAddress(IpV4Address address)
        {
            return IPAddress.Parse(address.ToString());
        }

        public static IPAddress ToIPAddress(IpV6Address address)
        {
            return IPAddress.Parse(address.ToString());
        }

        public static string ToString(PhysicalAddress address)
        {
            var lower = address.ToString().ToLowerInvariant();

            List<string> parts = new();
            for (int i = 0; i < PhysicalAddressStringLength; i++)
            {
                parts.Add(lower.Substring(i, (int)PhysicalAddressStringPartLenght));
            }

            return string.Join(":", parts);
        }

        public static string ToString(IPAddress address)
        {
            return address.ToString();
        }
    }
}
