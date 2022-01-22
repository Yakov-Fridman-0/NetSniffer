using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    public class LanHost : IIpAddress, IPhysicalAddress
    {
        public PhysicalAddress PhysicalAddress { get; protected set; } = null;

        public IPAddress IpAddress { get; protected set; } = null;

        public LanHost(PhysicalAddress physicalAddress)
        {
            PhysicalAddress = physicalAddress;
        }

        public LanHost(PhysicalAddress physicalAddress, IPAddress iPAddress)
        {
            IpAddress = iPAddress;
            PhysicalAddress = physicalAddress;
        }

        public override string ToString()
        {
            return PhysicalAddress.ToString() + IpAddress?.ToString();
        }
        //public LanHost ShallowCopy()
        //{
        //    return (LanHost)MemberwiseClone();
        //}

        //public virtual void SetIpAddress(IPAddress iPAddress)
        //{
        //    IpAddress = iPAddress;
        //}
    }
}
