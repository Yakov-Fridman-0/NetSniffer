using System.Net.NetworkInformation;

namespace NetSnifferLib
{
    public class LiveSnifferArgs : SnifferArgs
    {
        public NetworkInterface NetworkInterface { get; set; }
    }

    public class OfflineSnifferArgs : SnifferArgs
    {
        public string FileName { get; set; }
    }

    public class SnifferArgs
    {
        public string CaptureFilter { get; set; }

        public bool IsPromiscuous { get; set; }

        public int NumberOfPackets { get; set; }
    }
}
