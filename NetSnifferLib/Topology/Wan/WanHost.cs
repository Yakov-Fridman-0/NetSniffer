using System.Collections.Generic;
using System.Net;

namespace NetSnifferLib.Topology
{
    public class WanHost : IIPAddresses
    {
        public IPAddress IpAddress { get; }

        private int _hopsNum = 0;
        private int _hopsSum = 0;

        public WanHost(IPAddress ipAddress)
        {
            IpAddress = ipAddress;
        }

        public WanHost(IPAddress ipAddress, int hops)
        {
            IpAddress = ipAddress;
            RegisterHops(hops);
        }

        public void RegisterHops(int hops)
        {
            _hopsNum++;
            _hopsSum += hops;
        }

        public int MeanHops => _hopsSum / _hopsNum;

        public List<IPAddress> IPAddresses { get; set; }
    }
}
