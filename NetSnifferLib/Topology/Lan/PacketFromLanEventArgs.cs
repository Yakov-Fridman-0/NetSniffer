using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    class PacketFromLanEventArgs : EventArgs
    {
        public PacketFromLanEventArgs(
            IPAddress sourceIPAddress, PhysicalAddress sourcePhysicalAddress, 
            IPAddress destinationIPAddress, PhysicalAddress destinationPhysicalAddress)
        {
            SourceIPAddress = sourceIPAddress;
            SourcePhysicalAddress = sourcePhysicalAddress;
            DestinationIPAddress = destinationIPAddress;
            DestinationPhysicalAddress = destinationPhysicalAddress;
        }

        public IPAddress SourceIPAddress { get; set; }

        public PhysicalAddress SourcePhysicalAddress { get; set; }

        public IPAddress DestinationIPAddress { get; set; }

        public PhysicalAddress DestinationPhysicalAddress { get; set; }
    }
}
