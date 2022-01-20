using NetSnifferLib.General;

namespace NetSnifferLib.DataLink
{
    class DataLinkContext : GenericContext<PhysicalAddressContainer>
    {
        public DataLinkContext(PhysicalAddressContainer source, PhysicalAddressContainer destination) : base(source, destination) { }
    }
}
