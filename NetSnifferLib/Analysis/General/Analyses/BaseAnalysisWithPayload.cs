using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    class BaseAnalysisWithPayload<T> : BaseAnalysis<EmptyAddress, T>, IAddPayloadInfo<T>
        where T : class, IContext, new()
    {
        public BaseAnalysisWithPayload()
        {
            _payloadSupplied = false;
        }

        public void AddPayloadInfo(Datagram payload, T payloadContext, IAnalyzer payloadAnalyzer)
        {
            AddPayloadInfoCore(payload, payloadContext, payloadAnalyzer);
        }
    }
}
