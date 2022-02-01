using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    class TopologyBuilder
    {
        readonly LanMapBuilder lanMapBuilder = new();
        readonly WanMapBuilder wanMapBuilder = new();

        public void AddHost(PhysicalAddress physicalAddress)
        {
            lanMapBuilder.AddHost(physicalAddress);
        }

        public void AddHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            bool? isRemote = null;
            IPAddress remoteIPAddress = null;

            lanMapBuilder.AddHost(physicalAddress, ipAddress, ref isRemote, ref remoteIPAddress);

            if (isRemote.HasValue && isRemote.Value)
            {
                wanMapBuilder.Add
            }
        }
    }
}
