using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    class WanMapBuilder
    {
        WanHost LocalComputer;

        readonly List<WanHost> hosts = new();

        readonly List<WanHost> dnsServers = new();

        readonly List<WanHost> lanRouters = new();

        readonly List<WanHost> wanRouters = new();

        public void AddHost(IPAddress ipAddress)
        {
            hosts.Add(new WanHost(ipAddress));
        }

        public bool ContainsHost(IPAddress ipAddress)
        {
            return hosts.Any((host) => ipAddress.Equals(host.IPAddress));
        }

        public bool RemoveHost(IPAddress ipAddress)
        {
            return hosts.Remove(hosts.Find((host) => ipAddress.Equals(host.IPAddress)));
        }

        public void AddDnsServer(IPAddress ipAddress)
        {
            dnsServers.Add(new DnsServer(ipAddress));
        }

        public bool ContainseDnsServer(IPAddress ipAddress)
        {
            return dnsServers.Any((dnsServer) => ipAddress.Equals(dnsServer.IPAddress));
        }

        public void IntegrateTracertResults(TracertResults tracertResults)
        {
            var addresses = tracertResults.ToList();
            int n = addresses.Count;

            IPAddress currAddr, nextAddr;
            WanHost currHost, nextHost = null;

            for (int i = 0; i < n - 1; i++) 
            {
                currAddr = addresses[i];
                nextAddr = addresses[i + 1];

                if (currAddr != null)
                {
                    currHost = hosts.FirstOrDefault((aHost) => aHost.IPAddress.Equals(currAddr));

                    if (currHost == null)
                    {
                        currHost = new WanHost(currAddr);
                        hosts.Add(currHost);
                    }

                    switch (i)
                    {
                        case 0:
                            LocalComputer = currHost;
                            break;
                        case 1:
                            lanRouters.Add(currHost);
                            break;
                        default:
                            wanRouters.Add(currHost);
                            break;
                    }

                    if (nextAddr != null)
                    {
                        nextHost = hosts.FirstOrDefault((aHost) => aHost.IPAddress.Equals(nextAddr));

                        if (nextHost == null)
                        {
                            nextHost = new WanHost(nextAddr);
                            hosts.Add(nextHost);
                        }

                        if (!currHost.ConnectedHosts.Contains(nextHost, WanHost.IPAddressComparer))
                            currHost.ConnectedHosts.Add(nextHost);

                        if (!nextHost.ConnectedHosts.Contains(currHost, WanHost.IPAddressComparer))
                            nextHost.ConnectedHosts.Add(currHost);
                    }

                    
                }
            }
        }

        public WanMap WanMap => new(hosts, lanRouters, wanRouters, dnsServers);
    }
}
