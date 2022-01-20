using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    interface IAnalyzer
    {
        IAnalysis AnalyzeDatagram(Datagram datagram, IContext context);

        string Protocol { get; }
    }
}
