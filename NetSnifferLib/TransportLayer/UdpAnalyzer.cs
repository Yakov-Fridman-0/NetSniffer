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
        private const ushort DnsPort = 53;

        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            var udpDatagram = (UdpDatagram)datagram;

            ushort sourcePort = GetSourcePort(udpDatagram);
            ushort destinationPort = GetDestinationPort(udpDatagram);

            if (OneOf(sourcePort, destinationPort, DnsPort))
                return udpDatagram.Dns;
            else
                return udpDatagram.Payload;
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            var udpDatagram = (UdpDatagram)datagram;

            ushort sourcePort = GetSourcePort(udpDatagram);
            ushort destinationPort = GetDestinationPort(udpDatagram);

            if (OneOf(sourcePort, destinationPort, DnsPort))
                return DatagramAnalyzer.DnsAnalyzer;
            else
                return null;
        }

        public override string GetDatagramInfo(UdpDatagram datagram)
        {
            return $"{GetSourcePort(datagram)} → {GetDestinationPort(datagram)} Len={GetPayloadLength(datagram)}";
        }

        public override string ProtocolString => "UDP";
    }
}
