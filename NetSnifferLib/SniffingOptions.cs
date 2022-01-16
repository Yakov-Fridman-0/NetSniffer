using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace NetSnifferLib
{
    public struct SniffingOptions
    {
        public string Filter { get; set; }

        public NetworkInterface NetworkInterface { get; set; }

        public bool Promiscuous { get; set; }
    }
}
