using System.Net;

namespace NetSnifferLib.General
{
    class IpAddressContainer : BaseAddressContainer
    {
        readonly IPAddress _ipAddress;

        public IpAddressContainer(IPAddress ipAddress)
        {
            _ipAddress = ipAddress;
        }

        public override IPAddress IPAddress => _ipAddress;
    }
}
