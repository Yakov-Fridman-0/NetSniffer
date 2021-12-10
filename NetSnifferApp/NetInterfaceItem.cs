using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NetSnifferApp
{
    public class NetInterfaceItem
    {
        NetInterfaceItem(NetworkInterface networkInterface)
        {
            NetworkInterface = networkInterface;
        }

        public NetworkInterface NetworkInterface { get; private set; }

        public static NetInterfaceItem[] GetItems()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            var items = new List<NetInterfaceItem>();

            foreach (NetworkInterface adapter in adapters)
            {
                //TODO: add adapter filter here
                items.Add(new NetInterfaceItem(adapter));
            }

            items.Sort((left, right) => string.Compare(left.NetworkInterface.Name, right.NetworkInterface.Name, StringComparison.InvariantCultureIgnoreCase));

            return items.ToArray();
        }

        public override string ToString()
        {
            return NetworkInterface.Name;
        }
    }
}
