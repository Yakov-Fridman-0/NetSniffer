﻿using System;

using PcapDotNet.Packets.Arp;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.DataLink;
using NetSnifferLib.Topology;

using NetSnifferLib.StatefulAnalysis.Arp;

namespace NetSnifferLib.Analysis.Miscellaneous
{
    class ArpAnalyzer : BaseAnalyzer<ArpDatagram, DataLinkContext>
    {
        public override string Protocol => "ARP";

        public event EventHandler<PayloadIndicatesHostEventArgs> PayloadIndicatesHost;

        protected override string GetInfo(ArpDatagram datagram, DataLinkContext context)
        {
            var senderIp = AddressConvert.ToIPAddress(datagram.SenderProtocolIpV4Address);
            var targetIp = AddressConvert.ToIPAddress(datagram.TargetProtocolIpV4Address);

            var senderMac = AddressConvert.ToPhysicalAddress(datagram.SenderHardwareAddress);

            return datagram.Operation switch
            {
                ArpOperation.Request => $"Who has {targetIp}? Tell {senderIp}",

                ArpOperation.Reply => $"{senderIp} is at {senderMac}",

                _ => string.Empty
            };
        }

        private static bool IsRequest(ArpDatagram datagram)
        {
            return datagram.Operation == ArpOperation.Request;
        }

        private static bool IsReply(ArpDatagram datagram)
        {
            return datagram.Operation == ArpOperation.Reply;
        }

        protected override ArpAnalysis AnalyzeDatagramCore(ArpDatagram datagram, DataLinkContext context, int packetId)
        {
            if (IsRequest(datagram))
            {
                ArpStatefulAnalyzer.ReportRequest(datagram, packetId);

                PayloadIndicatesHost?.Invoke(
                    this, 
                    new PayloadIndicatesHostEventArgs(
                        AddressConvert.ToPhysicalAddress(datagram.SenderHardwareAddress),
                        AddressConvert.ToIPAddress(datagram.SenderProtocolIpV4Address))  
                    );
            }
            if (IsReply(datagram))
            {
                ArpStatefulAnalyzer.ReportReply(datagram, packetId);

                PayloadIndicatesHost?.Invoke(
                    this, 
                    new PayloadIndicatesHostEventArgs(
                        AddressConvert.ToPhysicalAddress(datagram.SenderHardwareAddress),
                        AddressConvert.ToIPAddress(datagram.SenderProtocolIpV4Address))                     
                    );

                PayloadIndicatesHost?.Invoke(
                    this, 
                    new PayloadIndicatesHostEventArgs(   
                        AddressConvert.ToPhysicalAddress(datagram.TargetHardwareAddress),
                        AddressConvert.ToIPAddress(datagram.TargetProtocolIpV4Address))
                    );
            }

            var analysis = new ArpAnalysis();
            analysis.AddInfo(GetInfo(datagram, context));

            return analysis;
        }
    }
}
