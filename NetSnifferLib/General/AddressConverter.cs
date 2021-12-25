using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.IpV6;
using PcapDotNet.Packets.Ethernet;
using System.Net.NetworkInformation;
using System.Net;

namespace NetSnifferLib.General
{
    public static class AddressConverter
    {
        public static PhysicalAddress GetPhysicalAddress(MacAddress address)
        {
            return PhysicalAddress.Parse(address.ToString());
        }

        public static PhysicalAddress GetPhysicalAddress(ReadOnlyCollection<byte> address)
        {
            byte[] byteArray = new byte[address.Count];
            address.CopyTo(byteArray, 0);
            PhysicalAddress physicalAddress = new(byteArray);
            return physicalAddress;
        }
        public static IPAddress GetIPAddress(IpV4Address address)
        {
            return IPAddress.Parse(address.ToString());
        }

        public static IPAddress GetIPAddress(IpV6Address address)
        {
            return IPAddress.Parse(address.ToString());
        }
    }
}
