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
        public Datagram GetDatagramPayload(Datagram datagram);

        public IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram);
    }
}
