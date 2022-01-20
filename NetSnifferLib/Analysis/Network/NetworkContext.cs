using NetSnifferLib.General;

namespace NetSnifferLib.Analysis.Network
{
    class NetworkContext : 
        BaseContext<IpAddressContainer>
    {
        public NetworkContext() { }

        public NetworkContext(IpAddressContainer source, IpAddressContainer destination) : 
            base(source, destination) { }
    }
}
