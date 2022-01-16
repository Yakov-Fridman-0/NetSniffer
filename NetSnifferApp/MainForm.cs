using NetSnifferLib;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSnifferApp
{
    public partial class MainForm : Form
    {
        private SniffingOptions _sniffingOptions;
        private NetworkSniffer _netSniffer;

        readonly PacketAnalyzer packetAnalyzer;

        bool capturing = false;

        public MainForm()
        {
            InitializeComponent();

            packetAnalyzer = new PacketAnalyzer();
            CtrlPacketViewer.SetAnalyzer(packetAnalyzer);

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

            packetFilter.Enabled = false;
            CtrlPacketViewer.Clear();

            _sniffingOptions.NetworkInterface = ((NetInterfaceItem)CmbNetInterface.SelectedItem).NetworkInterface;

            SartAsync();
        }

        private void SartAsync()
        {
            _netSniffer = NetworkSniffer.CreateLiveSniffer(_sniffingOptions);
            _sniffingOptions = new SniffingOptions();

            var netSniffer = _netSniffer;

            netSniffer.PacketReceived += NetSniffer_PacketReceived;
            netSniffer.StartAsync();

            capturing = true;
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

            capturing = false;

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

        private StatisticsForm _statisticsForm;

        private void GeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var packetStatisticsLogger = packetAnalyzer.PackeStatisticsLogger;

            StatisticsForm statisticForm = new();
            statisticForm.UpdateStatistics(
                packetStatisticsLogger.TotalPacketNumber,
                packetStatisticsLogger.TotalBytes,
                packetStatisticsLogger.EthernetPacketNumber,
                packetStatisticsLogger.EthernetTotalPayloadBytes,
                packetStatisticsLogger.IpV4PacketNumber,
                packetStatisticsLogger.IpV4TotalPayloadBytes,
                packetStatisticsLogger.IpV6PacketNumber,
                packetStatisticsLogger.IpV6TotalPayloadBytes,
                packetStatisticsLogger.UdpPacketNumber,
                packetStatisticsLogger.UdpTotalPayloadBytes,
                packetStatisticsLogger.TcpPacketNumber,
                packetStatisticsLogger.TcpTotalPayloadBytes);


            _statisticsForm = statisticForm;
            statisticsTimer.Start();

            statisticForm.FormClosed += StatisticsForm_Closed;
            statisticForm.Show();
        }

        private void StatisticsForm_Closed(object sender, EventArgs e)
        {
            statisticsTimer.Stop();
        }

        private void StatisticsTimer_Tick(object sender, EventArgs e)
        {
            var packetStatisticsLogger = packetAnalyzer.PackeStatisticsLogger;

            _statisticsForm.UpdateStatistics(
                packetStatisticsLogger.TotalPacketNumber,
                packetStatisticsLogger.TotalBytes,
                packetStatisticsLogger.EthernetPacketNumber,
                packetStatisticsLogger.EthernetTotalPayloadBytes,
                packetStatisticsLogger.IpV4PacketNumber,
                packetStatisticsLogger.IpV4TotalPayloadBytes,
                packetStatisticsLogger.IpV6PacketNumber,
                packetStatisticsLogger.IpV6TotalPayloadBytes,
                packetStatisticsLogger.UdpPacketNumber,
                packetStatisticsLogger.UdpTotalPayloadBytes,
                packetStatisticsLogger.TcpPacketNumber,
                packetStatisticsLogger.TcpTotalPayloadBytes);
        }

        private void PacketFilter_FilterChanged(object sender, string e)
        {
            if (string.IsNullOrEmpty(e))
                return;

            string filter = e;

            if (NetworkSniffer.IsValidFilter(filter))
            {
                packetFilter.ValidFilter = true;
                _sniffingOptions.Filter = filter;
            }
            else
            {
                packetFilter.ValidFilter = false;
            }
        }
    }
}
