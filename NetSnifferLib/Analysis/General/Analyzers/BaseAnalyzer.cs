using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    abstract class BaseAnalyzer<TDatagram, TContext> : IAnalyzer 
        where TDatagram: Datagram
        where TContext : IContext
    {
        public abstract string Protocol { get; }

        protected abstract string GetInfo(TDatagram datagram, TContext context);

        protected abstract IAnalysis AnalyzeDatagramCore(TDatagram datagram, TContext context, int packetId);

        public IAnalysis AnalyzeDatagram(Datagram datagram, IContext context, int packetId)
        {
            PacketData.GetPacketDataByPacketId(packetId).AddProtocol(Protocol, datagram);

            return AnalyzeDatagramCore((TDatagram)datagram, (TContext)context, packetId);
        }
    }
}
