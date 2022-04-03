using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis.Arp
{
    public readonly struct ArpPair 
    {
        public static ArpPair Empty => new()
        {
            PhysicalAddress = PhysicalAddress.None,
            IPAddress = IPAddress.None
        };

        public PhysicalAddress PhysicalAddress { get; init; }

        public IPAddress IPAddress { get; init; }

        public bool IsPhysicalAddressEmpty => PhysicalAddressHelper.IsEmpty(PhysicalAddress);

        public bool IsPhysicalAddressBroadcast => PhysicalAddressHelper.IsBroadcast(PhysicalAddress);

        public override bool Equals(object obj)
        {
            if (obj is ArpPair messageData)
                return PhysicalAddress.Equals(messageData.PhysicalAddress) && IPAddress.Equals(messageData.IPAddress);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return PhysicalAddress.GetHashCode() ^ IPAddress.GetHashCode();
        }

        public static bool operator ==(ArpPair lhs, ArpPair rhs) => lhs.Equals(rhs);

        public static bool operator !=(ArpPair lhs, ArpPair rhs) => !(lhs == rhs);

        public bool IPAddressEquals(ArpPair messageData)
        {
            return IPAddress.Equals(messageData.IPAddress);
        }
    }
}
