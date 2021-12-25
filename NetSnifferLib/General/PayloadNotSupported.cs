using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    public static class PayloadNotSupported
    {
        public static bool SupportsPayload => false;

        public static Datagram GetDatagramPayload(Datagram datagram)
        {
            return null;
        }

        public static IAnalyzer GetDatagramPayloadAnalyzer(Datagram datagram)
        {
            return null;
        }
    }
}
