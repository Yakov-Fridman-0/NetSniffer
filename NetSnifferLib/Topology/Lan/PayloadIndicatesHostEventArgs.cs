using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    class PayloadIndicatesHostEventArgs : EventArgs
    {
        public PayloadIndicatesHostEventArgs(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            PhysicalAddress = physicalAddress;
            IPAddress = ipAddress;
        }

        public IPAddress IPAddress { get; }

        public PhysicalAddress PhysicalAddress { get; }
    }
}
