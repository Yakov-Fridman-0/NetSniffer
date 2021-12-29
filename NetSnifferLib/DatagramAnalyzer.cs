using NetSnifferLib.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib
{
    public static class DatagramAnalyzer
    {
        static readonly LinkLayer.EthernetAnalyzer _ethernetAnalyzer;

        static readonly Miscellaneous.ArpAnalyzer _arpAnalyzer;

        static readonly NetworkLayer.IpV4Analyzer _ipV4Analyzer;
        static readonly NetworkLayer.IpV6Analyzer _ipV6Analyzer;

        static readonly TransportLayer.UdpAnalyzer _udpAnalyzer;
        static readonly TransportLayer.TcpAnalyzer _tcpAnalyzer;

        static readonly DnsAnalyzer _dnsAnalyzer;

        static readonly HttpAnalyzer _httpAnalyzer;

        static readonly BootpAnalyzer bootpAnalyzer;

        static readonly DhcpAnalyzer dhcpAnalyzer;

        static DatagramAnalyzer()
        {
            _ethernetAnalyzer = new LinkLayer.EthernetAnalyzer();

            _arpAnalyzer = new Miscellaneous.ArpAnalyzer();

            _ipV4Analyzer = new NetworkLayer.IpV4Analyzer();
            _ipV6Analyzer = new NetworkLayer.IpV6Analyzer();

            _udpAnalyzer = new TransportLayer.UdpAnalyzer();
            _tcpAnalyzer = new TransportLayer.TcpAnalyzer();

            _dnsAnalyzer = new DnsAnalyzer();

            _httpAnalyzer = new HttpAnalyzer();

            bootpAnalyzer = new BootpAnalyzer();

            dhcpAnalyzer = new DhcpAnalyzer();
        }

        public static LinkLayer.EthernetAnalyzer EthernetAnalyzer => _ethernetAnalyzer;

        public static Miscellaneous.ArpAnalyzer ArpAnalyzer => _arpAnalyzer;

        public static NetworkLayer.IpV4Analyzer IpV4Analyzer => _ipV4Analyzer;

        public static NetworkLayer.IpV6Analyzer IpV6Analyzer => _ipV6Analyzer;

        public static TransportLayer.UdpAnalyzer UdpAnalyzer => _udpAnalyzer;

        public static TransportLayer.TcpAnalyzer TcpAnalyzer => _tcpAnalyzer;

        public static Miscellaneous.DnsAnalyzer DnsAnalyzer => _dnsAnalyzer;

        public static Miscellaneous.HttpAnalyzer HttpAnalyzer => _httpAnalyzer;

        public static DhcpAnalyzer DhcpAnalyzer => dhcpAnalyzer;

        internal static BootpAnalyzer BootpAnalyzer => bootpAnalyzer;
    }
}
