using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using NetSnifferLib.General;
using NetSnifferLib.Analysis;
using NetSnifferLib.Topology;
using NetSnifferLib.StatefulAnalysis.Arp;

using PcapDotNet.Packets.Arp;

namespace NetSnifferLib.StatefulAnalysis.Arp
{
    static class ArpStatefulAnalyzer
    {
        //static int requestTimeout = 1000; // ms

        static public AdditionalInfoProvidedHandler TimeoutOccured { get; set; }

        static ActionBlock<(ArpDatagram Datagram, int PacketId)> replyActionBlock = new(
            new Action<(ArpDatagram Datagram, int PacketId)>( tuple => AnalyzeReplyCore(tuple.Datagram, tuple.PacketId)));
        static public AdditionalInfoProvidedHandler FoundReply { get; set; }

        static readonly List<ArpDatagram> AllDatagrams = new();
        static readonly ConcurrentDictionary<ArpDatagram, CancellationTokenSource> UnansweredRequestsTokenSources = new();

        //static void StartTimeoutTask(ArpDatagram request)
        //{
        //    CancellationTokenSource source = new();
        //    CancellationToken token = source.Token;

        //    Task.Run(() =>
        //    {
        //        Task.Delay(requestTimeout, token);

        //        if (!token.IsCancellationRequested)
        //            TimeoutOccured?.Invoke(1); //TODO: finish

        //    }, token);

        //    UnansweredRequestsTokenSources.TryAdd(request, source);
        //}

        static public void AnalyzeRequest(ArpDatagram request, int packetId)
        {
            AllDatagrams.Add(request);
        }

        static public void AnalyzeReply(ArpDatagram reply, int pakcetId)
        {
            replyActionBlock.Post((reply, pakcetId));
        }

        public static void AnalyzeReplyCore(ArpDatagram reply, int packetId)
        {
            AllDatagrams.Add(reply);

            var senderPhysicalAddress = AddressConvert.ToPhysicalAddress(reply.SenderHardwareAddress);
            var senderIPAddress = AddressConvert.ToIPAddress(reply.SenderProtocolIpV4Address);

            var targetPhysicalAddress = AddressConvert.ToPhysicalAddress(reply.TargetHardwareAddress);
            var targetIPAddress = AddressConvert.ToIPAddress(reply.TargetProtocolIpV4Address);

            LanHost targetHost = null, otherHost = null;

            var allHosts = PacketAnalyzer.Analyzer.GetLanHosts();

            lock (allHosts)
                targetHost = PacketAnalyzer.Analyzer.GetLanHosts().First(host => host.PhysicalAddress.Equals(targetPhysicalAddress));

            targetHost.ArpTable.UpdateEntry(senderIPAddress, senderPhysicalAddress, IdManager.GetPacket(packetId).Timestamp, packetId);


            lock (allHosts)
                otherHost = allHosts.FirstOrDefault(host => senderIPAddress.Equals(host.IPAddress));

            if (otherHost != null)
            {
                if (senderPhysicalAddress.Equals(otherHost.ArpTable.GetPhysicalAddress(targetIPAddress)))
                {
                    //Man-in-the-Middle
                    var packetData = PacketData.GetPacketDataByPacketId(packetId);

                    var otherPacketId = otherHost.ArpTable.FindTransactionPacketId(targetIPAddress, senderPhysicalAddress);
                    var otherPacketData = PacketData.GetPacketDataByPacketId(otherPacketId);

                    var attack = new Attack
                        (
                        "Man-in-the-Middle (ARP)",
                        new[] { otherPacketId, packetId },
                        new[] { new PhysicalAddressContainer(senderPhysicalAddress) },
                        new[] { new IpAddressContainer(targetHost.IPAddress), new IpAddressContainer(otherHost.IPAddress) }
                        );

                    packetData.AddAttack(attack);
                    //packetData.AttackDetected?.Invoke(packetId, attack);

                    otherPacketData.AddAttack(attack);
                    //otherPacketData.AttackDetected?.Invoke(otherPacketId, attack);
                }
            }
        }
    }
}
