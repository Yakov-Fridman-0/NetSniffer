using NetSnifferLib.General;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using System.Net.NetworkInformation;
   
namespace NetSnifferLib.Analysis.DataLink
{
    class EthernetAnalyzer : BaseDataLinkAnalyzer<EthernetDatagram>
    {
        public override string Protocol => "Ethernet";

        protected override string GetInfo(EthernetDatagram datagram, EmptyContext context)
        {
            return string.Empty;
        }

        protected override PhysicalAddress GetSource(EthernetDatagram datagram)
        {
            return AddressConvert.ToPhysicalAddress(datagram.Source);
        }

        protected override PhysicalAddress GetDestination(EthernetDatagram datagram)
        {
            return AddressConvert.ToPhysicalAddress(datagram.Destination);
        }

        protected override int GetPayloadLength(EthernetDatagram datagram)
        {
            return datagram.PayloadLength;
        }

        protected override Datagram GetPayloadAndAnalyzer(EthernetDatagram datagram, out IAnalyzer analyzer)
        {
            Datagram payload;

            switch (datagram.EtherType)
            {
                case EthernetType.Arp:
                    payload = datagram.Arp;
                    analyzer = DatagramAnalyzer.ArpAnalyzer;
                    break;
                case EthernetType.IpV4:
                    payload = datagram.IpV4;
                    analyzer = DatagramAnalyzer.IpV4Analyzer;
                    break;
                case EthernetType.IpV6:
                    payload = datagram.IpV6;
                    analyzer = DatagramAnalyzer.IpV6Analyzer;
                    break;
                default:
                    payload = datagram.Payload;
                    analyzer = null;
                    break;
            }

            return payload;        
        }
    }
}
