using PcapDotNet.Packets.Transport;

namespace NetSnifferLib.TransportLayer
{
    public interface ITransportLayerAnalyzer<T> : IDatagramInfo<T> where T : TransportDatagram
    {
        ushort GetSourcePort(T datagram);

        ushort GetDestinationPort(T datagram);

        ushort GetPayloadLength(T datagram);
    }
}
