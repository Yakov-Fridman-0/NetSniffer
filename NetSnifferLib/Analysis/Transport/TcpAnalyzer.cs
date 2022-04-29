using System.Collections.Generic;
using System.Linq;

using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;
using NetSnifferLib.General;

using NetSnifferLib.Analysis.Network;
using NetSnifferLib.Analysis.Application;
using NetSnifferLib.StatefulAnalysis.Tcp;

namespace NetSnifferLib.Analysis.Transport
{
    class TcpAnalyzer : BaseTransportAnalyzer<TcpDatagram>
    {
        public override string Protocol => "TCP";

        protected override string GetInfo(TcpDatagram datagram, NetworkContext context)
        {
            var portsInfo = $"{GetSourcePortCore(datagram)} → {GetDestinationPortCore(datagram)}";
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

            TcpStatefulAnalyzer.AnalyzeDatagram(datagram, context);

            return string.Join(" ", new[] { portsInfo, flagsInfo, optionsInfo });
        }

        protected override Datagram GetPayloadAndAnalyzer(TcpDatagram datagram, out IAnalyzer analyzer)
        {
            //ushort sourceport = getsourceportcore(datagram);
            //ushort destinationport = getdestinationportcore(datagram);

            //foreach (keyvaluepair<iapplicationanalyzer, iportsmatch> kvp in datagramanalyzer.ana)
            //{
            //    var portsmatch = kvp.value;
            //    var applicationanalyzer = kvp.key;
            //    datagram applicationdatagram = null;

            //    if (portsmatch.portsmatch(sourceport, destinationport) && applicationanalyzer.trygetdatagram(datagram, ref applicationdatagram))
            //    {
            //        analyzer = applicationanalyzer;
            //        return applicationdatagram;
            //    }
            //}

            analyzer = null;
            return null;
        }
    }
}
