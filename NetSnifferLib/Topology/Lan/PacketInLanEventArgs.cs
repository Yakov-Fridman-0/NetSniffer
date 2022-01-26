using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    class PacketInLanEventArgs : EventArgs
    {
        public PacketInLanEventArgs(PhysicalAddress source, PhysicalAddress destination)
        {
            Source = source;
            Destination = destination;
        }

        public PhysicalAddress Source { get; set; }

        public PhysicalAddress Destination { get; set; }
    }
}
