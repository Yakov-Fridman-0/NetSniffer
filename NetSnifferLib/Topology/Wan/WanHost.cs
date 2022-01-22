using System.Net;

namespace NetSnifferLib.Topology
{
    public class WanHost : IIpAddress
    {
        public IPAddress IpAddress { get; }

        private int _hopsNum = 0;
        private int _hopsSum = 0;

        public WanHost(IPAddress iPAddress)
        {
            IpAddress = iPAddress;
        }

        public WanHost(IPAddress iPAddress, int hops)
        {
            IpAddress = iPAddress;
            RegisterHops(hops);
        }

        public void RegisterHops(int hops)
        {
            _hopsNum++;
            _hopsSum += hops;
        }

        public int MeanHops => _hopsSum / _hopsNum;
    }
}
