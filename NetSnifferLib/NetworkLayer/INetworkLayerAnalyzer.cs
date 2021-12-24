using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSnifferLib;
using PcapDotNet.Packets.Ip;
using NetSnifferLib.General;
using System.Net;

namespace NetSnifferLib.NetworkLayer
{
    interface INetworkLayerAnalyzer<T> : IAnalyzer where T: IpDatagram
    {
        public IPAddress GetDatagramSource(T datagram);

        public IPAddress GetDatagramDestination(T datagram);
    }
}
