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
    public class UdpAnalyzer : BaseTransportLayerAnalyzer<UdpDatagram>
    {
        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            return null;
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            return null;
        }

        public override string GetDatagramInfo(UdpDatagram datagram)
        {
            return $"{GetSourcePort(datagram)} → {GetDestinationPort(datagram)} Len={GetPayloadLength(datagram)}";
        }

        public override string ProtocolString => "UDP";
    }
}
