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
            IAnalyzer nextAnalyzer;

            string protocol = default;

            while (analyzer is not null)
            {
                protocol = analyzer.ProtocolString;

                nextAnalyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = nextAnalyzer;
            } 

            return protocol;
        }

        public static string GetPakcetSource(Packet packet)
        {
            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            Datagram datagram = packet.Ethernet;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;
            IAnalyzer nextAnalyzer;

            string source = default;

            while (analyzer is not null)
            {
                if (analyzer.SupportsHosts)
                    source = analyzer.GetDatagramSourceString(datagram);

                nextAnalyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = nextAnalyzer;
            } 

            return source;
        }

        public static string GetPakcetDestination(Packet packet)
        {
            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            Datagram datagram = packet.Ethernet;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;
            IAnalyzer nextAnalyzer;

            string destination = default;

            while(analyzer is not null)
            {
                if (analyzer.SupportsHosts)
                    destination = analyzer.GetDatagramDestinationString(datagram);

                nextAnalyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = nextAnalyzer;
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
            IAnalyzer nextAnalyzer;

            string info = default;

            while (analyzer is not null)
            {
                info = analyzer.GetDatagramInfo(datagram);

                nextAnalyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = nextAnalyzer;
            }

            return info;
        }
    }
}
