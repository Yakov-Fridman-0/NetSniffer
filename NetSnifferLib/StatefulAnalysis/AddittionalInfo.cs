using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.StatefulAnalysis
{
    public class AddittionalInfo
    {
        public bool IsTimeoutInfo { get; private set; } = false;

        public TimeoutInfo TimeoutInfo { get; private set; }
        
        private AddittionalInfo()
        {

        }

        public static AddittionalInfo CreateTimeoutInfo(DateTime timeoutStarted, DateTime timeoutExpired)
        {
            var info = new AddittionalInfo
            {
                IsTimeoutInfo = true,
                TimeoutInfo = new() 
                {
                    TimeoutStarted = timeoutStarted,
                    TimeoutExpiered = timeoutExpired
                }
            };

            return info;
        }
    }
}
