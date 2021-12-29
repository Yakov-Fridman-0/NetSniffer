using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV6;
using PcapDotNet.Packets.IpV4;
using NetSnifferLib.General;

namespace NetSnifferLib.NetworkLayer
{
    public class IpV6Analyzer : BaseNetworkLayerAnalyzer<IpV6Datagram>
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
/*            return ipDatagram.Protocol switch
            {
                IpV4Protocol.Udp => ipDatagram.Udp,
                IpV4Protocol.Tcp => ipDatagram.Tcp,
                _ => null
            };*/
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
