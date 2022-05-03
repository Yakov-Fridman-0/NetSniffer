using System.Net;

using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.Network;

namespace NetSnifferLib.Analysis.Transport
{
    abstract class BaseTransportAnalyzer<T> : BaseAnalyzer<T, NetworkContext>, ITransportAnalyzer 
        where T : TransportDatagram
    {
        public int SentPackets { get; protected set; } = 0;

        public int SentBytes { get; protected set; } = 0;

        protected virtual ushort GetSourcePortCore(T datagram)
        {
            return datagram.SourcePort;
        }

        public ushort GetSourcePort(TransportDatagram datagram)
        {
            return GetSourcePortCore((T)datagram);
        }

        protected virtual ushort GetDestinationPortCore(T datagram)
        {
            return datagram.DestinationPort;
        }

        public ushort GetDestinationPort(TransportDatagram datagram)
        {
            return GetDestinationPortCore((T)datagram);
        }

        protected virtual int GetPayloadLength(T datagram)
        {
            return datagram.Payload.Length;
        }

        protected abstract Datagram GetPayloadAndAnalyzer(T datagram, out IAnalyzer analyzer);

        protected virtual void SpecificAnalysis(T datagram, NetworkContext context, int packetId)
        {

        }

        protected override TransportAnalysis AnalyzeDatagramCore(T datagram, NetworkContext context, int packetId)
        {
            SentPackets++;
            SentBytes += GetPayloadLength(datagram);

            SpecificAnalysis(datagram, context, packetId);

            var analysis = new TransportAnalysis();

            var info = GetInfo(datagram, context);
            analysis.AddInfo(info);


            var sourcePort = GetSourcePortCore(datagram);
            var sourceIpEndPoint = new IPEndPoint(context.Source.IPAddress, sourcePort);

            var destinationPort = GetDestinationPortCore(datagram);
            var destinationIpEndPoint = new IPEndPoint(context.Destination.IPAddress, destinationPort);

            var sourceIpEndPointContainer = (IpEndPointContainer)AddressConvert.ToIAddress(sourceIpEndPoint);
            var destinationIpEndPointContainer = (IpEndPointContainer)AddressConvert.ToIAddress(destinationIpEndPoint);
            analysis.AddHostsInfo(sourceIpEndPointContainer, destinationIpEndPointContainer);
            
            var payloadContext = new TransportContext(sourceIpEndPointContainer, destinationIpEndPointContainer);

            var payloadDatagram = GetPayloadAndAnalyzer(datagram, out IAnalyzer analyzer);
            analysis.AddPayloadInfo(payloadDatagram, payloadContext, analyzer);

            return analysis;
        }
    }
}
