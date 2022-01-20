using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    abstract class GenericAnalyzer<T> : IAnalyzer where T: Datagram
    {
        public abstract string Protocol { get; }

        public abstract IAnalysis AnalyzeDatagram(T datagram, IContext context);

        public IAnalysis AnalyzeDatagram(Datagram datagram, IContext context)
        {
            return AnalyzeDatagram(datagram, context);
        }
    }
}
