using PcapDotNet.Packets.Transport;
using NetSnifferLib.General;

namespace NetSnifferLib.TransportLayer
{
    public interface ITransportLayerAnalyzer<T> : IAnalyzer where T : TransportDatagram
    {
        ushort GetSourcePort(T datagram);

        ushort GetDestinationPort(T datagram);

        ushort GetPayloadLength(T datagram);
    }
}
