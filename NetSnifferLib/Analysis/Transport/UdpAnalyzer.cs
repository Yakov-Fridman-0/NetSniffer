using System.Linq;

using NetSnifferLib.General;
using NetSnifferLib.Packets.Bootp;
using NetSnifferLib.Packets.Dhcp;

using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;


namespace NetSnifferLib.Analysis.Transport
{
    class UdpAnalyzer : BaseTransportAnalyzer<UdpDatagram>
    {
        private const ushort DnsPort = 53;

        private const ushort DhcpPort1 = 67;
        private const ushort DhcpPort2 = 68;

        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            var udpDatagram = (UdpDatagram)datagram;

            ushort sourcePort = GetSourcePort(udpDatagram);
            ushort destinationPort = GetDestinationPort(udpDatagram);

            if (OneOf(sourcePort, destinationPort, DnsPort) && udpDatagram.Dns.IsValid)
            {
                return udpDatagram.Dns;
            }
            if (TwoOf(sourcePort, destinationPort, DhcpPort1, DhcpPort2))
            {
                if (GetDhcpDatagram(udpDatagram)?.IsValid ?? false)
                    return GetDhcpDatagram(udpDatagram);
            }
                
            return udpDatagram.Payload;
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            var udpDatagram = (UdpDatagram)datagram;

            ushort sourcePort = GetSourcePort(udpDatagram);
            ushort destinationPort = GetDestinationPort(udpDatagram);

            if (OneOf(sourcePort, destinationPort, DnsPort) && udpDatagram.Dns.IsValid)
            {
                return DatagramAnalyzer.DnsAnalyzer;
            }
            if (TwoOf(sourcePort, destinationPort, DhcpPort1, DhcpPort2))
            {
                if (GetDhcpDatagram(udpDatagram)?.IsValid ?? false)
                    return DatagramAnalyzer.DhcpAnalyzer;
            }
                
            return null;
        }

        public override string GetDatagramInfo(UdpDatagram datagram)
        {
            return $"{GetSourcePort(datagram)} → {GetDestinationPort(datagram)} Len={GetPayloadLength(datagram)}";
        }

        public static BootpDatagram GetBootpDatagram(UdpDatagram udpDatagram)
        {
            return new BootpDatagram(udpDatagram.Payload.ToArray());
        }

        public static DhcpDatagram GetDhcpDatagram(UdpDatagram udpDatagram)
        {
            return new DhcpDatagram(udpDatagram.Payload.ToArray());
        }

        public override string ProtocolString => "UDP";
    }
}
