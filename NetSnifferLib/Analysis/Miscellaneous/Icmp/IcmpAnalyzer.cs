using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSnifferLib.Analysis;

using PcapDotNet.Packets.Ip;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Icmp;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.Network;
using System.Diagnostics.CodeAnalysis;

namespace NetSnifferLib.Analysis.Miscellaneous
{
    class IcmpAnalyzer : BaseAnalyzer<IcmpDatagram, NetworkContext>
    {
        class IPAddressEqualityComparer : IEqualityComparer<IPAddress>
        {
            public bool Equals(IPAddress x, IPAddress y)
            {
                return x.Equals(y);
            }

            public int GetHashCode([DisallowNull] IPAddress obj)
            {
                return obj.GetHashCode();
            }
        }

        public override string Protocol => "ICMP";

        public EventHandler<PingReplyEventArgs> RegisteredPingReply;

        public EventHandler<PingRequestTimeToLiveExeededEventArgs> RegisteredPingRequestTimeToLiveExceeded;

        readonly Dictionary<IPAddress, Dictionary<uint, int>> registeredPings = new(new IPAddressEqualityComparer());

        public void RegisterPingRequest(IPAddress destination, uint identifier, int num)
        {
            if (registeredPings.TryGetValue(destination, out Dictionary<uint, int> ids))
            {
                ids.Add(identifier, num);
            }    
            else
            {
                registeredPings.Add(destination, new Dictionary<uint, int>() { { identifier, num } });
            }    
        }

        protected override string GetInfo(IcmpDatagram datagram, NetworkContext context)
        {
            return "";
        }

        protected override IcmpAnalysis AnalyzeDatagramCore(IcmpDatagram datagram, NetworkContext context, int packetId)
        {
            var analysis = new IcmpAnalysis();
            analysis.AddInfo(GetInfo(datagram, context));

            var source = context.Source.IPAddress;
            var destination = context.Destination.IPAddress;

            uint id;
            Dictionary<uint, int> registeredIds;

            if (destination.Equals(PacketAnalyzer.Analyzer.LocalComputerIPAddress))
            {
                switch (datagram.MessageType)
                {
                    case IcmpMessageType.EchoReply:
                        id = datagram.Variable;
                        if (registeredPings.TryGetValue(source, out registeredIds))
                        {
                            if(registeredIds.ContainsKey(id))
                            {
                                registeredIds.Remove(id);
                                RegisteredPingReply?.Invoke(this, new PingReplyEventArgs(source, id));
                            }
                        }
                        break;
                    case IcmpMessageType.TimeExceeded:
                        if (datagram.Payload is IpV4Datagram ipV4Datagram)
                        {
                            if (ipV4Datagram.Payload is IcmpDatagram icmpDatagram)
                            {
                                if (icmpDatagram.MessageType == IcmpMessageType.Echo)
                                {
                                    id = icmpDatagram.Variable;

                                    if (registeredPings.TryGetValue(source, out registeredIds))
                                    {
                                        if(registeredIds.ContainsKey(id))
                                        {
                                            var expretedSource = AddressConvert.ToIPAddress(ipV4Datagram.Source);
                                            int hops = ipV4Datagram.Ttl;


                                            registeredIds[id]--;

                                            if (registeredIds[id] == 0)
                                                registeredIds.Remove(id);

                                            RegisteredPingRequestTimeToLiveExceeded?.Invoke(this, 
                                                new PingRequestTimeToLiveExeededEventArgs(source, expretedSource, id, hops));
                                        }
                                    }
                                }
                                
                            }
                        }
                        break;
                }
            }

            return analysis;
        }
    }
}
