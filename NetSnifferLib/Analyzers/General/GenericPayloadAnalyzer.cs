using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    abstract class GenericPayloadAnalyzer<T> : GenericAnalyzer<T> where T: Datagram
    {
         
    }
}
