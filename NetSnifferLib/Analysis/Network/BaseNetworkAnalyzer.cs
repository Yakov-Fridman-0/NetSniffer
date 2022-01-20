using System.Net;

using PcapDotNet.Packets;
using PcapDotNet.Packets.Ip;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.DataLink;

namespace NetSnifferLib.Analysis.Network
{
    abstract class BaseNetworkAnalyzer<T> : BaseAnalyzer<T, DataLinkContext>, INetworkAnalyzer where T: IpDatagram
    {
        protected abstract IPAddress GetSource(T datagram);

        protected abstract IPAddress GetDestination(T datagram);

        protected abstract Datagram GetPayloadAndAnalyzer(T datagram, out IAnalyzer analyzer);

        protected override NetworkAnalysis AnalyzeDatagramCore(T datagram, DataLinkContext context)
        {
            var analysis = new NetworkAnalysis();

            var info = GetInfo(datagram);
            analysis.AddInfo(info);

            var source = GetSource(datagram);
            var destination = GetDestination(datagram);

            var sourceContainer = (IpAddressContainer)AddressConvert.ToIAddress(source);
            var destinationContainer = (IpAddressContainer)AddressConvert.ToIAddress(destination);

            analysis.AddHostsInfo(sourceContainer, destinationContainer);

            var payoad = GetPayloadAndAnalyzer(datagram, out IAnalyzer analyzer);
            var payloadContext = new NetworkContext(sourceContainer, destinationContainer);

            analysis.AddPayloadInfo(payoad, payloadContext, analyzer);

            return analysis;
        }
    }
}
