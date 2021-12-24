using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.Transport;

namespace NetSnifferLib.TransportLayer
{
    public abstract class BaseTransportLayerAnalyzer<T> : ITransportLayerAnalyzer<T> where T : TransportDatagram
    {
        public abstract string GetDatagramInfo(T datagram);

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
