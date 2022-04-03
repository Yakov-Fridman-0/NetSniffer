using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis.Arp
{
    /*class ArpReply : ArpMessage
    {
        public override bool IsRequest => false;

        public override bool IsReply => true;

        /// <summary>
        /// Points to the original request (and not to its duplicates)  
        /// </summary>
        public ArpRequest Request
        {
            get => request;
            set
            {
                if (request == null)
                    request = value;
                else
                    throw new InvalidOperationException("Request was already set");
            }
        }

        ArpRequest request = null;

        public bool MatchesRequest(ArpRequest request)
        {
            return request.TimeReceived > request.TimeReceived &&
                SenderMessageData.IPAddressEquals(request.TargetMessageData) &&
                TargetMessageData.Equals(request.SenderMessageData);
        }
    }*/
}
