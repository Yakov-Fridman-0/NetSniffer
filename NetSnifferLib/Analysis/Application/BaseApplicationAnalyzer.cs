using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.Transport;

namespace NetSnifferLib.Analysis.Application
{
    abstract class BaseApplicationAnalyzer<TDatagram, TTransportDatagram> : BaseAnalyzer<TDatagram, TransportContext>, IApplicationAnalyzer 
        where TDatagram : Datagram
        where TTransportDatagram : TransportDatagram
    {
        protected abstract bool TryGetDatagramCore(TTransportDatagram transportDatagram, ref TDatagram datagram);

        public bool TryGetDatagram(TransportDatagram transportDatagram, ref Datagram datagram)
        {
            var castedDatagram = (TDatagram)datagram;
            bool result =  TryGetDatagramCore((TTransportDatagram)transportDatagram, ref castedDatagram);
            datagram = castedDatagram;

            return result;
        }

        protected override IAnalysis AnalyzeDatagramCore(TDatagram datagram, TransportContext context)
        {
            var analysis = new ApplicationAnalysis();
            analysis.AddInfo(GetInfo(datagram, context));

            return analysis;
        }
    }
}
