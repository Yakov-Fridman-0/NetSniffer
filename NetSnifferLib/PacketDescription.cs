using System;

using NetSnifferLib.General;

namespace NetSnifferLib
{
    public record PacketDescription
    {
        public DateTime TimeStamp { get; set; }

        public string Protocol { get; set; }

        public IAddress Source { get; set; }

        public IAddress Destination { get; set; }

        public int Length { get; set; }

        public string Info { get; set; }
    }
}
