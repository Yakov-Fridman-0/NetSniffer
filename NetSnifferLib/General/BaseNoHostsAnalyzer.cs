using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.General
{
    public abstract class BaseNoHostsAnalyzer : BaseAnalyzer
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
    }
}
