using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using NetSnifferLib.General;
using System.Net.NetworkInformation;

namespace NetSnifferLib.LinkLayer
{
    interface ILinkLayerAnalyzer<T>: IAnalyzer where T : Datagram
    {
        public PhysicalAddress GetDatagramSource(T datagram);

        public PhysicalAddress GetDatagramDestination(T datagram);
    }
}
