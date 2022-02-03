using System;
using System.Net;

using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.Transport;

namespace NetSnifferLib.Analysis.Application
{
    abstract class BaseClientServerApplicationAnalyzer<TDatagram, TTransportDatagram> : BaseApplicationAnalyzer<TDatagram, TTransportDatagram>
        where TDatagram : Datagram
        where TTransportDatagram : TransportDatagram
    {
        public EventHandler<IPAddress> ServerDetected;

        protected abstract bool IsResponse(TDatagram datagram);

        protected override IAnalysis AnalyzeDatagramCore(TDatagram datagram, Transport.TransportContext context)
        {
            if (IsResponse(datagram))
            {
                var sourceIP = context.Source.IpEndPoint.Address;
                ServerDetected?.Invoke(this, sourceIP);
            }

            return base.AnalyzeDatagramCore(datagram, context);
        }
    }
}
