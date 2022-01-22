using PcapDotNet.Packets.Transport;

using NetSnifferLib.General;

namespace NetSnifferLib.Analysis.Transport
{
    interface ITransportAnalyzer : IAnalyzer, IStatistics 
    {
        ushort GetSourcePort(TransportDatagram datagram);

        ushort GetDestinationPort(TransportDatagram datagram);
    }
}
