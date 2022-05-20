using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks.Dataflow;

using PcapDotNet.Packets;

using NetSnifferLib;
using NetSnifferLib.Analysis;
using NetSnifferLib.Topology;

using ArpTableWinFormsApp.ArpTable;

namespace NetSnifferApp
{
    public partial class ImprovedMainForm : Form
    {
        readonly ActionBlock<Packet> addPacketActionBlock;

        GeneralStatisticsForm statisticsForm = null;
        GeneralTopologyForm topologyForm = null;

        bool isLiveCapture = true;
        bool isCapturing = false;
        bool captureEnded = false;
        bool isCaptureUnsaved = false;

        
        NetworkInterface selectedInterface;
        bool isPromiscuous = true;
        string captureFilterString = string.Empty;
        int numberOfPackets;
        string fileName;


        NetSniffer sniffer;


        bool isValidFilter = true;
        bool isOperationalInterface = false;

        public ImprovedMainForm()
        {
            InitializeComponent();

            addPacketActionBlock = new ActionBlock<Packet>(new Action<Packet>(packet => packetViewer.AddPacket(packet)));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SwitchToStartPanel();
            var items = NetInterfaceItem.CreateItems(NetworkInterface.GetAllNetworkInterfaces());
            interfaceComboBox.Items.AddRange(items);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isCaptureUnsaved)
            {
                var dialogResult = MessageBox.Show("Do you want to save the capture before quiting?", "Save Capture", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        StopLiveCapture();
                        bool saved = SaveCapture();
                        
                        if (saved)
                            base.OnFormClosing(e);
                        else
                            e.Cancel = true;
                        break;

                    case DialogResult.No:
                        base.OnFormClosing(e);
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var openCaptureDialog = new OpenCaptureDialog();

            var result = openCaptureDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                
                if (isCapturing)
                {
                    if (isLiveCapture)
                        StopLiveCapture();
                    else
                        StopOfflineCapture();
                        
                }
                
                isLiveCapture = false;
                fileName = openCaptureDialog.FileName;
                captureFilterString = openCaptureDialog.CaptureFilterString;
                numberOfPackets = openCaptureDialog.NumberOfPackets;

                if (statisticsForm != null)
                {
                    statisticsForm.Clear(); 
                }

                SwitchToCapturePanel();
                AdjustCapturePanel();

                StartOfflineCapture();
            }
        }

        void StartOfflineCapture()
        {
            isCapturing = true;
            isCaptureUnsaved = false;

            var args = new OfflineSnifferArgs()
            {
                FileName = fileName,
                CaptureFilter = captureFilterString,
                NumberOfPackets = numberOfPackets
            };

            sniffer = new OfflineSniffer(args);

            sniffer.PacketReceived += Sniffer_PacketReceived;
            sniffer.PacketLimitReached += Sniffer_PacketLimitReached;
            sniffer.CaptureStopped += OfflineSniffer_CaptureStopped;
            sniffer.StartAsync();
        }

        private void OfflineSniffer_CaptureStopped(object sender, EventArgs e)
        {

        }

        void StartLiveCapture()
        {
            isCapturing = true;
            isLiveCapture = true;
            isCaptureUnsaved = true;

            var args = new LiveSnifferArgs()
            {
                CaptureFilter = captureFilterString,
                NetworkInterface = selectedInterface,
                IsPromiscuous = isPromiscuous,
                NumberOfPackets = Convert.ToInt32(numberOfPackets)
            };

            sniffer = new LiveSniffer(args);
            
            isCaptureUnsaved = true;
            isCapturing = true;

            sniffer.PacketReceived += Sniffer_PacketReceived;
            sniffer.PacketLimitReached += Sniffer_PacketLimitReached;
            sniffer.StartAsync();
        }

        private void Sniffer_PacketLimitReached(object sender, EventArgs e)
        {

        }

        private void Sniffer_PacketReceived(object sender, Packet e)
        {
            addPacketActionBlock.Post(e);
        }

        void StopLiveCapture()
        {
            isCapturing = false;

            if (statisticsForm != null)
            {
                statisticsForm.StopRequestingUpdates();
            }

            if (topologyForm != null)
            {
                //topologyForm.StopRequestingUpdates();
            }

            sniffer.PacketReceived -= Sniffer_PacketReceived;
            sniffer.Stop();
        }

