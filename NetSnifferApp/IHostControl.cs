using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferApp
{
    interface IHostControl
    {
        void BecomeRouter();

        void BecomeServer();

        void BecomeRouterAndServer();

        bool IsRouter { get; }

        bool IsServer { get; }
    }
}
