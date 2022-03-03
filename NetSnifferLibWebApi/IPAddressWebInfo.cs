using System;
using System.Net;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IpData;

using NetSnifferLib;

namespace NetSnifferLibWebInfo
{
    public static class IPAddressWebInfo
    {
        const string IpdataKey = "914223c5df7081e91e71dbbffb0fe200e8b3e0713817ebb13c0232c5";
        static readonly IpDataClient client = new(IpdataKey);

        static readonly ConcurrentDictionary<IPAddress, Task<IpData.Models.IpInfo>> cache = new(IPAddressHelper.EqulityComparer);

        public static string GetCountryCodeAsync(IPAddress address)
        {
            
            if (cache.TryGetValue(address, out Task<IpData.Models.IpInfo> info))
                return info.Result.CountryCode;

            try
            {
                info = client.Lookup(address.ToString());
                cache.TryAdd(address, info);

                return info.Result.CountryCode;
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);

                return string.Empty;
            }
        }
    }
}
