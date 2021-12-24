using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSnifferLib;
using NetSnifferLib.General;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using System.Net.NetworkInformation;

namespace NetSnifferLib.LinkLayer
{
    public class EthernetAnalyzer : BaseLinkLayerAnalyzer<EthernetDatagram>
    {
        public override PhysicalAddress GetDatagramSource(EthernetDatagram datagram)
        {
            return GetPhysicalAddress(datagram.Source);
        }

        public override PhysicalAddress GetDatagramDestination(EthernetDatagram datagram)
        {
            return GetPhysicalAddress(datagram.Destination);
        }

        public override string GetDatagramInfo(EthernetDatagram datagram)
        {
            return string.Empty;
        }

        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            EthernetDatagram ethernetDatagram = (EthernetDatagram)datagram;

            return ethernetDatagram.EtherType switch 
            { 
                EthernetType.IpV4 => ethernetDatagram.IpV4,
                EthernetType.IpV6 => ethernetDatagram.IpV6,
                EthernetType.Arp => ethernetDatagram.Arp,
                EthernetType.VLanTaggedFrame => ethernetDatagram.VLanTaggedFrame,
                _ => ethernetDatagram.Payload
            };        
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            EthernetDatagram ethernetDatagram = (EthernetDatagram)datagram;

            return ethernetDatagram.EtherType switch
            {
                EthernetType.IpV4 => null,
                EthernetType.IpV6 => null,
                EthernetType.Arp => null,
                EthernetType.VLanTaggedFrame => null,
                _ => null
            };
        }

        public override string GetDatagramProtoclString(Datagram datagram)
        {
            return "Ethernet";
        }
    }
}
