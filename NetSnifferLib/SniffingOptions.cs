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
