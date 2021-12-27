using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Packets.Bootp
{
    public enum BootpMessageType : byte
    {
        BootRequest = 1,
        BootReplay = 2,
    }
}
