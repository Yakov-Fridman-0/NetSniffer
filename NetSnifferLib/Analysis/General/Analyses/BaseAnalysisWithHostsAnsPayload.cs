using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    class BaseAnalysisWithHostsAndPayload<TAddress, TConext> :
        BaseAnalysis<TAddress, TConext>,
        IAddHostsInfo<TAddress>,
        IAddPayloadInfo<TConext>
        where TAddress : class, IAddress
        where TConext : class, IContext, new()
    {
        public BaseAnalysisWithHostsAndPayload()
        {
            _hostsSupplied = false;
            _payloadSupplied = false;
        }

        public void AddHostsInfo(TAddress source, TAddress destination)
        {
            AddHostsInfoCore(source, destination);
        }

        public void AddPayloadInfo(Datagram payload, TConext payloadContext, IAnalyzer payloadAnalyzer)
        {
            AddPayloadInfo(payload, payloadContext, payloadAnalyzer);
        }
    }
}
