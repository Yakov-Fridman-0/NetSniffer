using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PcapDotNet.Packets;
using NetSnifferLib.StatefulAnalysis;

namespace NetSnifferLib
{
    public static class IdManager
    {
        static int nextPacketId = -1;

        static readonly ConcurrentDictionary<int, Packet> packetsById = new();

        public static int GetNewPacketId(Packet packet)
        {
            var newId = Interlocked.Increment(ref nextPacketId);
            packetsById.TryAdd(newId, packet);

            return newId;
        }

        public static Packet GetPacket(int id)
        {
            return packetsById[id]; //TODO: bugs 
        }

        public static DateTime GetPacketTimestamp(int id)
        {
            return GetPacket(id).Timestamp;
        }

        public static int GetId(Packet packet)
        {
            foreach (var (id, aPacket) in packetsById)
            {
                if (packet == aPacket)
                    return id;
            }

            return -1;
        }

        public static void Reset()
        {
            Interlocked.Exchange(ref nextPacketId, -1);

            packetsById.Clear();
        }
    }
}
