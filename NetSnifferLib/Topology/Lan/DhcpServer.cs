using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.Topology
{
    public class DhcpServer : LanHost
    {
        public DhcpServer(PhysicalAddress physicalAddress, IPAddress iPAddress) : base(physicalAddress, iPAddress)
        {

        }
    }
}
