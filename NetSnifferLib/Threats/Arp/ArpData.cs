using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Threats
{
    class ArpData
    {
        public bool DetectThreats
        {
            get
            {
                return _detectThreats;
            }
            set
            {
                lock (detectThreatsLock)
                    _detectThreats = value;
            }
        }

        bool _detectThreats;

        object detectThreatsLock = new();

        public ArpData(bool detectThreats)
        {
            DetectThreats = detectThreats;
        }

        class ArpMessage
        {
            public ArpMessageInfo MessageInfo { get; set; }
            
            public DateTime TimeReceived { get; set; }
        }

        public ThreatHandler ThreatDetected { get; set; }

        public Dictionary<PhysicalAddress, IPAddress> Data { get; } = new(PhysicalAddressHelper.EqulityComparer);

        public List<ArpMessageInfo> MessagesInfo = new();

        public void RegisterRequest(ArpMessageInfo messageInfo, DateTime timeReceived)
        {
            lock (detectThreatsLock)
            {
                if (_detectThreats)
                {

                }
                else
                {

                }
            }
        }
    }
}
