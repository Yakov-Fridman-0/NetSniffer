using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib
{
    class IPandPhysicalAddress
    {
        public IPAddress IPAddress { get; }
        public PhysicalAddress PhysicalAddress { get; }

        public IPandPhysicalAddress(IPAddress iPAddress, PhysicalAddress physicalAddress)
        {
            IPAddress = iPAddress;
            PhysicalAddress = physicalAddress;
        }
    }
}
