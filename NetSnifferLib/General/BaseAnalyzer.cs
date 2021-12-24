using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.General
{
    public abstract class BaseAnalyzer : IAnalyzer
    {
        public virtual bool SupportsHosts => true;

        public abstract string GetDatagramSourceString(Datagram datagram);

        public abstract string GetDatagramDestinationString(Datagram datagram);

        public abstract string GetDatagramInfo(Datagram datagram);


        public abstract Datagram GetDatagramPayload(Datagram datagram);

        public abstract IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram);

        public abstract string GetDatagramProtoclString(Datagram datagram);
    }
}
