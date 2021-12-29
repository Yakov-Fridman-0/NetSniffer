using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace NetSnifferLib
{
    public interface IPacketAnalyzer
    {
        bool IsMatch(Packet packet);

        DateTime GetTimeStamp(Packet packet)
        {    
            return packet.Timestamp;
        }

        string GetProtocol(Packet packet);

        string GetPacketSource(Packet packet);

        string GetPacketDestination(Packet packet);

        ushort GetLength(Packet packet);

        string GetPacketInfo(Packet packet);
    }
}
