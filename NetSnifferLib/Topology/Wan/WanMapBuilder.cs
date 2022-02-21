using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        readonly Dictionary<IPAddress, ManualResetEvent> hostCreatedEvents = new(IPAddressHelper.EqulityComparer);

        public void AddHost(IPAddress ipAddress)
        {
            ManualResetEvent hostCreatedEvent = null;
            
            lock (hostCreatedEvents)
            {
                if (!hostCreatedEvents.ContainsKey(ipAddress))
                {
                    hostCreatedEvent = new ManualResetEvent(false);
                    hostCreatedEvents.Add(ipAddress, hostCreatedEvent);
                }
                else
                {
                    hostCreatedEvent = hostCreatedEvents[ipAddress];
                }
            }

            hosts.Add(new WanHost(ipAddress));
            hostCreatedEvent.Set();
        }

        public bool ContainsHost(IPAddress ipAddress)
        {
            return hosts.Any((host) => ipAddress.Equals(host.IPAddress));
        }

        public bool RemoveHost(IPAddress ipAddress)
        {
            return hosts.Remove(hosts.Find((host) => ipAddress.Equals(host.IPAddress)));
        }

        private WanHost GetHost(IPAddress ipAddress)
        {
            ManualResetEvent hostCreatedEvent = null;

            lock (hostCreatedEvents)
            {
                if (!hostCreatedEvents.ContainsKey(ipAddress))
                {
                    hostCreatedEvent = new ManualResetEvent(false);
                    hostCreatedEvents.Add(ipAddress, hostCreatedEvent);
                }
                else
                {
                    hostCreatedEvent = hostCreatedEvents[ipAddress];
                }
            }

            hostCreatedEvent.WaitOne();

            return hosts.Find((host) => ipAddress.Equals(host.IPAddress));
        }

        public void AddDnsServer(IPAddress ipAddress)
        {
            var host = GetHost(ipAddress);
            dnsServers.Add(hosts.Find((host) => ipAddress.Equals(host.IPAddress)));
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
                    //currHost = hosts.FirstOrDefault((aHost) => aHost.IPAddress.Equals(currAddr));
                    currHost = GetHost(currAddr);

                    if (currHost == null)
                    {
                        //currHost = new WanHost(currAddr);
                        //hosts.Add(currHost);
                        AddHost(currAddr);
                        currHost = GetHost(currAddr);

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
                        //nextHost = hosts.FirstOrDefault((aHost) => aHost.IPAddress.Equals(nextAddr));
                        nextHost = GetHost(nextAddr);

                        if (nextHost == null)
                        {
                            //nextHost = new WanHost(nextAddr);
                            //hosts.Add(nextHost);
                            AddHost(nextAddr);
                            nextHost = GetHost(nextAddr);
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
