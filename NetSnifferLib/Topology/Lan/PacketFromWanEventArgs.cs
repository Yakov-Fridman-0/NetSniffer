using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    class PacketFromWanEventArgs : PacketFromLanEventArgs
    {
        public PacketFromWanEventArgs(
            IPAddress sourceIPAddress, PhysicalAddress sourcePhysicalAddress,
            IPAddress destinationIPAddress, PhysicalAddress destinationPhysicalAddress,
            int ttl) : base(sourceIPAddress, sourcePhysicalAddress, destinationIPAddress, destinationPhysicalAddress)
        {
            TTL = ttl;
        }

        public int TTL { get; set; }
    }
}
