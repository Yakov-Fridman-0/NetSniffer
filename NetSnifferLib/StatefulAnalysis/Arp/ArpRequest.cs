/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis.Arp
{
    class ArpRequest : ArpMessage
    {
        public override bool IsRequest => true;

        public override bool IsReply => false;

        public List<ArpReply> Replies { get; } = new();

        public void AddReply(ArpReply reply)
        {
            lock (Replies)
                Replies.Add(reply);
        }

        public static void RegisterDuplicate(ArpRequest original, ArpRequest duplicate)
        {
            lock (original.Duplicates)
                original.Duplicates.Add(duplicate);

            duplicate.DuplicateOf = original;
        }
    }
}
*/