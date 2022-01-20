using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSnifferLib.General;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;

namespace NetSnifferLib.Miscellaneous
{
    public class ArpAnalyzer : IAnalyzer
    {
        public override string ProtocolString => "ARP";

        public override string GetDatagramInfo(Datagram datagram)
        {
            ArpDatagram arpDatagram = (ArpDatagram)datagram;

            var senderIp = AddressConvert.ToIpAddress(arpDatagram.SenderProtocolIpV4Address);
            var targetIp = AddressConvert.ToIpAddress(arpDatagram.TargetProtocolIpV4Address);

            var senderMac = AddressConvert.ToPhysicalAddress(arpDatagram.SenderHardwareAddress);

            return arpDatagram.Operation switch
            {
                ArpOperation.Request => $"Who has {targetIp}? Tell {senderIp}",

                ArpOperation.Reply => $"{senderIp} is at {senderMac}",

                _ => string.Empty
            };
        }
    }
}
