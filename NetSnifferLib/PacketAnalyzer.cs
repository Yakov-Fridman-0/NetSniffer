using System;
using PcapDotNet.Packets;
using NetSnifferLib.General;

namespace NetSnifferLib
{
    public class PacketAnalyzer
    {
        public PackeStatisticsLogger PackeStatisticsLogger {get; private set;}

        public PacketAnalyzer()
        {
            PackeStatisticsLogger = new();
        }

        public PacketDescription AnalyzePacket(Packet packet)
        {
            PackeStatisticsLogger.LogPacket((ushort)packet.Length);

            if (!IsEthernet(packet))
                throw new InvalidOperationException("Packet datalink must be Ethernet.");

            string timeStamp, protocol = default, source = default, destination = default, length, info = default;

            timeStamp = packet.Timestamp.ToString("HH:mm:ss.ffff");
            length = packet.Length.ToString();

            Datagram datagram = packet.Ethernet;
            IAnalyzer analyzer = DatagramAnalyzer.EthernetAnalyzer;
            IAnalyzer nextAnalyzer;

            ushort GetPayloadLength(Datagram datagram) => (ushort)analyzer.GetDatagramPayload(datagram).Length;
            

            while (analyzer is not null)
            {
                protocol = analyzer.ProtocolString;
                info = analyzer.GetDatagramInfo(datagram);


                switch (analyzer)
                {
                    case LinkLayer.EthernetAnalyzer:
                        PackeStatisticsLogger.LogEthernetPacket(GetPayloadLength(datagram));
                        break;
                    case NetworkLayer.IpV4Analyzer:
                        PackeStatisticsLogger.LogIpV4Packet(GetPayloadLength(datagram));
                        break;
                    case NetworkLayer.IpV6Analyzer:
                        PackeStatisticsLogger.LogIpV6Packet(GetPayloadLength(datagram));
                        break;
                    case TransportLayer.UdpAnalyzer:
                        PackeStatisticsLogger.LogUdpPacket(GetPayloadLength(datagram));
                        break;
                    case TransportLayer.TcpAnalyzer:
                        PackeStatisticsLogger.LogTcpPacket(GetPayloadLength(datagram));
                        break;
                }


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

        public static bool IsEthernet(Packet packet) => packet.DataLink.Kind == DataLinkKind.Ethernet;
    }
}