        void StopOfflineCapture()
        {
            isCapturing = false;

            if (statisticsForm != null)
            {
                statisticsForm.StopRequestingUpdates();
            }

            sniffer.PacketReceived -= Sniffer_PacketReceived;
            sniffer.Stop();
        }

        void SwitchToCapturePanel()
        {
            SuspendLayout();          
            startPanel.Visible = false;
            capturePanel.Visible = true;
            ResumeLayout();

            saveAsToolStripMenuItem.Enabled = true;
            newCaptureToolStripMenuItem.Enabled = true;

            statisticsToolStripMenuItem.Enabled = true;
            topologyToolStripMenuItem.Enabled = true;

            packetViewer.Clear();
        }

        void AdjustCapturePanel()
        {
            if (isLiveCapture)
            {
                stopButton.Text = "Stop";
                stopButton.Enabled = true;
                restartButton.Enabled = true;

                interfaceTitleLabel.Visible = true;
                interfaceNameTextBox.Text = selectedInterface.Name;

                interfaceNameTextBox.Visible = true;
                captureFilterTextBox.Text = captureFilterString;

                captureFilterTitleLabel.Visible = true;
                captureFilterTextBox.Visible = true;

                controlFlowLayoutPanel.Visible = true;

                moreInfoLabel.Visible = false;
            }
            else
            {
                interfaceTitleLabel.Visible = false;
                interfaceNameTextBox.Visible = false;

                captureFilterTitleLabel.Visible = false;
                captureFilterTextBox.Visible = false;

                controlFlowLayoutPanel.Visible = false;

                moreInfoLabel.Visible = true;
            }
        }

        void SwitchToStartPanel()
        {
            SuspendLayout();
            capturePanel.Visible = false;
            startPanel.Visible = true;
            ResumeLayout();

            saveAsToolStripMenuItem.Enabled = false;
            newCaptureToolStripMenuItem.Enabled = false;

            statisticsToolStripMenuItem.Enabled = false;
            topologyToolStripMenuItem.Enabled = false;
        }

        private void generalStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statisticsForm != null)
                return;

            statisticsForm = new GeneralStatisticsForm
            {
                IsLive = isLiveCapture
            };

            statisticsForm.SetBaseTime(sniffer.StartingTime);
            
            statisticsForm.Show();

