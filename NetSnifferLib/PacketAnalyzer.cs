using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using NetSnifferLib.General;
using NetSnifferLib.LinkLayer;
using NetSnifferLib.NetworkLayer;
using NetSnifferLib.TransportLayer;

namespace NetSnifferLib
{
    public static class PacketAnalyzer
    {
        public static bool IsEthernet(Packet packet)
        {
            return packet.DataLink.Kind == DataLinkKind.Ethernet;
        }

        public static string GetPacketTimestamp(Packet packet)
        {
            return packet.Timestamp.ToString("HH:mm:ss.ffff");
        }

        public static string GetPakcetProtocol(Packet packet)
        {
            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            Datagram datagram = packet.Ethernet;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;

            string protocol = analyzer.ProtocolString;

            while (analyzer is not null)
            {
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);

                protocol = analyzer.GetDatagramSourceString(datagram);
            }

            return protocol;
        }

        public static string GetPakcetSource(Packet packet)
        {
            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            Datagram datagram = packet.Ethernet;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;

            string source = analyzer.GetDatagramSourceString(datagram);

            while (analyzer is not null)
            {
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);

                if (analyzer.SupportsHosts)
                    source = analyzer.GetDatagramSourceString(datagram);
            }

            return source;
        }

        public static string GetPakcetDestination(Packet packet)
        {
            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            Datagram datagram = packet.Ethernet;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;

            string destination = analyzer.GetDatagramDestinationString(datagram);

            while (analyzer is not null)
            {
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);

                if (analyzer.SupportsHosts)
                    destination = analyzer.GetDatagramDestinationString(datagram);
            }

            return destination;
        }

        public static string GetPacketLength(Packet packet)
        {
            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            return packet.Length.ToString();
        }

        public static string GetPacketInfo(Packet packet)
        {
            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            Datagram datagram = packet.Ethernet;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;

            string info = analyzer.GetDatagramInfo(datagram);

            while (analyzer is not null)
            {
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);

                info = analyzer.GetDatagramInfo(datagram);
            }

            return info;
        }
    }
}
