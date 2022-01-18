using NetSnifferLib.LinkLayer;
using NetSnifferLib.NetworkLayer;
using NetSnifferLib.TransportLayer;
using NetSnifferLib.Miscellaneous;

namespace NetSnifferLib
{
    public static class DatagramAnalyzer
    {
        public static EthernetAnalyzer EthernetAnalyzer { get; } = new();

        public static ArpAnalyzer ArpAnalyzer { get; } = new();

        public static IpV4Analyzer IpV4Analyzer { get; } = new();
        public static IpV6Analyzer IpV6Analyzer { get; } = new();

        public static UdpAnalyzer UdpAnalyzer { get; } = new();
        public static TcpAnalyzer TcpAnalyzer { get; } = new();

        public static DnsAnalyzer DnsAnalyzer { get; } = new();

        public static HttpAnalyzer HttpAnalyzer { get; } = new();


        public static DhcpAnalyzer DhcpAnalyzer { get; } = new();
    }
}