            if (captureEnded)
            {
                statisticsForm.UpdateStatistics(PacketAnalyzer.Analyzer.GetGeneralStatistics());
            }
            else if (isLiveCapture)
            {
                statisticsForm.StatisticsUpdateRequested += StatisticsForm_NewStatisticsRequested;
                statisticsForm.FormClosed += StatisticsForm_FormClosed;
                statisticsForm.StartRequestingUpdates();
            }
        }

        private void generalTopologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topologyForm == null)
                topologyForm = new();

            topologyForm.IsLive = true;

            topologyForm.Show();

            if (captureEnded)
            {
                topologyForm.UpdateTopology(
                    PacketAnalyzer.Analyzer.GetLanMap(),
                    PacketAnalyzer.Analyzer.GetWanMap());
            }
            else if (isLiveCapture)
            {
                topologyForm.TopologyUpdateRequested += TopologyForm_TopologyUpdateRequested;
                topologyForm.FormClosed += TopologyForm_FormClosed;
                topologyForm.StartReuqestingUpdates();
            }
        }

        async void DoIt()
        {
            await Task.Run(() => CreateForm());
        }

        void CreateForm()
        {
            /*            topologyForm = new GeneralTopologyForm
                        {
                            IsLive = isLiveCapture
                        };*/


        }

        private void TopologyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            topologyForm = null;
        }

        //ActionBlock<(LanMap LanMap, WanMap WanMap)> lanUpdatedBlock;

        private void TopologyForm_TopologyUpdateRequested(object sender, EventArgs e)
        {
            //if (lanUpdatedBlock == null)
            //    lanUpdatedBlock = new (new Action<(LanMap LanMap, WanMap WanMap)>(
            //        async ((LanMap LanMap, WanMap WanMap) tuple) =>
            //    {
            //        await topologyForm.UpdateTopologyAsync(tuple.LanMap, tuple.WanMap);
            //    }));

            //lanUpdatedBlock.Post((PacketAnalyzer.Analyzer.GetLanMap(), PacketAnalyzer.Analyzer.GetWanMap()));

            topologyForm.UpdateTopology(PacketAnalyzer.Analyzer.GetLanMap(), PacketAnalyzer.Analyzer.GetWanMap());
        }

        private void StatisticsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            statisticsForm = null;
        }

        private void StatisticsForm_NewStatisticsRequested(object sender, EventArgs e)
        {
            if (isCapturing)
                statisticsForm.UpdateStatistics(PacketAnalyzer.Analyzer.GetGeneralStatistics());
        }

        private void interfaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (NetInterfaceItem)(interfaceComboBox.SelectedItem);

            if (item != null)
            {
                selectedInterface = item.NetworkInterface;
                interfaceInfo.Interface = selectedInterface;

                isOperationalInterface = IsInterfcaeOperational(selectedInterface);
                if (isOperationalInterface)
                {
                    startButton.Enabled = isValidFilter;
                }
                else
                {
                    startButton.Enabled = false;
                }
            }
            else
            {
                isOperationalInterface = false;
                interfaceInfo.Interface = null;
                startButton.Enabled = false;
            }
        }

        static bool IsInterfcaeOperational(NetworkInterface @interface)
        {
            return @interface.OperationalStatus is OperationalStatus.Up or OperationalStatus.Unknown or OperationalStatus.Testing;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            SwitchToCapturePanel();
            AdjustCapturePanel();

            StartLiveCapture();
        }

        private void promiscuousCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isPromiscuous = promiscuousCheckBox.Checked;
        }

        private void captureFilter_FilterChanged(object sender, EventArgs e)
        {
            string filter = captureFilter.Filter;

            if (NetSniffer.IsValidCaptureFilter(filter))
            {
                captureFilterString = filter;
                captureFilter.IsValidFilter = true;

                isValidFilter = true;
                startButton.Enabled = isOperationalInterface;
            }
            else
            {
                captureFilter.IsValidFilter = false;
                startButton.Enabled = false;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCapturing)
            {
                MessageBox.Show("Stop cature before saving", "Capture Running", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                SaveCapture();
            }
        }

        bool SaveCapture()
        {
            var saveCaptureDialog = new SaveCaptureDialog();
            var result = saveCaptureDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                sniffer.SaveCapture(saveCaptureDialog.FileName, saveCaptureDialog.DispalyFilterString);

                if (isCaptureUnsaved)
                {
                    isCaptureUnsaved = false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void newCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCapturing)
                StopLiveCapture();

            if (isCaptureUnsaved)
            {
                var dialogResult = MessageBox.Show("Do you want to save the capture before starting a new one?", "Save Capture", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        var saved = SaveCapture();

                        if (saved)
                        {
                            SwitchToStartPanel();
                        }
                        break;

                    case DialogResult.No:
                        isCaptureUnsaved = false;
                        SwitchToStartPanel();
                        break;

                    case DialogResult.Cancel:
                        break;
                }
            }
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            isLiveCapture = true;
            isCapturing = true;
            isCaptureUnsaved = true;

            if (statisticsForm != null)
            {
                statisticsForm.Clear();
                statisticsForm.SetBaseTime(DateTime.Now);
                statisticsForm.IsLive = true;
                statisticsForm.StartRequestingUpdates();
            }

            AdjustCapturePanel();

            packetViewer.Clear();
            StartLiveCapture();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopLiveCapture();

            stopButton.Text = "Stopped";
            stopButton.Enabled = false;
        }

        private void packetNumberUpDown_ValueChanged(object sender, EventArgs e)
        {
            numberOfPackets = (int)packetNumberUpDown.Value;
        }

        AttackLogForm attackLogForm;

        private void LogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            attackLogForm = new();
            attackLogForm.FormClosed += delegate { attackLogForm = null; };

            attackToolStripMenuItem.Image = null;

            attackLogForm.Show();
        }

        private void ImprovedMainForm_Load(object sender, EventArgs e)
        {
            topologyForm = new();
        }

        private void ArpTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ArpTableSimple();
            form.Show();
        }

        private void RoutingTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new RouteTabeSimple();
            form.Show();
        }

        private void IpconfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new IpConfigSimple();
            form.Show();
        }

        private void PacketViewer_AttackAdded(object sender, EventArgs e)
        {
            if (attackLogForm == null)
                attackToolStripMenuItem.Image = imageList1.Images[0];
        }
    }
}
