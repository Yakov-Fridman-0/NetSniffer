using NetSnifferLib;
using NetSnifferLib.Analysis;
using NetSnifferLib.Statistics;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSnifferApp
{
    public partial class MainForm : Form
    {
        private SniffingOptions _sniffingOptions;
        private NetSniffer _netSniffer;

        GeneralStatisticsForm statisticsForm;
        TopologyForm topologyForm;

        public MainForm()
        {
            InitializeComponent();

            _sniffingOptions = new SniffingOptions();
            _netSniffer = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CmbNetInterface.Items.AddRange(NetInterfaceItem.GetItems());
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (CmbNetInterface.SelectedItem == null)
            {
                var text = "Please select a network interface first";
                var caption = "Sniffing Error";

                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (!packetFilter.ValidFilter)
            {
                var text = "Please choose a valid capture filter";
                var caption = "Sniffing Error";

                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            packetFilter.Enabled = false;
            CtrlPacketViewer.Clear();

            var selectedInterface = ((NetInterfaceItem)CmbNetInterface.SelectedItem).NetworkInterface;


            UnicastIPAddressInformation[] localComputerIPAddresses = new UnicastIPAddressInformation[selectedInterface.GetIPProperties().UnicastAddresses.Count];
            selectedInterface.GetIPProperties().UnicastAddresses.CopyTo(localComputerIPAddresses, 0 );
            IPAddress localComputerIPv4Address = localComputerIPAddresses.FirstOrDefault((info) => info.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Address;

            PhysicalAddress myPhysicalAddress =  selectedInterface.GetPhysicalAddress();

            PacketAnalyzer.LocalComputerIPAddress = localComputerIPv4Address;
            PacketAnalyzer.LocalComputerPhysicalAddress = myPhysicalAddress;

            _sniffingOptions.NetworkInterface = selectedInterface;

            StartSniffingAsync();
        }

        private Task StartSniffingAsync()
        {
            return Task.Run(() => StartSniffing());
        }

        private void StartSniffing()
        {
            _netSniffer = NetSniffer.CreateLiveSniffer(_sniffingOptions);
            PacketAnalyzer.NetSniffer = _netSniffer;

            _sniffingOptions = new SniffingOptions();

            _netSniffer.PacketReceived += NetSniffer_PacketReceived;
            _netSniffer.Start();
        }

        private void NetSniffer_PacketReceived(object sender, PcapDotNet.Packets.Packet e)
        {
            var packet = e;
            OnPacketReceived(packet);
        }

        private void OnPacketReceived(PcapDotNet.Packets.Packet packet)
        {
            CtrlPacketViewer.Add(packet);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {

            var netSniffer = _netSniffer;

            if (netSniffer == null)
                return;

            netSniffer.PacketReceived -= NetSniffer_PacketReceived;
            netSniffer.Stop();

            packetFilter.Enabled = true;
        }

        private Task<bool> SaveFile(string fileName)
        {    
            var packets = CtrlPacketViewer.GetSelectedPackets();

            return Task<bool>.Factory.StartNew(() => 
            {
                try
                {
                    var tmpFileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".pcap"));

                    PcapFile.Save(tmpFileName, packets);

                    if (System.IO.File.Exists(tmpFileName) == false)
                        return false;

                    System.IO.File.Copy(tmpFileName, fileName, true);

                    System.IO.File.Delete(tmpFileName);

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    return false;
                }
            });
        }

        private Task<bool> OpenFile(string fileName)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    CtrlPacketViewer.Clear();

                    var pakects = PcapFile.Read(fileName);

                    CtrlPacketViewer.AddRange(pakects);

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    return false;
                }
            });
        }

        private async void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var opneFileDialog = new OpenFileDialog()
            {
                FileName = "Select a file",
                Filter = "Pcap files (*.pcap)|*.pcap",
                Title = "Opne pcap file",
            };

            var result = opneFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;
            var fileName = opneFileDialog.FileName;

            var saveResult = await OpenFile(fileName);

            if (saveResult == false)
            {
                var message = $"Failed to opne file \"{fileName}\".";
                var caption = "File Error";

                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                FileName = "Select a file",
                Filter = "Pcap files (*.pcap)|*.pcap",
                Title = "Save pcap file"
            };

            var result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            var fileName = saveFileDialog.FileName;

            var saveResult = await SaveFile(fileName);

            if (saveResult == false)
            {
                var message = $"Failed to save file \"{fileName}\".";
                var caption = "File Error";

                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralStatisticsForm form = new();

            statisticsForm = form;
            statisticsForm.NewStatisticsRequired += statisticsForm_NewStatisticsRequired;

            form.Show();
        }

        private void statisticsForm_NewStatisticsRequired(object sender, EventArgs e)
        {
            statisticsForm.SendNewStatistics(PacketAnalyzer.GetGeneralStatistics());
        }

        private void PacketFilter_FilterChanged(object sender, string e)
        {
            if (string.IsNullOrEmpty(e))
                return;

            string filter = e;

            if (NetSniffer.IsValidFilter(filter))
            {
                packetFilter.ValidFilter = true;
                _sniffingOptions.Filter = filter;
            }
            else
            {
                packetFilter.ValidFilter = false;
            }
        }

        private void generalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            topologyForm = new();
            topologyForm.Show();
            topologyForm.TopologyUpdateRequired += TopologyForm_TopologyUpdateRequired;
        }

        public void UpdateTopology()
        {
            topologyForm.UpdateLanMap(PacketAnalyzer.GetLanMap());
            topologyForm.UpdateWanMap(PacketAnalyzer.GetWanMap());
        }

        private void TopologyForm_TopologyUpdateRequired(object sender, EventArgs e)
        {
            topologyForm?.Invoke(new Action(() => UpdateTopology()));

        }
    }
}
