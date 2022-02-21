using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using PcapDotNet.Base;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Icmp;

using NetSnifferLib.Statistics;

namespace NetSnifferLib
{
    public class NetSniffer
    {
        PhysicalAddress physicalAddress;
        IPAddress ipAddress;
        IPAddress defaultGateway;

        protected PacketCommunicator _communicator;

        protected readonly ActionBlock<Packet> _eventRaiser;

        protected Task _snifferTask;

        public event EventHandler<Packet> PacketReceived = delegate { };

        private static LivePacketDevice FindLivePacketDevice(NetworkInterface networkInterface)
        {
            var allDevices = LivePacketDevice.AllLocalMachine;

            if (allDevices.Count == 0)
                throw new Exception("No interfaces found! Make sure Npcap is installed.");

            var livePacketDevice = allDevices.FirstOrDefault(device => device.Name.Contains(networkInterface.Id));

            if (livePacketDevice == null)
                throw new Exception($"Live Packet Device {networkInterface.Name} not found.");

            return livePacketDevice;
        }
   
        protected NetSniffer(
            PhysicalAddress physicalAddress, IPAddress ipAddress, 
            IPAddress defaultGateway)
        {
            this.physicalAddress = physicalAddress;
            this.ipAddress = ipAddress;
            this.defaultGateway = defaultGateway;

            _eventRaiser = new ActionBlock<Packet>(packet => PacketReceived.Invoke(this, packet)) ;         
        }

        public static NetSniffer CreateLiveSniffer(SniffingOptions sniffingOptions)
        {
            var networkInterface = sniffingOptions.NetworkInterface;
            var filter = sniffingOptions.Filter;
            var promiscuous = sniffingOptions.Promiscuous;


            UnicastIPAddressInformation[] localComputerIPAddresses = new UnicastIPAddressInformation[networkInterface.GetIPProperties().UnicastAddresses.Count];
            networkInterface.GetIPProperties().UnicastAddresses.CopyTo(localComputerIPAddresses, 0);
            IPAddress localComputerIPv4Address = localComputerIPAddresses.FirstOrDefault((info) => info.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Address;
           
            var networkSniffer = new NetSniffer(
                networkInterface.GetPhysicalAddress(),
                localComputerIPv4Address,
                networkInterface.GetIPProperties().GatewayAddresses[0].Address
                );

            var livePacketDevice = FindLivePacketDevice(networkInterface);

            PacketDeviceOpenAttributes openAttributes = promiscuous ? PacketDeviceOpenAttributes.Promiscuous : PacketDeviceOpenAttributes.None;
            var communicator = livePacketDevice.Open(65536, openAttributes, 1000);                                  

            if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
            {
                throw new Exception("This program works only on Ethernet networks.");
            }

            communicator.SetFilter(filter);

            networkSniffer._communicator = communicator;

            return networkSniffer;
        }

        public static Task<NetSniffer> CreateLiveSnifferAsync(SniffingOptions sniffingOptions)
        {
            return Task.Run(() => CreateLiveSniffer(sniffingOptions));
        }

        //public static NetSniffer CreateOfflineSniffer(string fileName)
        //{
        //    var offlinePacketDevice = new OfflinePacketDevice(fileName);
           
        //    PacketDeviceOpenAttributes openAttributes = PacketDeviceOpenAttributes.Promiscuous;
        //    var communicator = offlinePacketDevice.Open(65536, openAttributes, 1000);

        //    var offlineNetworkSniffer = new NetSniffer
        //    {
        //        _communicator = communicator
        //    };

        //    return offlineNetworkSniffer;
        //}


        /// <summary>
        /// Starts capturing traffic
        /// </summary>
        public void Start()
        {
            if (_communicator != null)
            {
                _communicator.ReceivePackets(0, packet => _eventRaiser.Post(packet));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A task where the capturing takes place</returns>
        public Task StartAsync()
        {
            return _snifferTask = Task.Run(() => Start());
        }

        public void Stop()
        {
            var communicator = _communicator;        
            communicator.Break();
            communicator.Dispose();

            _communicator = null;
        }

        public static bool IsValidFilter(string filter)
        {
            BerkeleyPacketFilter packetFilter = null;
            try
            {
                packetFilter = new BerkeleyPacketFilter(filter, 65536, DataLinkKind.Ethernet);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            finally
            {
                packetFilter?.Dispose();
            }
        }

        public PingReply Ping(IPAddress destination, byte ttl)
        {
            Ping pingSender = new();
            PingOptions options = new();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;
            options.Ttl = ttl;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            PingReply reply = pingSender.Send(destination, timeout, buffer, options);
            return reply;
            //// Ethernet Layer
            //EthernetLayer ethernetLayer = new()
            //{
            //    Source = new MacAddress(UInt48.Parse(Convert.ToUInt64(physicalAddress.ToString(), 16).ToString())),
            //    Destination = new MacAddress("FF:FF:FF:FF:FF:FF") // will be later fixed
            //};

            //// IPv4 Layer
            //IpV4Layer ipV4Layer = new()
            //{
            //    Source = new IpV4Address(ipAddress.ToString()),
            //    CurrentDestination = new IpV4Address(destination.ToString()),
            //    Ttl = ttl,
            //};

            //// ICMP Layer
            //IcmpEchoLayer icmpLayer = new();

            //// Create the builder that will build our packets
            //PacketBuilder builder = new(ethernetLayer, ipV4Layer, icmpLayer);

            //Packet packet = builder.Build(DateTime.Now);

            //// Send down the packet
            //_communicator.SendPacket(packet);
        }

        //public PingReply Ping(IPAddress destination, byte ttl, byte identifier)
        //{
        //    Ping pingSender = new();
        //    PingOptions options = new();

        //    // Use the default Ttl value which is 128,
        //    // but change the fragmentation behavior.
        //    options.DontFragment = true;
        //    options.Ttl = ttl;

        //    // Create a buffer of 32 bytes of data to be transmitted.
        //    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        //    byte[] buffer = Encoding.ASCII.GetBytes(data);
        //    int timeout = 120;
        //    PingReply reply = pingSender.Send(destination, timeout, buffer, options);
        //    return reply;
        //    // Ethernet Layer
        //    //EthernetLayer ethernetLayer = new()
        //    //{
        //    //    Source = new MacAddress(physicalAddress.ToString()),
        //    //    Destination = new MacAddress("FF:FF:FF:FF:FF:FF") // will be later fixed
        //    //};

        //    //// IPv4 Layer
        //    //IpV4Layer ipV4Layer = new()
        //    //{
        //    //    Source = new IpV4Address(ipAddress.ToString()),
        //    //    CurrentDestination = new IpV4Address(destination.ToString()),
        //    //    Ttl = ttl,
        //    //};

        //    //// ICMP Layer
        //    //IcmpEchoLayer icmpLayer = new()
        //    //{
        //    //    Identifier = identifier
        //    //};

        //    //// Create the builder that will build our packets
        //    //PacketBuilder builder = new(ethernetLayer, ipV4Layer, icmpLayer);

        //    //Packet packet = builder.Build(DateTime.Now);

        //    //// Send down the packet
        //    //_communicator.SendPacket(packet);
        //}

        //public Task PingAsync(IPAddress destination, byte ttl)
        //{
        //    return Task.Run(() => Ping(destination, ttl));
        //}

        public Task PingAsync(IPAddress destination, byte ttl)
        {
            return Task.Run(() => Ping(destination, ttl));
        }
    }
}
