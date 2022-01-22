using System;

using PcapDotNet.Packets.Arp;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.DataLink;

namespace NetSnifferLib.Analysis.Miscellaneous
{
    class ArpAnalyzer : BaseAnalyzer<ArpDatagram, DataLinkContext>
    {
        public override string Protocol => "ARP";

        public event EventHandler<IPandPhysicalAddress> PayloadIndicatesHost;

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

        private static bool IsRequest(ArpDatagram datagram)
        {
            return datagram.Operation == ArpOperation.Request;
        }

        private static bool IsReply(ArpDatagram datagram)
        {
            return datagram.Operation == ArpOperation.Reply;
        }

        protected override ArpAnalysis AnalyzeDatagramCore(ArpDatagram datagram, DataLinkContext context)
        {
            if (IsRequest(datagram))
            {
                PayloadIndicatesHost?.Invoke(
                    this, 
                    new IPandPhysicalAddress(
                        AddressConvert.ToIpAddress(datagram.SenderProtocolIpV4Address),
                        AddressConvert.ToPhysicalAddress(datagram.SenderHardwareAddress))
                    );
            }
            if (IsReply(datagram))
            {
                PayloadIndicatesHost?.Invoke(
                    this, 
                    new IPandPhysicalAddress(
                        AddressConvert.ToIpAddress(datagram.SenderProtocolIpV4Address),
                        AddressConvert.ToPhysicalAddress(datagram.SenderHardwareAddress))
                    );

                PayloadIndicatesHost?.Invoke(
                    this, 
                    new IPandPhysicalAddress(
                        AddressConvert.ToIpAddress(datagram.TargetProtocolIpV4Address),
                        AddressConvert.ToPhysicalAddress(datagram.TargetHardwareAddress))
                    );
            }

            var analysis = new ArpAnalysis();
            analysis.AddInfo(GetInfo(datagram, context));

            return analysis;
        }
    }
}
