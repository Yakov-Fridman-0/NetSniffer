using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.General
{
    sealed class EmptyContext : IContext
    {
        public EmptyContext() { }

        public IAddress Source => null;

        public IAddress Destination => null;
    }
}
