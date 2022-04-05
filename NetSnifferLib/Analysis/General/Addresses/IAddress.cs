﻿using System.Net;
using System.Net.NetworkInformation;

namespace NetSnifferLib.General
{
    public interface IAddress
    {
        PhysicalAddress PhysicalAddress { get; }

        bool IsPhysicalAddress { get; }

        IPAddress IPAddress { get; } 

        bool IsIpAddress { get; }

        IPEndPoint IpEndPoint { get; }

        bool IsIpEndPoint { get; }
    }
}
