using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Threats
{
    class ArpMessageInfo
    {
        public bool IsRequest { get; set; }

        public PhysicalAddress SenderPhysicalAddress { get; set; }

        public IPAddress SenderIPAddress { get; set; }

        public PhysicalAddress TargetPhysicalAddress { get; set; }

        public IPAddress TargetIPAddress { get; set; }
    }
}
