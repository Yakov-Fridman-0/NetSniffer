using NetSnifferLib.General;

namespace NetSnifferLib.Analysis.Transport
{
    class TransportContext : BaseContext<IpEndPointContainer> 
    {
        public TransportContext() { }

        public TransportContext(IpEndPointContainer source, IpEndPointContainer destination) :
            base(source, destination)
        {
            
        }
    }
}
