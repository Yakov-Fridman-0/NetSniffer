using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSnifferLib.General;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ip;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.IpV6;
using System.Net;
using NetSnifferLib.DataLink;

namespace NetSnifferLib.Network
{
    abstract class BaseNetworkAnalyzer<T> : IAnalyzer where T: IpDatagram
    {
        public abstract string Protocol { get; }

        public General.Layer Layer => General.Layer.Network;

        public abstract string GetDatagramInfo(T datagram);

        public abstract IPAddress GetDatagramSource(T datagram);

        public IPAddress GetDatagramSource(Datagram datagram)
        {
            return GetDatagramSource((T)datagram);
        }

        protected abstract IPAddress GetDatagramDestination(T datagram);

        public IPAddress GetDatagramDestination(Datagram datagram)
        {
            return GetDatagramDestination((T)datagram);
        }

        public NetworkAnalysis AnalyzeDatagram(Datagram datagram, IContext context)
        {
            return null;
        }
    }
}
