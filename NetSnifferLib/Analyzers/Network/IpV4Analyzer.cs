using System.Net;
using NetSnifferLib.General;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;

namespace NetSnifferLib.Network
{
    public class IpV4Analyzer : BaseNetworkAnalyzer<IpV4Datagram>
    {
        private int packets;
        private int bytes;

        public override IPAddress GetDatagramSource(IpV4Datagram datagram)
        {
            return GetIPAddress(datagram.Source);
        }

        public override IPAddress GetDatagramDestination(IpV4Datagram datagram)
        {
            return GetIPAddress(datagram.Destination);
        }

        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            IpV4Datagram ipDatagram = (IpV4Datagram)datagram;

            return ipDatagram.Protocol switch
            {
                IpV4Protocol.Udp => ipDatagram.Udp,
                IpV4Protocol.Tcp => ipDatagram.Tcp,
                _ => ipDatagram.Payload
            };
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            IpV4Datagram ipDatagram = (IpV4Datagram)datagram;

            return ipDatagram.Protocol switch
            {
                IpV4Protocol.Udp => DatagramAnalyzer.UdpAnalyzer,
                IpV4Protocol.Tcp => DatagramAnalyzer.TcpAnalyzer,
                _ => null
            };
        }

        public override string GetDatagramInfo(IpV4Datagram datagram)
        {
            return string.Empty;
        }

        public override string ProtocolString => "IPv4";
    }
}
