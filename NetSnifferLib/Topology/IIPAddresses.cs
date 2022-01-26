using System.Collections.Generic;
using System.Net;

namespace NetSnifferLib.Topology
{
    interface IIPAddresses
    {
        List<IPAddress> IPAddresses { get; }
    }
}
