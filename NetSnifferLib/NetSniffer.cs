using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using NetSnifferLib.Analysis;
using System.Threading;

namespace NetSnifferLib
{
    public abstract class NetSniffer
    {
        readonly bool isPromiscous;
        readonly string filter;

        readonly List<Packet> allPackets = new();

        public DateTime StartingTime { get; private set; }

        public DateTime StoppingTime { get; private set; }

        public int MaxPacketNumber { get; protected set; }

        protected PacketCommunicator communicator;

        protected readonly ActionBlock<Packet> eventRaiser;

        int packetsNum = 0;

        protected Task _snifferTask;

        public event EventHandler<PacketData> PacketReceived = delegate { };

        public event EventHandler PacketLimitReached = delegate { };

        public event EventHandler CaptureStopped = delegate { };

        public ManualResetEvent SniffingStarted { get; } = new(false);

        public void SaveCapture(string fileName, string displayFilterString)
        {
            DisplayFilter displayFilter = null;

            if (DisplayFilter.TryParse(displayFilterString, ref displayFilter))
            {
                var packets = allPackets.Where(packet => displayFilter.PacketMatches(packet)).ToList();
                PacketDumpFile.Dump(fileName, DataLinkKind.Ethernet, 65536, packets);
            }
        }

        protected NetSniffer(SnifferArgs args)
        {
            isPromiscous = args.IsPromiscuous;
            filter = args.CaptureFilter;
            MaxPacketNumber = args.NumberOfPackets;

            eventRaiser = new ActionBlock<Packet>(packet =>
            {
                allPackets.Add(packet);

                packetsNum++;

                var packetData = PacketData.Create(packet);
                PacketReceived.Invoke(this, packetData);

                if (packetsNum == MaxPacketNumber)
                    PacketLimitReached.Invoke(this, EventArgs.Empty);

                packetData.AnalyzeAsync();
            }
            );
        }

        public void Start()
        {
            IdManager.Reset();
            PacketData.Reset();
            PacketAnalyzer.CreateNewAnalyzer();

            PacketAnalyzer.Analyzer.Sniffer = this;

            StartingTime = DateTime.Now;
            
            SniffingStarted.Set();

            try
            {
                communicator.ReceivePackets(MaxPacketNumber, OnPacketReceived);
            }
            catch (InvalidOperationException)
            {

            }

            StoppingTime = DateTime.Now;

            lock (disposeLock)
            {
                if (!disposed)
                {
                    disposed = true;
                    communicator.Dispose();
                }
            }
            CaptureStopped.Invoke(this, new EventArgs());
        }

        bool disposed = false;
        readonly object disposeLock = new();

        void OnPacketReceived(Packet packet)
        {
            eventRaiser.Post(packet);
        }

        public Task StartAsync()
        {
            return Task.Run(() => Start());
        }

        public void Stop()
        {
            var communicator_ = communicator;

            if (communicator_ != null)
            {
                communicator_.Break();

                lock (disposeLock)
                {
                    if (!disposed)
                    {
                        disposed = true;
                        communicator_.Dispose();
                    }
                }
            }

            communicator = null;

            PacketData.CancelAllAnalysis();
        }

        protected abstract PacketDevice GetPacketDevice();

        protected void CreateCommunicator()
        {
            var packetDevice = GetPacketDevice();

            PacketDeviceOpenAttributes openAttributes = isPromiscous ? PacketDeviceOpenAttributes.Promiscuous : PacketDeviceOpenAttributes.None;
            communicator = packetDevice.Open(65536, openAttributes, 1000);

            if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
            {
                throw new Exception("This program works only on Ethernet networks.");
            }

            communicator.SetFilter(filter);
        }

        public static bool IsValidCaptureFilter(string filter)
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
