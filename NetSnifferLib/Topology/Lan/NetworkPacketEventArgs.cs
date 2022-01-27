using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    class NetworkPacketEventArgs : EventArgs
    {
        public NetworkPacketEventArgs(
            IPAddress sourceIPAddress, PhysicalAddress sourcePhysicalAddress, 
            IPAddress destinationIPAddress, PhysicalAddress destinationPhysicalAddress,
            int ttl)
        {
            SourceIPAddress = sourceIPAddress;
            SourcePhysicalAddress = sourcePhysicalAddress;
            DestinationIPAddress = destinationIPAddress;
            DestinationPhysicalAddress = destinationPhysicalAddress;
            TTL = ttl;
        }

        public IPAddress SourceIPAddress { get; set; }

        public PhysicalAddress SourcePhysicalAddress { get; set; }

        public IPAddress DestinationIPAddress { get; set; }

        public PhysicalAddress DestinationPhysicalAddress { get; set; }

        public int TTL { get; set; }
    }
}
