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
    public class ArpAnalyzer : BaseNoHostsNoPayloadAnalyzer
    {
        public override string ProtocolString => "ARP";

        public override string GetDatagramInfo(Datagram datagram)
        {
            ArpDatagram arpDatagram = (ArpDatagram)datagram;

            var senderIp = AddressConverter.GetIPAddress(arpDatagram.SenderProtocolIpV4Address);
            var targetIp = AddressConverter.GetIPAddress(arpDatagram.TargetProtocolIpV4Address);

            var senderMac = AddressConverter.GetPhysicalAddress(arpDatagram.SenderHardwareAddress);

            return arpDatagram.Operation switch
            {
                ArpOperation.Request => $"Who has {targetIp}? Tell {senderIp}",

                ArpOperation.Reply => $"{senderIp} is at {senderMac}",

                _ => string.Empty
            };
        }
    }
}
