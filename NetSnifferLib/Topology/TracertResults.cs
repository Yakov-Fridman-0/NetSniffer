using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSnifferLib.Topology
{
    class TracertResults
    {   
        public IPAddress Source { get; set; }

        public IPAddress Destination { get; set; }

        public bool IsComplete { get; set; }

        public bool Successfull { get; set; }

        public TracertResults(IPAddress source, IPAddress destination)
        {
            Source = source;
            Destination = destination;
        }

        public readonly SortedDictionary<int, IPAddress> Hops = new();

        public void AddHop(IPAddress hop, int hopIndex)
        {
            Hops.Add(hopIndex, hop);
        }

        public List<IPAddress> ToList()
        {
            if (IsComplete)
            {
                var finalList = Hops.Values.ToList();

                finalList.Insert(0, Source);

                if (Successfull)
                    finalList.Add(Destination);

                return finalList;
            }

            return null;
        }
    }
}
