using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    abstract class BaseAnalyzer<TDatagram, TContext> : IAnalyzer 
        where TDatagram: Datagram
        where TContext : IContext
    {
        public abstract string Protocol { get; }

        protected abstract string GetInfo(TDatagram datagram, TContext context);

        protected abstract IAnalysis AnalyzeDatagramCore(TDatagram datagram, TContext context);

        public IAnalysis AnalyzeDatagram(Datagram datagram, IContext context)
        {
            return AnalyzeDatagramCore((TDatagram)datagram, (TContext)context);
        }
    }
}
