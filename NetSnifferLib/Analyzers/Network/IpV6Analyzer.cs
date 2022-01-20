using System.Net;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV6;
using NetSnifferLib.General;

namespace NetSnifferLib.Network
{
    public class IpV6Analyzer : BaseNetworkAnalyzer<IpV6Datagram>
    {
        public override IPAddress GetDatagramSource(IpV6Datagram datagram)
        {
            return GetIPAddress(datagram.Source);
        }

        public override IPAddress GetDatagramDestination(IpV6Datagram datagram)
        {
            //TODO: Check options
            return GetIPAddress(datagram.CurrentDestination);
        }

        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            //TODO: Add logic
            IpV6Datagram ipDatagram = (IpV6Datagram)datagram;

            if (ipDatagram.ExtensionHeaders == IpV6ExtensionHeaders.Empty)
                return ipDatagram.Payload;

            return ipDatagram.Payload;
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            //TODO: Add logic
            IpV6Datagram ipDatagram = (IpV6Datagram)datagram;

            if (ipDatagram.ExtensionHeaders == IpV6ExtensionHeaders.Empty)
                return null;

            return null;
/*            return ipDatagram.Protocol switch
            {
                IpV4Protocol.Udp => DatagramAnalyzer.UdpAnalyzer,
                IpV4Protocol.Tcp => DatagramAnalyzer.TcpAnalyzer,
                _ => null
            };*/
        }

        public override string GetDatagramInfo(IpV6Datagram datagram)
        {
            return string.Empty;
        }

        public override string ProtocolString => "IPv6";
    }
}
