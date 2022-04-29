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

        public event EventHandler TopologyUpdateRequested;

        public void StartReuqestingUpdates()
        {
            updateTimer.Start();
        }

        public void StopRequestingUpdates()
        {
            updateTimer.Stop();
        }

        public async void UpdateTopology(LanMap lanMap, WanMap wanMap)
        {
            var lanDiff = lanMap.GetDiff(LanMap);

            foreach (var host in lanDiff.HostsAdded)
                await lanViewer.AddHostAsync(host);

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
            
            LanMap.Update(lanDiff);


            var wanDiff = wanMap.GetDiff(WanMap);
            
            foreach (var wanHost in wanMap.HostsAsReadOnly)
            {
                if (wanHost.IPAddress.Equals(System.Net.IPAddress.Any))
                    continue;

                var lanHost = LanMap.GetHostByIPAddress(wanHost.IPAddress);

                if (lanHost != null)
                {
                    lanViewer.AssociateWanHostWithLanHost(lanHost, wanHost);

                    if (wanViewer1.ContainsHost(wanHost))
                        wanViewer1.RemoveHost(wanHost);
                }
                else
                {
                    if (!wanViewer1.ContainsHost(wanHost))
                        wanViewer1.AddHost(wanHost);
                }

                foreach (var otherHost in wanHost.ConnectedHosts)
                {
                    wanViewer1.AddConnection(wanHost, otherHost);
                }
            }

            foreach (var router in wanDiff.LanRouterAdded)
                wanViewer1.AddLanRouter(router);

            foreach (var router in wanDiff.WanRoutersAdded)
                wanViewer1.MakeHostWanRouter(router);

            foreach (var server in wanDiff.DnsServersAdded)
                wanViewer1.MakeHostServer(server);

            WanMap.Update(wanDiff);
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            TopologyUpdateRequested.Invoke(this, new EventArgs());
        }

        private void GeneralTopologyForm_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);

            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
    }
}
