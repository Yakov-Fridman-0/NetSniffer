using NetSnifferLib;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSnifferApp
{
    public partial class MainForm : Form
    {
        NetSniffer _netSniffer;
        readonly PacketAnalyzer packetAnalyzer;

        public MainForm()
        {
            InitializeComponent();

            packetAnalyzer = new();
            CtrlPacketViewer.SetAnalyzer(packetAnalyzer);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CmbNetInterface.Items.AddRange(NetInterfaceItem.GetItems());
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (CmbNetInterface.SelectedItem == null)
            {
                var text = "Please select a network interface first";
                var caption = "Sniffing Error";

                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var networkInterface = ((NetInterfaceItem)CmbNetInterface.SelectedItem).NetworkInterface;

            CtrlPacketViewer.Clear();

            SartAsync(networkInterface);
        }

        private void SartAsync(System.Net.NetworkInformation.NetworkInterface networkInterface)
        {
            var netSniffer = new NetSniffer();

            netSniffer.PacketReceived += NetSniffer_PacketReceived;
            netSniffer.StartAsync(networkInterface);
            
            _netSniffer = netSniffer;
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            var netSniffer = _netSniffer;
            _netSniffer = null;

            if (netSniffer == null)
                return;

            netSniffer.PacketReceived -= NetSniffer_PacketReceived;
            netSniffer.Stop();
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

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
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

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
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

            statisticForm.FormClosed += statisticsForm_Closed;
            statisticForm.Show();
        }

        private void statisticsForm_Closed(object sender, EventArgs e)
        {
            statisticsTimer.Stop();
        }

        private void statisticsTimer_Tick(object sender, EventArgs e)
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
    }
}
