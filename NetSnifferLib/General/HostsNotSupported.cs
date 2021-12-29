using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    public static class HostsNotSupported
    {
        public static bool SupportsHosts => false;

        public static string GetDatagramSourceString(Datagram datagram)
        {
            return null;
        }

        public static string GetDatagramDestinationString(Datagram datagram)
        {
            return null;
        }
    }
}
