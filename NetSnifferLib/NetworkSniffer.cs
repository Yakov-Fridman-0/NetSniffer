using PcapDotNet.Core;
using PcapDotNet.Packets;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace NetSnifferLib
{
    public class NetworkSniffer : IDisposable
    {
        private LivePacketCommunicator _communicator;
        public PacketAnalyzer _packetAnalyzer;

        
        private readonly ActionBlock<Packet> _eventRaiser;

        private Task _snifferTask;

        public event EventHandler<Packet> PacketReceived = delegate { };

        public NetworkInterface NetworkInterface { get; private set; }
        public string Filter { get; set; }
        public bool Promiscous { get; set; }

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

        private NetworkSniffer()
        {
            _eventRaiser = new ActionBlock<Packet>(packet => PacketReceived(this, packet));         
        }

        public static NetworkSniffer CreateLiveSniffer(SniffingOptions sniffingOptions)
        {
            var networkInterface = sniffingOptions.NetworkInterface;
            var filter = sniffingOptions.Filter;
            var promiscuous = sniffingOptions.Promiscuous;

            var networkSniffer = new NetworkSniffer()
            {
                NetworkInterface = networkInterface,
                Filter = filter,
                Promiscous = promiscuous
            };

            var livePacketDevice = FindLivePacketDevice(networkInterface);

            PacketDeviceOpenAttributes openAttributes = promiscuous ? PacketDeviceOpenAttributes.Promiscuous : PacketDeviceOpenAttributes.None;
            var communicator = livePacketDevice.Open(65536, openAttributes, 1000);                                  

            if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
            {
                throw new Exception("This program works only on Ethernet networks.");
            }

            communicator.SetFilter(filter);

            networkSniffer._communicator = (LivePacketCommunicator)communicator;

            return networkSniffer;
        }

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

        public void Dispose()
        {
            //TODO: Add dispose logic here
            GC.SuppressFinalize(this);
        }
    }
}
