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
    }
}
