﻿using System.Net;

using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV6;

using NetSnifferLib.General;
using NetSnifferLib.Analysis.DataLink;

namespace NetSnifferLib.Analysis.Network
{
    class IpV6Analyzer : BaseNetworkAnalyzer<IpV6Datagram>
    {
        protected override IPAddress GetSource(IpV6Datagram datagram)
        {
            return AddressConvert.ToIpAddress(datagram.Source);
        }

        protected override bool IsFromLan(IpV6Datagram datagram)
        {
            var ttl = GetTTL(datagram);
            return (ttl == 64) || (ttl == 128);
        }

        protected override int GetTTL(IpV6Datagram daragram)
        {
            return daragram.HopLimit;
        }

        protected override IPAddress GetDestination(IpV6Datagram datagram)
        {
            //TODO: Check options
            return AddressConvert.ToIpAddress(datagram.CurrentDestination);
        }

        protected override int GetPayloadLength(IpV6Datagram datgram)
        {
            return datgram.PayloadLength;
        }

        protected override Datagram GetPayloadAndAnalyzer(IpV6Datagram datagram, out IAnalyzer analyzer)
        {
            Datagram payload;

            if (datagram.ExtensionHeaders == IpV6ExtensionHeaders.Empty)
            {
                payload = datagram.Payload;
                analyzer = null;
            }
            else
            {
                //TODO: Add logic
                payload = null;
                analyzer = null;
            }

            return payload;
        }

        protected override string GetInfo(IpV6Datagram datagram, DataLinkContext context)
        {
            return string.Empty;
        }

        public override string Protocol => "IPv6";
    }
}
