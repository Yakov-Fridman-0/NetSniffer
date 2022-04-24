using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.IpV6;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.General
{
    static class AddressConvert
    {

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

        public static IPAddress ToIpAddress(IpV6Address address)
        {
            return IPAddress.Parse(address.ToString());
        }

        public static IAddress ToIAddress(IPAddress address)
        {
            return new IpAddressContainer(address);
        }

        public static IAddress ToIAddress(PhysicalAddress address)
        {
            return new PhysicalAddressContainer(address);
        }

        public static IAddress ToIAddress(IPEndPoint address)
        {
            return new IpEndPointContainer(address);
        }
    }
}
