using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.Network;

namespace NetSnifferLib.Analysis.Transport
{
    abstract class BaseTransportAnalyzer<T> : BaseAnalyzer<T, NetworkContext>, ITransportAnalyzer 
        where T : TransportDatagram
    {
        protected static bool OneOf(ushort sourcePort, ushort destinationPort, ushort port)
        {
            return sourcePort == port || destinationPort == port;
        }

        protected static bool TwoOf(ushort sourcePort, ushort destinationPort, ushort port1, ushort port2)
        {
            return OneOf(sourcePort, destinationPort, port1) && OneOf(sourcePort, destinationPort, port2);
        }

        public override abstract Datagram GetPayload(Datagram datagram);

        public abstract string GetDatagramInfo(T datagram);

        public override string GetDatagramInfo(Datagram datagram)
        {
            return GetDatagramInfo((T)datagram);
        }

        public virtual ushort GetDestinationPort(T datagram)
        {
            return datagram.DestinationPort;
        }

        public virtual ushort GetPayloadLength(T datagram)
        {
            return (ushort)datagram.Payload.Length;
        }

        public virtual ushort GetSourcePort(T datagram)
        {
            return datagram.SourcePort;
        }
    }
}
