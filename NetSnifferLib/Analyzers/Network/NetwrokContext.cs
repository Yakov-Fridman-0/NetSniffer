using NetSnifferLib.General;

namespace NetSnifferLib.Network
{
    class NetwrokContext : GenericContext<IpAddressContainer>
    {
        public NetwrokContext(IpAddressContainer source, IpAddressContainer destination) : base(source, destination) { }
    }
}
