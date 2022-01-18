using PcapDotNet.Core;
using PcapDotNet.Packets;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace NetSnifferLib
{
    public class NetworkSniffer
    {
        protected PacketCommunicator _communicator;
        public PacketAnalyzer _packetAnalyzer;

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
   
        protected NetworkSniffer()
        {
            _eventRaiser = new ActionBlock<Packet>(packet => PacketReceived(this, packet));         
        }

        public static NetworkSniffer CreateLiveSniffer(SniffingOptions sniffingOptions)
        {
            var networkInterface = sniffingOptions.NetworkInterface;
            var filter = sniffingOptions.Filter;
            var promiscuous = sniffingOptions.Promiscuous;

            var networkSniffer = new NetworkSniffer();

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

        public static NetworkSniffer CreateOfflineSniffer(string fileName)
        {
            var offlinePacketDevice = new OfflinePacketDevice(fileName);
           
            PacketDeviceOpenAttributes openAttributes = PacketDeviceOpenAttributes.Promiscuous;
            var communicator = offlinePacketDevice.Open(65536, openAttributes, 1000);

            var offlineNetworkSniffer = new NetworkSniffer
            {
                _communicator = communicator
            };

            return offlineNetworkSniffer;
        }


        /// <summary>
        /// Starts capturing traffic
        /// </summary>
        private void Start()
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
    }
}
