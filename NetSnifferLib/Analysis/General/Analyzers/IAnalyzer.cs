using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    interface IAnalyzer
    {
        IAnalysis AnalyzeDatagram(Datagram datagram, IContext context, int packetId);

        string Protocol { get; }
    }
}
