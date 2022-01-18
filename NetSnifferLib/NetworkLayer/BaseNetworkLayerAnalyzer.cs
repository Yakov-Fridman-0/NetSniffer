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

namespace NetSnifferLib.NetworkLayer
{
    public abstract class BaseNetworkLayerAnalyzer<T> : BaseAnalyzer, INetworkLayerAnalyzer<T> where T : IpDatagram
    {
        protected IPAddress GetIPAddress(IpV4Address address)
        {
            return IPAddress.Parse(address.ToString());
        }

        protected IPAddress GetIPAddress(IpV6Address address)
        {
            return IPAddress.Parse(address.ToString());
        }

        public virtual string GetDatagramSourceString(T datagram)
        {
            return AddressConvert.ToString(GetDatagramSource(datagram));
        }

        public override string GetDatagramSourceString(Datagram datagram)
        {
            return GetDatagramSource(datagram).ToString();
        }

        public virtual string GetDatagramDestinationString(T datagram)
        {
            return GetDatagramDestination(datagram).ToString();
        }

        public override string GetDatagramDestinationString(Datagram datagram)
        {
            return GetDatagramDestinationString((T)datagram);
        }

        public abstract string GetDatagramInfo(T datagram);

        public override string GetDatagramInfo(Datagram datagram)
        {
            return GetDatagramInfo((T)datagram);
        }

        public abstract IPAddress GetDatagramSource(T datagram);

        public IPAddress GetDatagramSource(Datagram datagram)
        {
            return GetDatagramSource((T)datagram);
        }

        public abstract IPAddress GetDatagramDestination(T datagram);

        public IPAddress GetDatagramDestination(Datagram datagram)
        {
            return GetDatagramDestination((T)datagram);
        }
    }
}
