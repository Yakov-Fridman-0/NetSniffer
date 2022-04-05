using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.General
{
    sealed class EmptyAddress : IAddress
    {
        public EmptyAddress() { }

        public PhysicalAddress PhysicalAddress => null;

        public bool IsPhysicalAddress => false;

        public IPAddress IPAddress => null;

        public bool IsIpAddress => false;

        public IPEndPoint IpEndPoint => null;

        public bool IsIpEndPoint => false;
    }
}
