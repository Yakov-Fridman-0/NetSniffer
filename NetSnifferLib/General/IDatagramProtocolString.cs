using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    public interface IDatagramProtocolString
    {
        public string GetDatagramProtoclString(Datagram datagram);
    }
}
