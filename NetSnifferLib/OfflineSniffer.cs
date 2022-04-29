using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PcapDotNet.Base;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets;

namespace NetSnifferLib
{
    public class OfflineSniffer : NetSniffer
    {
        string dumpFileName;

        public OfflineSniffer(OfflineSnifferArgs args) : base(args)
        {
            dumpFileName = args.FileName;

            CreateCommunicator();
        }



        protected override PacketDevice GetPacketDevice()
        {
            return new OfflinePacketDevice(dumpFileName);
        }
    }
}
