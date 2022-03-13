using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Threats
{
    abstract class SingleTargetThreat : Threat
    {
        public PhysicalAddress TargetPhysicalAddress { get; protected set; }
    }
}
