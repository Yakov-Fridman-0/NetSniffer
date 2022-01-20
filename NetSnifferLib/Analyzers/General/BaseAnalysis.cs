using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    abstract class BaseAnalysis : IAnalysis
    {
        public virtual string Info => "";

        public virtual IAddress Source => null;

        public virtual IAddress Destination => null;

        public bool HasAddresses => Source != null;

        public virtual Datagram Payload => null;

        public virtual IContext PayloadContext => null;

        public virtual IAnalyzer NextAnalyzer => null;

        public bool HasPayload => Payload != null;
    }
}
