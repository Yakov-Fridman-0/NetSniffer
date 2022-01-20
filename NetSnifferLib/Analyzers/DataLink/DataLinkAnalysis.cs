using NetSnifferLib.General;
using System.Net.NetworkInformation;
using PcapDotNet.Packets;

namespace NetSnifferLib.DataLink
{
    class DataLinkAnalysis : BaseAnalysis
    {
        readonly string _info;
        readonly PhysicalAddressContainer _source;
        readonly PhysicalAddressContainer _destination;
        readonly Datagram _payload;
        readonly IContext _context;
        readonly IAnalyzer _nextAnaluzer;

        public DataLinkAnalysis(
            string info, 
            PhysicalAddress source, 
            PhysicalAddress destination, 
            Datagram payload, 
            DataLinkContext context, 
            IAnalyzer nextAnalyzer) 
        {
            _info = info;
            _source = new PhysicalAddressContainer(source);
            _destination = new PhysicalAddressContainer(destination);
            _payload = payload;
            _context = context;
            _nextAnaluzer = nextAnalyzer;
        }

        public override string Info => _info;

        public override IAddress Source => _source;

        public override IAddress Destination => _destination;

        public override Datagram Payload => _payload;

        public override IContext PayloadContext => _context;

        public override IAnalyzer NextAnalyzer => _nextAnaluzer;
    }
}
