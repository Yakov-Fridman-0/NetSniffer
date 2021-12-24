using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace NetSnifferLib
{
    public interface IDatagramInfo<T> where T: Datagram
    {
        string GetDatagramInfo(T datagram);
    }
}
