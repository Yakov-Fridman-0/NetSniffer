using NetSnifferLib.General;

namespace NetSnifferLib.Analysis.DataLink
{
    class DataLinkContext : BaseContext<PhysicalAddressContainer>
    {
        public DataLinkContext() { }

        public DataLinkContext(PhysicalAddressContainer source, PhysicalAddressContainer destination) : base(source, destination) { }
    }
}
