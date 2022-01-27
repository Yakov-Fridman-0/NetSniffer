using System;
using System.Net;

using PcapDotNet.Packets;
using PcapDotNet.Packets.Ip;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.DataLink;
using NetSnifferLib.Topology;

namespace NetSnifferLib.Analysis.Network
{
    abstract class BaseNetworkAnalyzer<T> : BaseAnalyzer<T, DataLinkContext>, INetworkAnalyzer where T: IpDatagram
    {
        public int SentPackets { get; protected set; } = 0;

        public int SentBytes { get; protected set; } = 0;

        public EventHandler<NetworkPacketEventArgs> PacketCaptured;

        //public EventHandler<NetworkPacketEventArgs> PacketFromWan;

        protected abstract bool IsFromLan(T datagram);

        protected abstract int GetPayloadLength(T datgram);

        protected abstract IPAddress GetSource(T datagram);

        protected abstract IPAddress GetDestination(T datagram);

        protected abstract int GetTTL(T daragram);

        protected abstract Datagram GetPayloadAndAnalyzer(T datagram, out IAnalyzer analyzer);

        protected override NetworkAnalysis AnalyzeDatagramCore(T datagram, DataLinkContext context)
        {
            SentPackets++;
            SentBytes += GetPayloadLength(datagram);

            var analysis = new NetworkAnalysis();

            var info = GetInfo(datagram, context);
            analysis.AddInfo(info);

            var source = GetSource(datagram);
            var destination = GetDestination(datagram);

            var sourcePhysicalAddress = context.Source.PhysicalAddress;
            var destinationPhysicalAddress = context.Destination.PhysicalAddress;

            var ttl = GetTTL(datagram);

            PacketCaptured?.Invoke(this, new NetworkPacketEventArgs(source, sourcePhysicalAddress, destination, destinationPhysicalAddress, ttl));

            //if (IsFromLan(datagram))
            //{
            //    PacketCaptured?.Invoke(this, new NetworkPacketEventArgs(source, sourcePhysicalAddress, destination, destinationPhysicalAddress, ttl));
            //}
            //else
            //{
            //    PacketFromWan?.Invoke(this, new NetworkPacketEventArgs(source, sourcePhysicalAddress, destination, destinationPhysicalAddress, ttl));
            //}

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
