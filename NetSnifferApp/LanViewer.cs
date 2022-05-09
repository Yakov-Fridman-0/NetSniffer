using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;

using NetSnifferLib.Topology;

namespace NetSnifferApp
{
    public partial class LanViewer : UserControl
    {
        public bool IsFull => hostsCount == MAX_HOST_COUNT;

        int hostsCount = 0;

        const int MAX_HOST_COUNT = PANEL_NUM * HostViewer.MAX_HOST_COUNT;

        readonly ConcurrentDictionary<LanHost, HostViewer> hostViewersByHost = new();

        public bool IsLive { get; set; }

        public LanViewer()
        {
            InitializeComponent();

            InitPanelsAndHostViewers();
        }

        const int PANEL_NUM = 25;
        int panelInd = 0;

        readonly Panel[] panels = new Panel[PANEL_NUM];
        readonly HostViewer[] hostViewers = new HostViewer[PANEL_NUM];

        void InitPanelsAndHostViewers()
        {
            IAsyncResult[] results = new IAsyncResult[PANEL_NUM];

            if (!IsHandleCreated)
                _ = Handle;

            for (int i = 0; i < PANEL_NUM; i++) 
            {
                results[i] = BeginInvoke((Action<int>)CreatePanel, i);
            }

            //foreach (var result in results)
            //WaitHandle.WaitAny(results.Select(result => result.AsyncWaitHandle).ToArray());

            //panels[0].Visible = true;
        }

        void CreatePanel(int index)
        {
            Panel panel = new()
            {
                Dock = DockStyle.Fill,
                Visible = index != 0
            };

            HostViewer hostViewer = new()
            {
                Dock = DockStyle.Fill,
                Visible = true
            };

            hostViewers[index] = hostViewer;

            Controls.Add(hostViewer);

            panel.Controls.Add(hostViewer);

            mainPanel.Controls.Add(panel);
            panels[index] = panel;
        }

        int currentHostViewerIndex = 0;

        public void AddHost(LanHost host)
        {
            if (IsFull)
                throw new InvalidOperationException($"LanViewer is full");

            var viewer = hostViewers[currentHostViewerIndex];
            hostViewersByHost.TryAdd(host, viewer);

            viewer.AddHost(host);

            if (Array.IndexOf(hostViewers, viewer) == panelInd)
            {
                panels[panelInd].Invalidate();
                viewer.Invalidate();
                Invalidate();

                panels[panelInd].Visible = false;
                panels[panelInd].Visible = true;
            }

            Interlocked.Increment(ref hostsCount);

            if (viewer.IsFull)
                Interlocked.Increment(ref currentHostViewerIndex);
        }

        public void RemoveHost(LanHost host)
        {
            hostViewersByHost[host].RemoveHost(host);
        }

        public void AssociateWanHostWithLanHost(LanHost lanHost, WanHost wanHost)
        {
            hostViewersByHost[lanHost].AssociateWanHostWithLanHost(lanHost, wanHost);
        }

        public bool IsHostIPAddressShown(LanHost host)
        {
            return hostViewersByHost[host].IsHostIPAddressShown(host);
        }

        public void ShowHostIPAddress(LanHost host)
        {
            hostViewersByHost[host].ShowHostIPAddress(host);
        }

        public void HideHostIPAddress(LanHost host)
        {
            hostViewersByHost[host].HideHostIPAddress(host);
        }

        public void MakeHostRouter(LanHost host)
        {
            hostViewersByHost[host].MakeHostRouter(host);
        }

        public void MakeHostServer(LanHost host)
        {
            hostViewersByHost[host].MakeHostServer(host);
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            MoveToPanel(panelInd - 1);
            UpdateIndexLabel();

            if (panelInd == 0)
                prevButton.Enabled = false;

            if (!nextButton.Enabled)
                nextButton.Enabled = true;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            MoveToPanel(panelInd + 1);
            UpdateIndexLabel();

            if (panelInd == PANEL_NUM - 1) 
                nextButton.Enabled = false;

            if (!prevButton.Enabled)
                prevButton.Enabled = true;
        }

        void MoveToPanel(int newPanelInd)
        {
            panels[panelInd].Visible = false;
            panels[newPanelInd].Visible = true;

            panelInd = newPanelInd;
        }

        void UpdateIndexLabel()
        {
            indLabel.Text = $"{panelInd + 1}/{PANEL_NUM}";
        }
    }
}
