using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;

namespace NetSnifferLib.StatefulAnalysis.Arp
{
    abstract class ArpMessage : Message, IDuplicates<ArpMessage>
    {
        public abstract bool IsRequest { get; }

        public abstract bool IsReply { get; }

        protected ArpMessage(int[] packetIds) : base(packetIds)
        {

        }

        public bool IsGratuitiuous
        {
            get
            {
                if (IsRequest || IsReply)
                {
                    return SenderMessageData.IPAddress.Equals(TargetMessageData.IPAddress) && SenderMessageData.IsPhysicalAddressBroadcast;
                }
                else
                {
                    throw new InvalidOperationException("Cann't determine if a non reply or request packet is gratitous");
                }
            }
        }

        public ArpPair SenderMessageData { get; }
        
        public ArpPair TargetMessageData { get; }

        public bool ContentEquals(ArpMessage message)
        {
            return IsRequest == message.IsRequest && 
                IsReply == message.IsReply &&
                SenderMessageData.Equals(message.SenderMessageData) && 
                TargetMessageData.Equals(message.TargetMessageData);
        }

        public static void RegisterDuplicate<T>(T original, T duplicate) where T : ArpMessage 
        {
            lock (original.Duplicates)
                original.Duplicates.Add(duplicate);

            duplicate.DuplicateOf = original;
        }

        public ArpMessage DuplicateOf { get; protected set; }

        public List<ArpMessage> Duplicates { get; protected set; }
    }
}
