using PcapDotNet.Packets.Arp;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.DataLink;

namespace NetSnifferLib.Analysis.Miscellaneous
{
    class ArpAnalyzer : BaseAnalyzer<ArpDatagram, DataLinkContext>
    {
        public override string Protocol => "ARP";

        protected override string GetInfo(ArpDatagram datagram, DataLinkContext context)
        {
            var senderIp = AddressConvert.ToIpAddress(datagram.SenderProtocolIpV4Address);
            var targetIp = AddressConvert.ToIpAddress(datagram.TargetProtocolIpV4Address);

            var senderMac = AddressConvert.ToPhysicalAddress(datagram.SenderHardwareAddress);

            return datagram.Operation switch
            {
                ArpOperation.Request => $"Who has {targetIp}? Tell {senderIp}",

                ArpOperation.Reply => $"{senderIp} is at {senderMac}",

                _ => string.Empty
            };
        }

        protected override ArpAnalysis AnalyzeDatagramCore(ArpDatagram datagram, DataLinkContext context)
        {
            var analysis = new ArpAnalysis();
            analysis.AddInfo(GetInfo(datagram, context));

            return analysis;
        }
    }
}
