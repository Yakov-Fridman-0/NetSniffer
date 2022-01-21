using System.Collections.Generic;
using System.Linq;

using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.Network;
using NetSnifferLib.Analysis.Application;
using NetSnifferLib.Packets.Bootp;
using NetSnifferLib.Packets.Dhcp;

namespace NetSnifferLib.Analysis.Transport
{
    class UdpAnalyzer : BaseTransportAnalyzer<UdpDatagram>
    {
        public override string Protocol => "UDP";

        protected override string GetInfo(UdpDatagram datagram, NetworkContext context)
        {
            return $"{GetSourcePortCore(datagram)} → {GetDestinationPortCore(datagram)} Len={GetPayloadLength(datagram)}";
        }

        protected override Datagram GetPayloadAndAnalyzer(UdpDatagram datagram, out IAnalyzer analyzer)
        {
            ushort sourcePort = GetSourcePortCore(datagram);
            ushort destinationPort = GetDestinationPortCore(datagram);

            foreach (KeyValuePair<IApplicationAnalyzer, IPortsMatch> kvp in DatagramAnalyzer.AnalyzersOverUdp)
            {
                var portsMatch = kvp.Value;
                var applicationAnalyzer = kvp.Key;
                Datagram applicationDatagram = null;

                if(portsMatch.PortsMatch(sourcePort, destinationPort) && applicationAnalyzer.TryGetDatagram(datagram, ref applicationDatagram))
                {
                    analyzer = applicationAnalyzer;
                    return applicationDatagram;
                }
            }

            analyzer = null;
            return null;
        }
    }
}
