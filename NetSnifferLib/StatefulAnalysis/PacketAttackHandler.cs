using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis
{
    public delegate void PacketAttackHandler(int packetId, Attack attack);
}
