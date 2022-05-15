using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetSnifferLib.Topology;

namespace NetSnifferApp
{
    public partial class GeneralTopologyForm : Form
    {
        bool _isLive = true;

        public LanMap LanMap { get; } = LanMap.Empty;

        public WanMap WanMap { get; } = WanMap.Empty;

        public bool IsLive
        {
            get => _isLive;
            set
            {
                _isLive = value;

                if (_isLive)
                {
                    updateTimer.Interval = 500;

                    titleLabel.Visible = false;

                    splitContainer1.Location = new Point(0, 0);
                    splitContainer1.Dock = DockStyle.Fill;
                }
                else
                {
                    titleLabel.Visible = true;
                    titleLabel.Text = "Topology from dump file";

                    splitContainer1.Location = new Point(1, 62);
                    splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
                }
            }
        }

        public GeneralTopologyForm()
        {
            InitializeComponent();
        }

        public event EventHandler TopologyUpdateRequested = delegate { };

        public void StartReuqestingUpdates()
        {
            updateTimer.Start();
        }

        public void StopRequestingUpdates()
        {
            updateTimer.Stop();
            titleLabel.Text = "Capture Stopped";
            titleLabel.BringToFront();
        }

        async public Task UpdateTopologyAsync(LanMap lanMap, WanMap wanMap)
        {
            updateTimer.Stop();

            var lanDiff = await lanMap.GetDiffAsync(LanMap);

            foreach (var host in lanDiff.HostsAdded)
                lanViewer.AddHost(host);


            foreach (var hosts in lanDiff.RoutersAdded)
                lanViewer.MakeHostRouter(hosts);

            foreach (var server in lanDiff.DhcpServersAdded)
                lanViewer.MakeHostServer(server);

            foreach (var host in LanMap.ReadOnlyHosts)
            {
                if (host.IPAddress != System.Net.IPAddress.Any && !lanViewer.IsHostIPAddressShown(host))
                    lanViewer.ShowHostIPAddress(host);

                if (host.IPAddress == System.Net.IPAddress.Any && lanViewer.IsHostIPAddressShown(host))
                    lanViewer.HideHostIPAddress(host);
            }

            //LanMap.Update(lanDiff);
            await LanMap.UpdateAsync(lanDiff);


            var wanDiff = await wanMap.GetDiffAsync(WanMap);
            //var wanDiff = wanMap.GetDiff(WanMap);

            foreach (var wanHost in wanMap.HostsAsReadOnly)
            {
                if (wanHost.IPAddress.Equals(System.Net.IPAddress.Any))
                    continue;

                var lanHost = LanMap.GetHostByIPAddress(wanHost.IPAddress);

                if (lanHost != null)
                {
                    lanViewer.AssociateWanHostWithLanHost(lanHost, wanHost);

                    if (wanViewer.ContainsHost(wanHost))
                        wanViewer.RemoveHost(wanHost);
                }
                else
                {
                    if (!wanViewer.ContainsHost(wanHost))
                        await wanViewer.AddHostAsync(wanHost);
                }

/*                foreach (var otherHost in wanHost.ConnectedHosts)
                {
                    wanViewer.AddConnection(wanHost, otherHost);
                }*/
            }

            foreach (var router in wanDiff.LanRouterAdded)
                await wanViewer.AddLanRouterAsync(router);

            foreach (var wanHost in wanMap.HostsAsReadOnly)
            {
                foreach (var otherHost in wanHost.ConnectedHosts)
                {
                    wanViewer.AddConnection(wanHost, otherHost);
                }
            }

            wanViewer.ShowConnections();

            foreach (var router in wanDiff.WanRoutersAdded)
                wanViewer.MakeHostWanRouter(router);

            foreach (var server in wanDiff.DnsServersAdded)
                wanViewer.MakeHostServer(server);

            WanMap.Update(wanDiff);

            updateTimer.Start();
        }

        void updateTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(() => Invoke(new MethodInvoker(() => TopologyUpdateRequested.Invoke(this, new EventArgs()))));
        }

        private void GeneralTopologyForm_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);

            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;

            lanViewer.IsLive = IsLive;
            wanViewer.IsLive = IsLive;
        }
    }
}
