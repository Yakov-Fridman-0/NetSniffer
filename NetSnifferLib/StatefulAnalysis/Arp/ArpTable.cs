using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis.Arp
{
    public class ArpTable
    {
        //bool _detectDoS = true;
        //readonly object detectDoSLock = new();

        //public bool DetectDoS
        //{
        //    get
        //    {
        //        lock (detectDoSLock)
        //            return _detectDoS;
        //    }
        //    set
        //    {
        //        lock (detectDoSLock)
        //            _detectDoS = value;
        //    }
        //}

        //public bool DoSDetected { get; private set; } = false;


        //static int entryModificationDoSInverval = 250; // ms
        //static readonly object entryModificationDoSInvervalLock = new();

        //public static int EntryModificationDoSInverval
        //{
        //    get
        //    {
        //        lock (entryModificationDoSInvervalLock)
        //            return entryModificationDoSInverval;
        //    }
        //    set
        //    {
        //        lock (entryModificationDoSInvervalLock)
        //            entryModificationDoSInverval = value;
        //    }
        //}


        //static int _enrtyModificationDoSCount = 3; // ms
        //static readonly object enrtyModificationDoSCountLock = new();

        //public static int EnrtyModificationDoSCount
        //{
        //    get
        //    {
        //        lock (entryModificationDoSInvervalLock)
        //            return _enrtyModificationDoSCount;
        //    }
        //    set
        //    {
        //        lock (enrtyModificationDoSCountLock)
        //            _enrtyModificationDoSCount = value;
        //    }
        //}


        //static int _entryCreationDoSInterval = 250; // ms
        //static readonly object entryCreationDoSIntervalLock = new();

        //public static int EntryCreationDoSInterval
        //{
        //    get
        //    {
        //        lock (entryCreationDoSIntervalLock)
        //            return _entryCreationDoSInterval;
        //    }
        //    set
        //    {
        //        lock (entryCreationDoSIntervalLock)
        //            _entryCreationDoSInterval = value;
        //    }
        //}


        //static int _entryCreationDoSCount = 5;
        //static readonly object entryCreationDoSCountLock = new();

        //public static int EntryCreationDoSCount
        //{
        //    get
        //    {
        //        lock (entryCreationDoSCountLock)
        //            return _enrtyModificationDoSCount;
        //    }
        //    set
        //    {
        //        lock (enrtyModificationDoSCountLock)
        //            _enrtyModificationDoSCount = value;
        //    }
        //}

        readonly Dictionary<IPAddress, PhysicalAddress> arpTable = new(IPAddressHelper.EqulityComparer);

        readonly ConcurrentDictionary<IPAddress, List<ArpEntryTransaction>> transactionHistory = new(IPAddressHelper.EqulityComparer);

        readonly List<DateTime> entriesCreationTime = new();
        
        public PhysicalAddress GetPhysicalAddress(IPAddress address)
        {
            return arpTable.GetValueOrDefault(address);
        }

        public int FindTransactionPacketId(IPAddress ipAddress, PhysicalAddress physicalAddress)
        {
            var transactions = transactionHistory[ipAddress];

            foreach (var transaction in transactions)
            {
                if (physicalAddress.Equals(transaction.NewState.PhysicalAddress))
                    return transaction.PacketId;
            }

            return -1;
        }

        public void UpdateEntry(IPAddress ipAddress, PhysicalAddress physicalAddress, DateTime receivedTime, int packetId)
        {
            if (arpTable.TryGetValue(ipAddress, out PhysicalAddress currentPhysicalAddress))
            {
                if (!physicalAddress.Equals(currentPhysicalAddress))
                    ModifiyEntry(ipAddress, physicalAddress, receivedTime, packetId);
            }
            else
            {
                AddEntry(ipAddress, physicalAddress, receivedTime, packetId);
            }
        }

        void AddEntry(IPAddress ipAddress, PhysicalAddress physicalAddress, DateTime receivedTime, int packetId)
        {
            if (!arpTable.ContainsKey(ipAddress))
            {
/*                if (DetectDoS)
                {
                    if (entriesCreationTime.Count >= EntryCreationDoSCount)
                    {
                        TimeSpan interval = receivedTime - entriesCreationTime[^(_entryCreationDoSCount - 1)];

                        if (interval.TotalSeconds > _entryCreationDoSInterval)
                        {
                            DoSDetected = true;
                        }
                    }
                }*/

                entriesCreationTime.Add(receivedTime);
                transactionHistory.TryAdd(
                    ipAddress,
                    new List<ArpEntryTransaction>()
                    {
                        new(
                            ArpEntryTransactionType.Creation,
                            new() {IPAddress = ipAddress, PhysicalAddress = physicalAddress},
                            receivedTime,
                            packetId)
                    });

                lock (arpTable)
                    arpTable.Add(ipAddress, physicalAddress);
            }
            else
            {
                throw new ArgumentException("Address was already added");
            }
        }

        void ModifiyEntry(IPAddress ipAddress, PhysicalAddress physicalAddress, DateTime receivedTime, int packetId)
        {
            bool result;

            lock (arpTable)
                result = arpTable.ContainsKey(ipAddress);

            if (result)
            {
                var transactions = transactionHistory[ipAddress];

/*                if (DetectDoS)
                {
                    if (transactions.Count >= _enrtyModificationDoSCount)
                    {
                        TimeSpan interval = receivedTime - transactions[^(_enrtyModificationDoSCount -1)].TransactionTime;
                        
                        if (interval.TotalSeconds > entryModificationDoSInverval)
                        {
                            DoSDetected = true;
                        }
                    }
                }*/

                transactions.Add(
                    new(
                        transactionType: ArpEntryTransactionType.Modification, 
                        previousState: new() { IPAddress = ipAddress, PhysicalAddress = arpTable[ipAddress] },
                        newState: new() { IPAddress = ipAddress, PhysicalAddress =  physicalAddress},
                        transactionTime: receivedTime, 
                        packetId: packetId
                    ));

                lock (arpTable)
                    arpTable[ipAddress] = physicalAddress;
            }
            else
            {
                throw new ArgumentException("No entry found");
            }
        }
    }
}
