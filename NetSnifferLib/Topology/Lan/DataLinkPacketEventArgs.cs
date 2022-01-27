using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    class DataLinkPacketEventArgs : EventArgs
    {
        public DataLinkPacketEventArgs(PhysicalAddress source, PhysicalAddress destination)
        {
            Source = source;
            Destination = destination;
        }

        public PhysicalAddress Source { get; set; }

        public PhysicalAddress Destination { get; set; }
    }
}
