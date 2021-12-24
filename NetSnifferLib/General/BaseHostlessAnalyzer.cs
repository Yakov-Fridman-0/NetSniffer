using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.General
{
    public abstract class BaseHostlessAnalyzer : BaseAnalyzer
    {
        public override bool SupportsHosts => false;

        public override string GetDatagramSourceString(Datagram datagram)
        {
            throw new NotImplementedException();
        }

        public override string GetDatagramDestinationString(Datagram datagram)
        {
            throw new NotImplementedException();
        }
    }
}
