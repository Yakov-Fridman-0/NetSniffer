using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    interface IAddPayloadInfo<T> where T : IContext
    {
        public void AddPayloadInfo(Datagram payload, T payloadContext, IAnalyzer payloadAnalyzer);
    }
}
