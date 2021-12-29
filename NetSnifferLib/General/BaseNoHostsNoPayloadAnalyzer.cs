using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    public abstract class BaseNoHostsNoPayloadAnalyzer : BaseAnalyzer
    {
        public override bool SupportsHosts => HostsNotSupported.SupportsHosts;

        public override string GetDatagramSourceString(Datagram datagram)
        {
            return HostsNotSupported.GetDatagramSourceString(datagram);
        }

        public override string GetDatagramDestinationString(Datagram datagram)
        {
            return HostsNotSupported.GetDatagramDestinationString(datagram);
        }

        public override bool SupportsPayload => PayloadNotSupported.SupportsPayload;

        public override Datagram GetDatagramPayload(Datagram datagram)
        {
            return PayloadNotSupported.GetDatagramPayload(datagram);
        }

        public override IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            return PayloadNotSupported.GetDatagramPayloadAnalyzer(datagram);
        }
    }
}
