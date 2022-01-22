using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    class LanMapHandler
    {
        public LanMapBuilder LanMap { get; } = new();

        public void HostDetectedHandler(object sender, PhysicalAddress address)
        {

        }

        public void HostDetectedHandler(object sender, LanHost host)
        {

        }
    }
}
