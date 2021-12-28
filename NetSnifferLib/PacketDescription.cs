using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib
{
    public record PacketDescription
    {
        public string TimeStamp { get; set; }
        public string Protocol { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Length { get; set; }
        public string Info { get; set; }
    }
}
