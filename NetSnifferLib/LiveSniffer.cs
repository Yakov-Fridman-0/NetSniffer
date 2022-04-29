using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PcapDotNet.Base;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets;

using NetSnifferLib.General;
using NetSnifferLib.Analysis;

namespace NetSnifferLib
{
    public class LiveSniffer : NetSniffer
    {
        readonly NetworkInterface @interface;

        public LiveSniffer(LiveSnifferArgs args) : base(args)
        {
            @interface = args.NetworkInterface;

            CreateCommunicator();

            PacketAnalyzer.Analyzer.Sniffer = this;
        }

        protected override LivePacketDevice GetPacketDevice()
        {
            var allDevices = LivePacketDevice.AllLocalMachine;

            if (allDevices.Count == 0)
                throw new Exception("No interfaces found! Make sure Npcap is installed.");

            var livePacketDevice = allDevices.FirstOrDefault(device => device.Name.Contains(@interface.Id));

            if (livePacketDevice == null)
                throw new Exception($"Live Packet Device {@interface.Name} not found.");

            return livePacketDevice;
        }

        public PingReply Ping(IPAddress destination, byte ttl)
        {
            Ping pingSender = new();
            PingOptions options = new();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;
            options.Ttl = ttl;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            PingReply reply = pingSender.Send(destination, timeout, buffer, options);
            return reply;
        }
    }
}
