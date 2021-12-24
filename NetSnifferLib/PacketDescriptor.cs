using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PcapDotNet.Packets;

namespace NetSnifferLib
{
    class PacketDescriptor
    {
        string GetPacketSource(Packet packet)
        {
            throw new NotImplementedException();
        }

        string GetPacketDestination(Packet packet)
        {
            throw new NotImplementedException();
        }

        string GetPacketProtocol(Packet packet)
        {
            throw new NotImplementedException();
        }

        string GetPacketLength(Packet packet)
        {
            return packet.Length.ToString();
        }

        string GetPacketInfo(Packet packet)
        {
            throw new NotImplementedException();
        }
    }
}
