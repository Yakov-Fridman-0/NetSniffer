﻿using System.Net;
using System.Net.NetworkInformation;

using NetSnifferLib.General;

namespace NetSnifferLib
{
    public static class AddressFormat
    {
        public static string ToString(IAddress address)
        {
            if (address.IsPhysicalAddress)
                return ToString(address.PhysicalAddress);
            else if (address.IsIpAddress)
                return ToString(address.IpAddress);
            else if (address.IsIpEndPoint)
                return ToString(address.IpEndPoint);
            else
                return string.Empty;
        }

        public static string ToString(PhysicalAddress address)
        {
            return address.ToString();
        }

        public static string ToString(IPAddress address)
        {
            return address.ToString();
        }

        public static string ToString(IPEndPoint address)
        {
            return ToString(address.Address);
        }
    }
}
