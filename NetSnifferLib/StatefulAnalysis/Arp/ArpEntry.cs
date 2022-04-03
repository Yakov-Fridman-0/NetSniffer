using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis.Arp
{
    public class ArpEntry
    {
        public IPAddress IPAddress { get; init; }

        public PhysicalAddress PhysicalAddress { get; init; }

        public ArpEntryType EntryType { get; set; }
    }
}
