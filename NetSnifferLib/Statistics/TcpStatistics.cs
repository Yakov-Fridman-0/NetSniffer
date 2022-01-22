using System;

namespace NetSnifferLib.Statistics
{
    public class TcpStatistics
    {
        /// <summary>
        /// Number of connections
        /// </summary>
        public int Connections { get; internal set; } = 0;

        /// <summary>
        /// Number of endpoints
        /// </summary>
        public int Endpoints { get; internal set; } = 0;

        public TimeSpan TotalConnectionSpans { get; internal set; } = default;

        public TimeSpan MeanConnectionSpan => TotalConnectionSpans / Connections;

        public int TotalSentPackets { get; internal set; } = 0;

        public int MeanSentPackets => TotalSentPackets / Connections;

        public int TotalSentBytes { get; internal set; } = 0;

        public int MeanSentBytes => TotalSentBytes / Connections;

        public int MeanBytesPerSentPacket => TotalSentBytes / TotalSentPackets;

        public int TotalReceivedPackets { get; internal set; } = 0;

        public int MeanReceivedPackets => TotalReceivedPackets / Connections;

        public int TotalReceivedBytes { get; internal set; } = 0;

        public int MeanBytesPerReceivedPacket => TotalReceivedBytes / TotalReceivedPackets;

        public int MeanReceivedBytes => TotalReceivedBytes / Connections;

        public int TotalPackets => TotalSentPackets + TotalReceivedBytes;

        public int MeanTotalPackets => TotalPackets / Connections;

        public int TotalBytes => TotalSentBytes + TotalReceivedBytes;

        public int MeanBytes => TotalBytes / Connections;
    }
}
