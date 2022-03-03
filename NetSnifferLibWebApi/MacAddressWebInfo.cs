using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.NetworkInformation;

namespace NetSnifferLibWebApi
{
    public static class MacAddressWebInfo
    {
        const string VendorBaseUriString = "http://api.macvendors.com/";

        public static Task<string> GetVendorAsync(PhysicalAddress address)
        {
            var uri = new Uri(VendorBaseUriString + address.ToString());
            using var client = new HttpClient();

            return client.GetStringAsync(uri);
        }
    }
}
