using System.Collections.Generic;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.DataLink;
using NetSnifferLib.Analysis.Network;
using NetSnifferLib.Analysis.Transport;
using NetSnifferLib.Analysis.Application;
using NetSnifferLib.Analysis.Miscellaneous;

namespace NetSnifferLib.Analysis
{
    static class DatagramAnalyzer
    {
        public static EthernetAnalyzer EthernetAnalyzer { get; } = new();

        public static IDataLinkAnalyzer[] DataLinkAnalyzers { get; } =
            new IDataLinkAnalyzer[] { EthernetAnalyzer };

        public static ArpAnalyzer ArpAnalyzer { get; } = new();

        public static IpV4Analyzer IpV4Analyzer { get; } = new();

        public static IpV6Analyzer IpV6Analyzer { get; } = new();

        public static INetworkAnalyzer[] NetworkAnalyzers { get; } =
            new INetworkAnalyzer[] { IpV4Analyzer, IpV6Analyzer };

        public static IcmpAnalyzer IcmpAnalyzer { get; } = new();

        public static UdpAnalyzer UdpAnalyzer { get; } = new();

        public static TcpAnalyzer TcpAnalyzer { get; } = new();

        public static ITransportAnalyzer[] TransportAnalyzers { get; } =
            new ITransportAnalyzer[] { UdpAnalyzer, TcpAnalyzer };

        public static DnsAnalyzer DnsAnalyzer { get; } = new();

        public static DhcpAnalyzer DhcpAnalyzer { get; } = new();

        public static Dictionary<IApplicationAnalyzer, IPortsMatch> AnalyzersOverUdp { get; } = new()
        {
            { DhcpAnalyzer, new KnownPortPair(67, 68)},
            { DnsAnalyzer, new KnownPort(53) }
        };
    }
}
