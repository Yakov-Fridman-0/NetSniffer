using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;
using NetSnifferLib.General;

namespace NetSnifferLib.Transport
{
    public abstract class BaseTransportAnalyzer<T> where T : TransportDatagram
    {
        protected static bool OneOf(ushort sourcePort, ushort destinationPort, ushort port)
        {
            return sourcePort == port || destinationPort == port;
        }

        protected static bool TwoOf(ushort sourcePort, ushort destinationPort, ushort port1, ushort port2)
        {
            return OneOf(sourcePort, destinationPort, port1) && OneOf(sourcePort, destinationPort, port2);
        }

        public override abstract Datagram GetDatagramPayload(Datagram datagram);

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
