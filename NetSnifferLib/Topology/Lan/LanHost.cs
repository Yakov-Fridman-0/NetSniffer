using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    public class LanHost : IPhysicalAddress, IIPAddress
    {
        public PhysicalAddress PhysicalAddress { get; protected set; } = null;

        public IPAddress IPAddress { get; set; } = null;

        public LanHost(PhysicalAddress physicalAddress)
        {
            PhysicalAddress = physicalAddress;
        }

        public LanHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            IPAddress = ipAddress;
            PhysicalAddress = physicalAddress;
        }
       

        public override string ToString()
        {
            return string.Format("{0} {1}", PhysicalAddress.ToString(), IPAddress != null ? IPAddress.ToString() : "N/A");
        }
    }
}
