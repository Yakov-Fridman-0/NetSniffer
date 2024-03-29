﻿using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    interface IAnalysis
    {
        string Info { get; }

        IAddress Source { get; }

        IAddress Destination { get; }

        bool HasAddresses { get; }

        Datagram Payload { get; }

        IContext PayloadContext { get; }

        IAnalyzer PayloadAnalyzer { get; }

        bool HasPayload { get; }
    }
}
