using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib
{
    static class IPAddressHelper
    {
        public static IPAddress EmptyAddress { get; } = IPAddress.Parse("0.0.0.0");

        public static bool IsBroadcast(IPAddress address)
        {
            return IPAddress.Broadcast.Equals(address);
        }

        public static bool IsMulticast(IPAddress address)
        {
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                var firstByte = address.GetAddressBytes()[0];
                return 224 <= firstByte && firstByte <= 239;
            }
            else
            {
                return address.IsIPv6Multicast;

            }
        }

        public static bool IsValid(IPAddress address)
        {
            return !EmptyAddress.Equals(address);
        }

        public static bool IsHostAddrress(IPAddress address)
        {
            return IsValid(address) && !IsBroadcast(address) && !IsMulticast(address);
        }
    }
}
