using System.Collections.Generic;
using System.Net;

namespace NetSnifferLib.Topology
{
    public class WanHost : IIPAddress
    {
        public IPAddress IPAddress { get; }

        private int _hopsNum = 0;
        private int _hopsSum = 0;

        public WanHost(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
        }

        public WanHost(IPAddress ipAddress, int hops)
        {
            IPAddress = ipAddress;
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
