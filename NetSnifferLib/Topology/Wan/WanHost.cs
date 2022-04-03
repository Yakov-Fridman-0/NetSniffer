using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;

using NetSnifferLib;
using System.Diagnostics.CodeAnalysis;

namespace NetSnifferLib.Topology
{
    public class WanHost : IIPAddress, ICloneable, IEquatable<WanHost>
    {
        public IPAddress IPAddress { get; }

        public static SameIPAddress IPAddressComparer { get; } = new();

        public List<WanHost> ConnectedHosts { get; } = new();

        public WanHost(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
        }

        public WanHost(IPAddress ipAddress, List<WanHost> connectedHosts)
        {
            IPAddress = ipAddress;
            ConnectedHosts = connectedHosts;
        }

        public override string ToString()
        {
            return IPAddress.ToString();
        }

        public class SameIPAddress : IEqualityComparer<WanHost>
        {
            public bool Equals(WanHost x, WanHost y)
            {
                if (x == null)
                    return y == null;

                return x.IPAddress.Equals(y.IPAddress);
            }

            public int GetHashCode([DisallowNull] WanHost obj)
            {
                return obj.IPAddress.GetHashCode();
            }
        }

        public object Clone()
        {
            var newHost = new WanHost(IPAddress, ConnectedHosts);

            foreach (var connectedHost in ConnectedHosts)
            {
                connectedHost.ConnectedHosts.RemoveAt(
                    connectedHost.ConnectedHosts.FindIndex((host) => host.IPAddress.Equals(IPAddress)));
                
                connectedHost.ConnectedHosts.Add(newHost);
            }

            return newHost;
        }

        public bool Equals(WanHost other)
        {
            if (other == null)
                return false;

            return IPAddress.Equals(other.IPAddress) && ConnectedHosts.SequenceEqual(other.ConnectedHosts, new SameIPAddress());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as WanHost);
        }

        public override int GetHashCode()
        {
            return IPAddress.GetHashCode() ^ ConnectedHosts.Aggregate(1, (oldHash, host) => oldHash ^ host.IPAddress.GetHashCode());
        }
    }
}
