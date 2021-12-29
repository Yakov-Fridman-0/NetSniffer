using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    public interface IDatagramPayload
    {
        public abstract Datagram GetDatagramPayload(Datagram datagram);

        public abstract IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram);

        public bool SupportsPayload
        {
            get;
        }
    }
}
