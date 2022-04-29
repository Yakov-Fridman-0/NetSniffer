using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.NetworkInformation;

using NetSnifferLib.StatefulAnalysis.Arp;

namespace NetSnifferLib.Topology
{
    public class LanHost : IPhysicalAddress, IIPAddress//, ICloneable, IEquatable<LanHost>
    {
        public PhysicalAddress PhysicalAddress { get; internal set; } = null;

        /*        public static SamePhysicalAddress PhysicalAddressComparer { get; } = new();

                public class SamePhysicalAddress : IEqualityComparer<LanHost>
                {
                    public bool Equals(LanHost x, LanHost y)
                    {
                        return x.PhysicalAddress.Equals(y.PhysicalAddress);
                    }

                    public int GetHashCode([DisallowNull] LanHost obj)
                    {
                        return obj.PhysicalAddress.GetHashCode();
                    }
                }*/

        IPAddress _ipAddress = IPAddress.Any;
        public IPAddress IPAddress
        {
            get => _ipAddress;

            internal set => _ipAddress = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal LanHost(PhysicalAddress physicalAddress)
        {
            PhysicalAddress = physicalAddress;
        }

        internal LanHost(PhysicalAddress physicalAddress, IPAddress ipAddress)
        {
            IPAddress = ipAddress;
            PhysicalAddress = physicalAddress;
        }
       
/*        public object Clone()
        {
            return new LanHost(
                PhysicalAddressHelper.CloneAddress(PhysicalAddress),
                IPAddressHelper.CloneAddress(IPAddress));
        }*/

/*        public bool Equals(LanHost other)
        {
            if (other == null)
                return false;

            return PhysicalAddress.Equals(other.PhysicalAddress) && 
                ((IPAddress?.Equals(other.IPAddress) ??
                other.IPAddress?.Equals(other) ?? false) || (IPAddress == null && other.IPAddress == null));
        }*/

/*        public override int GetHashCode()
        {
            return PhysicalAddress.GetHashCode() ^ IPAddress.GetHashCode();
        }*/

        public override string ToString()
        {
            return string.Format("{0} {1}", PhysicalAddress.ToString(), IPAddress != null ? IPAddress.ToString() : "N/A");
        }

/*        public override bool Equals(object obj)
        {
            return Equals(obj as LanHost);
        }*/

        internal ArpTable ArpTable { get; } = new();
    }
}
