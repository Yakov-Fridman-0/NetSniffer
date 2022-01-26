using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    public class LanHost : IPhysicalAddress, IIPAddresses
    {
        public PhysicalAddress PhysicalAddress { get; protected set; } = null;

        public List<IPAddress> IPAddresses { get; protected set; } = new();

        public LanHost(PhysicalAddress physicalAddress)
        {
            PhysicalAddress = physicalAddress;
        }

        public LanHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            IPAddresses.Add(ipAddress);
            PhysicalAddress = physicalAddress;
        }

        public void AddAddress(IPAddress ipAddress)
        {
            IPAddresses.Add(ipAddress);
        }

        public override string ToString()
        {
            return PhysicalAddress.ToString() + " " + string.Join(", ", IPAddresses)?.ToString();
        }
    }
}
