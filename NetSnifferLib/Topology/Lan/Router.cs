using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    public class Router : LanHost
    {
        public Router(PhysicalAddress physicalAddress) : base(physicalAddress)
        {

        }

        public Router(PhysicalAddress physicalAddress, IPAddress ipAddress) : base(physicalAddress,ipAddress)
        {

        }
    }
}
