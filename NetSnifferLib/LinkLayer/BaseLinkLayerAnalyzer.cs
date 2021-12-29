using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets;
using NetSnifferLib.General;
using System.Net.NetworkInformation;

namespace NetSnifferLib.LinkLayer
{
    public abstract class BaseLinkLayerAnalyzer<T> : BaseAnalyzer, ILinkLayerAnalyzer<T> where T: Datagram
    {
        protected PhysicalAddress GetPhysicalAddress(MacAddress address)
        {
            return PhysicalAddress.Parse(address.ToString());
        }

        public abstract string GetDatagramInfo(T datagram);

        public override string GetDatagramInfo(Datagram datagram)
        {
            return GetDatagramInfo((T)datagram);
        }

        public abstract PhysicalAddress GetDatagramSource(T datagram);

        public PhysicalAddress GetDatagramSource(Datagram datagram)
        {
            return GetDatagramSource((T)datagram);
        }

        public abstract PhysicalAddress GetDatagramDestination(T datagram);

        public PhysicalAddress GetDatagramDestination(Datagram datagram)
        {
            return GetDatagramDestination((T)datagram);
        }

        public virtual string GetDatagramSourceString(T datagram)
        {
            return GetDatagramSource(datagram).ToString();
        }

        public override string GetDatagramSourceString(Datagram datagram)
        {
            return GetDatagramSourceString((T)datagram);
        }

        public virtual string GetDatagramDestinationString(T datagram)
        {
            return GetDatagramDestination(datagram).ToString();
        }

        public override string GetDatagramDestinationString(Datagram datagram)
        {
            return GetDatagramDestinationString((T)datagram);
        }
    }
}
