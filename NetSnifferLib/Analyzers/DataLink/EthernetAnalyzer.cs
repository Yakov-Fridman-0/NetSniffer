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

namespace NetSnifferLib.DataLink
{
    class EthernetAnalyzer : BaseDataLinkAnalyzer<EthernetDatagram>
    {
        public override PhysicalAddress GetSource(EthernetDatagram datagram)
        {
            return AddressConvert.ToPhysicalAddress(datagram.Source);
        }

        public override PhysicalAddress GetDestination(EthernetDatagram datagram)
        {
            return AddressConvert.ToPhysicalAddress(datagram.Destination);
        }

        public override string GetInfo(EthernetDatagram datagram)
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
                EthernetType.IpV4 => DatagramAnalyzer.IpV4Analyzer,
                EthernetType.IpV6 => DatagramAnalyzer.IpV6Analyzer,
                EthernetType.Arp => DatagramAnalyzer.ArpAnalyzer,
                _ => null
            };
        }

        public override string ProtocolString => "Etherent";
    }
}
