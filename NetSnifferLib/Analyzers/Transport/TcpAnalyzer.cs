using System.Collections.Generic;
using System.Linq;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;
using NetSnifferLib.General;
using NetSnifferLib.Miscellaneous;

namespace NetSnifferLib.Transport
{
    public class TcpAnalyzer : BaseTransportAnalyzer<TcpDatagram>
    {
        private const ushort HttpPort = 80;

        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            var tcpDatagram = (TcpDatagram)datagram;
            Datagram payload = tcpDatagram.Payload;

            ushort sourcePort = GetSourcePort(tcpDatagram), destinationPort = GetDestinationPort(tcpDatagram);

            if (OneOf(sourcePort, destinationPort, HttpPort) && HttpAnalyzer.DatagramMatches(payload))
            {
                return tcpDatagram.Http;
            }
            else
            {
                return tcpDatagram.Payload;
            }
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            var tcpDatagram = (TcpDatagram)datagram;
            Datagram payload = tcpDatagram.Payload;

            ushort sourcePort = GetSourcePort(tcpDatagram), destinationPort = GetDestinationPort(tcpDatagram);

            if (OneOf(sourcePort, destinationPort, HttpPort) && HttpAnalyzer.DatagramMatches(payload))
            {
                return DatagramAnalyzer.HttpAnalyzer;
            }
            else
            {
                return null;
            }
        }

        public override string GetDatagramInfo(TcpDatagram datagram)
        {
            var portsInfo = $"{GetSourcePort(datagram)} → {GetDestinationPort(datagram)}";
            List<string> flags = new();

            if (datagram.IsFin)
                flags.Add("FYN");
            if (datagram.IsSynchronize)
                flags.Add("SYN");
            if (datagram.IsReset)
                flags.Add("RST");
            if (datagram.IsPush)
                flags.Add("PSH");
            if (datagram.IsAcknowledgment)
                flags.Add("ACK");
            if (datagram.IsUrgent)
                flags.Add("URG");
            //TODO: Add other options

            var flagsInfo = $"[{string.Join(", ", flags)}]";

            SortedDictionary<string, string> options = new();

            foreach (TcpOption tcpOption in datagram.Options)
            {
                if (tcpOption.OptionType == TcpOptionType.MaximumSegmentSize)
                    options.Add("MSS", "");
            }

            var optionsInfo = string.Join(" ", from kvp in options select $"{ kvp.Key}: {kvp.Value}");

            return string.Join(" ", new[] { portsInfo, flagsInfo, optionsInfo });
        }

        public override string ProtocolString => "TCP";
    }
}
