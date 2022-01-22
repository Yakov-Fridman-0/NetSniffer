using System;
using System.Net.NetworkInformation;

using PcapDotNet.Packets;

using NetSnifferLib.General;

namespace NetSnifferLib.Analysis.DataLink
{
    abstract class BaseDataLinkAnalyzer<T> : BaseAnalyzer<T, EmptyContext>, IDataLinkAnalyzer 
        where T : Datagram
    {
        public int SentPackets { get; protected set; } = 0;

        public int SentBytes { get; protected set; } = 0;

        public event EventHandler<PhysicalAddress> HostDetected;

        protected abstract PhysicalAddress GetSource(T datagram);

        protected abstract PhysicalAddress GetDestination(T datagram);

        protected abstract Datagram GetPayloadAndAnalyzer(T datagram, out IAnalyzer analyzer);

        protected abstract int GetPayloadLength(T datagram);

        protected override DataLinkAnalysis AnalyzeDatagramCore(T datagram, EmptyContext context)
        {
            SentPackets++;
            SentBytes += GetPayloadLength(datagram);

            var analysis = new DataLinkAnalysis();

            var info = GetInfo(datagram, context);
            analysis.AddInfo(info);

            var source = GetSource(datagram);
            var destination = GetDestination(datagram);

            HostDetected?.Invoke(this, source);
            HostDetected?.Invoke(this, destination);

            var sourceContainer = (PhysicalAddressContainer)AddressConvert.ToIAddress(source);
            var destinationContainer = (PhysicalAddressContainer)AddressConvert.ToIAddress(destination);

            analysis.AddHostsInfo(sourceContainer, destinationContainer);

            var payoad = GetPayloadAndAnalyzer(datagram, out IAnalyzer analyzer);
            var payloadContext = new DataLinkContext(sourceContainer, destinationContainer);

            analysis.AddPayloadInfo(payoad, payloadContext, analyzer);

            return analysis;
        }
    }
}
