using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.General
{
    class PhysicalAddressContainer : BaseAddressContainer
    {
        readonly PhysicalAddress _physicalAddress;

        public PhysicalAddressContainer(PhysicalAddress physicalAddress)
        {
            _physicalAddress = physicalAddress;
        }

        public override PhysicalAddress PhysicalAddress => _physicalAddress;
    }
}
