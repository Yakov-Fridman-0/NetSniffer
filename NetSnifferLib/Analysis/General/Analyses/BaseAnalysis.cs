using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    abstract class BaseAnalysis<TAddress, TContext> : IAnalysis
        where TAddress : class, IAddress
        where TContext : class, IContext,
        new()
    {
        protected string _info = "";
        protected TAddress _source = null;
        protected TAddress _destination = null;
        protected Datagram _payload = null;
        protected TContext _payloadContext = null;
        protected IAnalyzer _payloadAnalyzer = null;

        protected bool? _infoSupplied = false;
        protected bool? _hostsSupplied = null;
        protected bool? _payloadSupplied = null;

        public void AddInfo(
            string info)
        {
            _infoSupplied = true;
            _info = info;
        }

        protected void AddHostsInfoCore(
            TAddress source,
            TAddress destination)
        {
            _hostsSupplied = true;
            _source = source;
            _destination = destination;
        }

        protected void AddPayloadInfoCore(
            Datagram payload,
            TContext payloadContext,
            IAnalyzer payloadAnalyzer)
        {
            _payloadSupplied = true;
            _payload = payload;
            _payloadContext = payloadContext;
            _payloadAnalyzer = payloadAnalyzer;
        }

        public string Info => _info;

        public IAddress Source => _source;

        public IAddress Destination => _destination;

        public bool HasAddresses => Source != null;

        public Datagram Payload => _payload;

        public IContext PayloadContext => _payloadContext;

        public IAnalyzer PayloadAnalyzer => _payloadAnalyzer;

        public bool HasPayload => Payload != null;

        public bool Complete => 
            _infoSupplied.GetValueOrDefault(true) && 
            _hostsSupplied.GetValueOrDefault(true) && 
            _hostsSupplied.GetValueOrDefault(true);
    }
}
