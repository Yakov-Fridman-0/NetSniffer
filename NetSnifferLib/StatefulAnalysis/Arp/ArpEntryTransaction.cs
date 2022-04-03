using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis.Arp
{
    readonly struct ArpEntryTransaction
    {
        public int PacketId { get; init; }

        public ArpEntryTransactionType TransactionType { get; init; }        
        
        public DateTime TransactionTime { get; init; }

        public ArpPair PreviousState { get; init; }

        public ArpPair NewState { get; init; }

        public ArpEntryTransaction(
            ArpEntryTransactionType transactionType,
            ArpPair state,
            DateTime transactionTime, 
            int packetId) : this()
        {
            TransactionType = transactionType;
            TransactionTime = transactionTime;
            PreviousState = ArpPair.Empty;
            NewState = state;
            PacketId = packetId;
        }

        public ArpEntryTransaction(
            ArpEntryTransactionType transactionType, 
            DateTime transactionTime, 
            ArpPair previousState, 
            ArpPair newState, 
            int packetId) : this(transactionType, newState, transactionTime, packetId)
        {
            PreviousState = previousState;
        }
    }
}
