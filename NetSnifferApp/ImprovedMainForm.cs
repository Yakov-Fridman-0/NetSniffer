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
        //readonly ActionBlock<Packet> addPacketActionBlock;

        GeneralStatisticsForm statisticsForm = null;
        GeneralTopologyForm topologyForm = null;

        GeneralTopologyForm spareTopologyForm = null;
        bool hasSpareTopologyForm = false;

        bool isLiveCapture = true;
        bool captureStarted = false;
        bool isCapturing = false;
        bool captureEnded = false;
        bool isCaptureUnsaved = false;


        NetworkInterface selectedInterface;
        bool isPromiscuous = true;
        string captureFilterString = string.Empty;
        int numberOfPackets = 0;

        string fileName = string.Empty;

        bool isPrivateFile = false;
        string privateFileName = string.Empty;


        NetSniffer sniffer;

        bool isValidFilter = true;
        bool isOperationalInterface = false;

        readonly AccountManager accountManager;

        bool isSignedIn = false;
        string username = null;

        public ImprovedMainForm()
        {
            InitializeComponent();

            //addPacketActionBlock = new ActionBlock<Packet>(new Action<Packet>(packet => packetViewer.AddPacket(packet)));
            accountManager = AccountManager.Create();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SwitchToStartPanel();
            var items = NetInterfaceItem.CreateItems(NetworkInterface.GetAllNetworkInterfaces());
            interfaceComboBox.Items.AddRange(items);

            spareTopologyForm = new();
            hasSpareTopologyForm = true;
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
                        bool saved = SaveCaptureOnFS();
                        
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

        void StartOfflineCapture()
        {
            captureStarted = true;
            isCapturing = true;
            isCaptureUnsaved = false;

            if (isPrivateFile)
            {
                var fileName = AccountManager.CreateTempFile(username, privateFileName);
            }
            var args = new OfflineSnifferArgs()
            {
                FileName = fileName,
                CaptureFilter = captureFilterString,
                NumberOfPackets = numberOfPackets
            };

            sniffer = new OfflineSniffer(args);

            
            if (statisticsForm != null)
            {
                statisticsForm.IsLive = false;
            }
            if (topologyForm != null)
            {
                topologyForm.IsLive = false;
            }

            sniffer.PacketReceived += Sniffer_PacketReceived;
            sniffer.PacketLimitReached += Sniffer_PacketLimitReached;
            sniffer.CaptureStopped += OfflineSniffer_CaptureStopped;

            if (isPrivateFile)
                sniffer.CaptureStopped += DeleteTempFile;

            sniffer.StartAsync();
        }

        private void OfflineSniffer_CaptureStopped(object sender, EventArgs e)
        {
            captureEnded = true;
        }

        private void DeleteTempFile(object sender, EventArgs e)
        {
            AccountManager.DeleteTempFile(fileName);
        }

        void StartLiveCapture()
        {
            captureStarted = true;

            isCapturing = true;
            isLiveCapture = true;
            isCaptureUnsaved = true;

            if (statisticsForm != null)
            {
                if(!statisticsForm.IsLive)
                {
                    statisticsForm.IsLive = true;
                }

                if (!statisticsForm.CaptureStarted)
                {
                    statisticsForm.CaptureStarted = true;
                }
            }

            if (topologyForm != null)
            {
                if (!topologyForm.IsLive)
                {
                    topologyForm.IsLive = true;
                }

                if (!topologyForm.CaptureStarted)
                {
                    topologyForm.CaptureStarted = true;
                }
            }

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

            if (statisticsForm != null)
            {
                statisticsForm.IsLive = true;
                statisticsForm.CaptureStarted = true;

                ActivateStatisticsForm();
            }
            if (topologyForm != null)
            {
                topologyForm.IsLive = true;
                topologyForm.CaptureStarted = true;

                ActivateTopologyForm();
            }
        }

        private void Sniffer_PacketLimitReached(object sender, EventArgs e)
        {

        }

        private void Sniffer_PacketReceived(object sender, PacketData e)
        {
            packetViewer.AddPacket(e);
        }

        void StopLiveCapture()
        {
            captureEnded = true;
            isCapturing = false;

            if (statisticsForm != null)
            {
                statisticsForm.CaptureStopped = true;
                statisticsForm.StopRequestingUpdates();
            }

            if (topologyForm != null)
            {
                topologyForm.CaptureEnded = true;
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

            //statisticsToolStripMenuItem.Enabled = false;
            //topologyToolStripMenuItem.Enabled = false;
        }

        private void GeneralStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statisticsForm != null)
                return;

            if (captureStarted)
            {
                statisticsForm = new GeneralStatisticsForm
                {
                    CaptureStarted = true,
                    IsLive = isLiveCapture
                };

                statisticsForm.SetBaseTime(sniffer.StartingTime);

                if (captureEnded)
                {
                    statisticsForm.CaptureStopped = true;
                    statisticsForm.UpdateStatistics(PacketAnalyzer.Analyzer.GetGeneralStatistics());
                }
                else if (isLiveCapture)
                {
                    ActivateStatisticsForm();
                }
            }
            else
            {
                statisticsForm = new GeneralStatisticsForm
                {
                    CaptureStarted = false
                };
            }

            statisticsForm.FormClosed += StatisticsForm_FormClosed;
            statisticsForm.Show();
        }

        void ActivateStatisticsForm()
        {
            statisticsForm.StatisticsUpdateRequested += StatisticsForm_NewStatisticsRequested;
            statisticsForm.StartRequestingUpdates();

            sniffer.SniffingStarted.WaitOne();
            statisticsForm.SetBaseTime(sniffer.StartingTime);
        }

        private void GeneralTopologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topologyForm != null)
                return;

            if (hasSpareTopologyForm)
            {
                hasSpareTopologyForm = false;
                topologyForm = spareTopologyForm;
            }
            else
            {
                topologyForm = new();
            }

            if (captureStarted)
            {
                if (captureEnded)
                {
                    topologyForm.CaptureEnded = true;

                    topologyForm.FormClosed += TopologyForm_FormClosed;
                    topologyForm.Show();
                    topologyForm.UpdateTopology(
                        PacketAnalyzer.Analyzer.GetLanMap(),
                        PacketAnalyzer.Analyzer.GetWanMap());

                    return;
                }
                else if (isLiveCapture)
                {
                    topologyForm.IsLive = true;
                    ActivateTopologyForm();
                }
            }
            else
            {
                topologyForm.CaptureStarted = false;
            }

            topologyForm.FormClosed += TopologyForm_FormClosed;
            topologyForm.Show();
        }

        void ActivateTopologyForm()
        {
            topologyForm.TopologyUpdateRequested += TopologyForm_TopologyUpdateRequested;
            topologyForm.StartReuqestingUpdates();
        }
        private void TopologyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            topologyForm = null;
        }


        private void TopologyForm_TopologyUpdateRequested(object sender, EventArgs e)
        {
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

        private void InterfaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void StartButton_Click(object sender, EventArgs e)
        {
            SwitchToCapturePanel();
            AdjustCapturePanel();

            StartLiveCapture();
        }

        private void PromiscuousCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isPromiscuous = promiscuousCheckBox.Checked;
        }

        private void CaptureFilter_FilterChanged(object sender, EventArgs e)
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

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        bool SaveCaptureOnFS()
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

        private void NewCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCapturing)
                StopLiveCapture();
            
            if (topologyForm != null)
                topologyForm.StopRequestingUpdates();

            if (isCaptureUnsaved)
            {
                var dialogResult = MessageBox.Show("Do you want to save the capture before starting a new one?", "Save Capture", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        var saved = SaveCaptureOnFS();

                        if (saved)
                        {
                            packetViewer.Clear();

                            if (statisticsForm != null)
                            {
                                statisticsForm.Clear();
                                statisticsForm.SetBaseTime(DateTime.Now);
                                statisticsForm.IsLive = true;
                                statisticsForm.StartRequestingUpdates();
                            }
                            if (topologyForm != null)
                            {
                                topologyForm.Clear();
                            }

                            SwitchToStartPanel();
                        }
                        break;

                    case DialogResult.No:
                        isCaptureUnsaved = false;

                        packetViewer.Clear();

                        if (statisticsForm != null)
                        {
                            statisticsForm.Clear();
                            statisticsForm.SetBaseTime(DateTime.Now);
                            statisticsForm.IsLive = true;
                            statisticsForm.StartRequestingUpdates();
                        }
                        if (topologyForm != null)
                        {
                            topologyForm.Clear();
                        }

                        SwitchToStartPanel();
                        break;

                    case DialogResult.Cancel:
                        break;
                }
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            StopLiveCapture();

            isLiveCapture = true;
            isCapturing = true;
            isCaptureUnsaved = true;

            packetViewer.Clear();

            if (statisticsForm != null)
            {
                statisticsForm.Clear();
                statisticsForm.SetBaseTime(DateTime.Now);
                statisticsForm.IsLive = true;
                statisticsForm.StartRequestingUpdates();
            }
            if (topologyForm != null)
            {
                topologyForm.Clear();
            }

            AdjustCapturePanel();

            StartLiveCapture();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopLiveCapture();

            stopButton.Text = "Stopped";
            stopButton.Enabled = false;
        }

        private void PacketNumberUpDown_ValueChanged(object sender, EventArgs e)
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
                Invoke (new MethodInvoker( () => attackToolStripMenuItem.Image = imageList1.Images[0]));
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (isSignedIn)
            {
                isSignedIn = false;
                username = null;

                openPrivateFileToolStripMenuItem.Enabled = false;
                savePrivateFileToolStripMenuItem.Enabled = false;

                usernameLabel.Text = "Not Signed In";
                linkLabel1.Text = "Sign In";
            }
            else
            {
                var signInForm = new SignInForm()
                {
                    AccountManager = accountManager
                };

                signInForm.ShowDialog();

                if (signInForm.IsSignedIn)
                {
                    openPrivateFileToolStripMenuItem.Enabled = true;
                    savePrivateFileToolStripMenuItem.Enabled = true;

                    isSignedIn = true;
                    username = signInForm.Username;

                    usernameLabel.Text = username;
                    linkLabel1.Text = "Log Out";
                }
            }
        }

        private void SaveFSFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCapturing)
            {
                MessageBox.Show("Stop cature before saving", "Capture Running", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                SaveCaptureOnFS();
            }
        }

        private void SavePrivateFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveCaptureDialog = new SavePrivateCaptureDialog();
            var result = saveCaptureDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileName = System.IO.Path.GetRandomFileName();

                sniffer.SaveCapture(fileName, saveCaptureDialog.DispalyFilterString);
                AccountManager.MoveFileToStorage(username, saveCaptureDialog.PrivateFileName, fileName);

                if (isCaptureUnsaved)
                {
                    isCaptureUnsaved = false;
                }
            }
        }

        private void OpenFSFileToolStripMenuItem_Click(object sender, EventArgs e)
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

                isPrivateFile = false;

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

        private void OpenPrivateFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openCaptureDialog = new OpenPrivateCaptureDialog()
            {
                Username = username
            };

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

                isPrivateFile = true;
                privateFileName = openCaptureDialog.FileName;

                fileName = AccountManager.CreateTempFile(username, privateFileName);

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
    }
}
