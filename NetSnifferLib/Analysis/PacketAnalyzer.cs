using System;
using PcapDotNet.Packets;
using NetSnifferLib.General;
using NetSnifferLib.Statistics;

namespace NetSnifferLib
{
    public class PacketAnalyzer
    { 
        public static PacketDescription AnalyzePacket(Packet packet)
        {

            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            string timeStamp, protocol = default, source = default, destination = default, length, info = default;

            timeStamp = packet.Timestamp.ToString("HH:mm:ss.ffff");
            length = packet.Length.ToString();

            Datagram datagram = packet.Ethernet;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;
            IAnalyzer nextAnalyzer;
            

            while (analyzer is not null)
            {
                protocol = analyzer.ProtocolString;
                info = analyzer.GetDatagramInfo(datagram);

                if (analyzer.SupportsHosts)
                {
                    source = analyzer.GetDatagramSourceString(datagram);
                    destination = analyzer.GetDatagramDestinationString(datagram);
                }

                nextAnalyzer = analyzer.GetDatagramPayloadAnalyzer(datagram);
                datagram = analyzer.GetDatagramPayload(datagram);
                analyzer = nextAnalyzer;       
            }

            return new PacketDescription() 
            { 
                TimeStamp = timeStamp,
                Protocol = protocol,
                Source = source,
                Destination = destination,
                Length = length,
                Info = info
            };
        }

        public static GeneralStatistics GetGeneralStatistics()
        {

        }

        public static bool IsEthernet(Packet packet) => packet.DataLink.Kind == DataLinkKind.Ethernet;
    }
}
