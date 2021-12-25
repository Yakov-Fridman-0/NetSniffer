using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;
using NetSnifferLib.General;

namespace NetSnifferLib.TransportLayer
{
    public abstract class BaseTransportLayerAnalyzer<T> : BaseNoHostsAnalyzer, ITransportLayerAnalyzer<T> where T : TransportDatagram
    {
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
