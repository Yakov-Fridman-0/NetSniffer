using System;
using System.Net;

namespace NetSnifferLib
{
    public class TcpConnection
    {
        public IPEndPoint Connector { get; internal set; }
        public IPEndPoint Listener { get; internal set; }

        public DateTime OpeningTime { get; internal set; }

        public DateTime ClosingTime { get; internal set; }

        public TimeSpan Span => ClosingTime - OpeningTime;

        public int SentPackets { get; internal set; }

        public int SentBytes { get; set; }

        public int ReceviedPackets { get; internal set; }

        public int ReceivedBytes { get; internal set; }

        public int Packets => SentPackets + ReceviedPackets;

        public int Bytes => SentBytes + ReceivedBytes;

        public bool Open { get; internal set; }
    }
}
