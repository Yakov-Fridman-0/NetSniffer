using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets;

namespace NetSnifferLib.General
{
    public interface IDatagramHostsString
    {
        public abstract string GetDatagramSourceString(Datagram datagram);

        public string GetDatagramDestinationString(Datagram datagram);

        public bool SupportsHosts 
        { 
            get; 
        }
    }
}
