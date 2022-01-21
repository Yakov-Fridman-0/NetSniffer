using System.Net;

using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.DataLink;

namespace NetSnifferLib.Analysis.Network
{
    class IpV4Analyzer : BaseNetworkAnalyzer<IpV4Datagram>
    {
        public override string Protocol => "IPv4";

        protected override IPAddress GetSource(IpV4Datagram datagram)
        {
            return AddressConvert.ToIpAddress(datagram.Source);
        }

        protected override IPAddress GetDestination(IpV4Datagram datagram)
        {
            return AddressConvert.ToIpAddress(datagram.Destination);
        }

        protected override int GetPayloadLength(IpV4Datagram datgram)
        {
            return datgram.Payload.Length;
        }

        protected override Datagram GetPayloadAndAnalyzer(IpV4Datagram datagram, out IAnalyzer analyzer)
        {
            Datagram payload;

            switch(datagram.Protocol)
            {
                case IpV4Protocol.Udp:
                    payload = datagram.Udp;
                    analyzer = DatagramAnalyzer.UdpAnalyzer;
                    break;
                case IpV4Protocol.Tcp:
                    payload = datagram.Tcp;
                    analyzer = DatagramAnalyzer.TcpAnalyzer;
                    break;
                default:
                    payload = datagram.Payload;
                    analyzer = null;
                    break;
            }

            return payload;
        }

        protected override string GetInfo(IpV4Datagram datagram, DataLinkContext context)
        {
            return string.Empty;
        }
    }
}
