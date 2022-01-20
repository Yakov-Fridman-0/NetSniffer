using System.Net;

namespace NetSnifferLib.General
{
    class IpEndPointContainer : BaseAddressContainer
    {
        readonly IPEndPoint _ipEndPoint;

        public IpEndPointContainer(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;
        }

        public override IPEndPoint IpEndPoint => _ipEndPoint;
    }
}
