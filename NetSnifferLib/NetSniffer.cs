using PcapDotNet.Core;
using PcapDotNet.Packets;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace NetSnifferLib
{
    public class NetSniffer : IDisposable
    {
        private LivePacketCommunicator _communicator;

        private readonly ActionBlock<Packet> _eventRaiser;
        private Task _snifferTask;

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

        public NetSniffer()
        {
            _eventRaiser = new ActionBlock<Packet>(packet => PacketReceived(this, packet));
        }

        public void Start(NetworkInterface networkInterface)
        {
            StartCore(networkInterface);
        }

        public Task StartAsync(NetworkInterface networkInterface)
        {
            _snifferTask = Task.Factory.StartNew(() =>
            {
                StartCore(networkInterface);
            });

            return _snifferTask;
        }

        public void Stop()
        {
            var communicator = _communicator;
            _communicator = null;

            communicator.Break();
            communicator.Dispose();
        }

        private void StartCore(NetworkInterface networkInterface)
        {

            var livePacketDevice = FindLivePacketDevice(networkInterface);

            var communicator =
                 livePacketDevice.Open(65536,                                // portion of the packet to capture
                                                                             // 65536 guarantees that the whole packet will be captured on all the link layers
                                     PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                     1000);                                  // read timeout

            if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
            {
                throw new Exception("This program works only on Ethernet networks.");
            }

            _communicator = communicator as LivePacketCommunicator;

            // start the capture
            communicator.ReceivePackets(0, packet => _eventRaiser.Post(packet));
        }

        public void Dispose()
        {
            //TODO: add dispose logic here
            GC.SuppressFinalize(this);
        }
    }
}
