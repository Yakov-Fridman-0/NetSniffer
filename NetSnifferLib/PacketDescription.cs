using System;

using NetSnifferLib.General;

namespace NetSnifferLib
{
    public readonly struct PacketDescription
    {
        public DateTime TimeStamp { get; init; }

        public string Protocol { get; init; }

        public IAddress Source { get; init; }

        public IAddress Destination { get; init; }

        public int Length { get; init; }

        public string Info { get; init; }

        public PacketDescription(DateTime timestamp, string protocol, IAddress source, IAddress destination, int length, string info)
        {
            TimeStamp = timestamp;
            Protocol = protocol;
            Source = source;
            Destination = destination;
            Length = length;
            Info = info;
        }
    }
}
