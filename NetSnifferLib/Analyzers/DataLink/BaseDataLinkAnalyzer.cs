using PcapDotNet.Packets;
using NetSnifferLib.General;
using System.Net.NetworkInformation;

namespace NetSnifferLib.DataLink
{
    abstract class BaseDataLinkAnalyzer<T> : GenericAnalyzer<T> where T: Datagram
    {
        abstract PhysicalAddress GetSource(T datagram);

        public abstract PhysicalAddress GetDestination(T datagram);

        public override DataLinkAnalysis AnalyzeDatagram(T datagram, IContext context)
        {
            var analysis = new LinkLayerAnalysis("");
            return analysis;
        }

        public override string Protocol { get; }
    }
}
