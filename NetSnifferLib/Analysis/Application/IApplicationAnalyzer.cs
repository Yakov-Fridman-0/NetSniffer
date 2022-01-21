using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;

using NetSnifferLib.General;

namespace NetSnifferLib.Analysis.Application
{
    interface IApplicationAnalyzer : IAnalyzer 
    {
        bool TryGetDatagram(TransportDatagram transportPayload, ref Datagram datagram);
    }
}
