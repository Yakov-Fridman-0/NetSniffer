using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    abstract class GenericHostsAnalyzer<TAddress, TDatagram> : GenericAnalyzer<TDatagram> where TDatagram: Datagram
    {
        protected abstract TAddress GetSource(TDatagram datagram);

        protected abstract TAddress GetDestination(TDatagram datagram);
    }
}
