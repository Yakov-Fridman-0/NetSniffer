using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.General
{
    class BaseAddressContainer : IAddress
    {
        public virtual PhysicalAddress PhysicalAddress => null;

        public bool IsPhysicalAddress => PhysicalAddress != null;

        public virtual IPAddress IpAddress => null;

        public bool IsIpAddress => IpAddress != null;

        public virtual IPEndPoint IpEndPoint => null;

        public bool IsIpEndPoint => IpEndPoint != null;
    }
}
