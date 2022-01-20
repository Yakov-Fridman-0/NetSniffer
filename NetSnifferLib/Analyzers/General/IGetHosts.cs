using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.General
{
    interface IGetHosts<TAddress, TDatagram>
    {
        protected TAddress GetSource(TDatagram datagram);

        protected TAddress GetDestination(TDatagram datagram);
    }
}
