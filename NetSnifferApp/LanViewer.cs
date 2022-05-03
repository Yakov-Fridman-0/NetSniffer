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
    public partial class LanViewer : UserControl
    {
        int numberOfHosts = 0;
        readonly Dictionary<LanHost, LanHostControl> hostControls = new();

        const int hostWidth = 128;
        const int hostHeight = 143;

        int rows = 3;
        int columns = 3;

        int currentRow = 0;
        int currentColumn = 0;

        public bool IsLive { get; set; }
        
        public LanViewer()
        {
            InitializeComponent();
        }

        public void AddHost(LanHost host)
        {
            LanHostControl control = null;
            hostsTableLayoutPanel.Invoke((MethodInvoker) delegate
            {
                //hostsTableLayoutPanel.SuspendLayout();
                control = new LanHostControl
                {
                    Host = host,

                    IsLive = IsLive
                };
            });

            Invoke(new MethodInvoker(() => Controls.Add(control)));

            numberOfHosts++;
            hostControls.Add(host, control);

            //hostsTableLayoutPanel.SuspendLayout();
            PlaceHostContorl(control);
            //shostsTableLayoutPanel.ResumeLayout();
        }

        public Task AddHostAsync(LanHost host)
        {
            return Task.Run(() => AddHost(host));
        }
        
        void PlaceHostContorl(LanHostControl control)
        {
            control.Invoke(new MethodInvoker(
                () =>
                {
                    hostsTableLayoutPanel.SuspendLayout();
                    hostsTableLayoutPanel.Controls.Add(control, currentColumn, currentRow);
                    control.Dock = DockStyle.Fill;

                    currentColumn++;

                    if (currentColumn == columns)
                    {
                        currentColumn = 0;

                        currentRow++;
                        if (currentRow == rows)
                        {
                            hostsTableLayoutPanel.RowCount++;
                        }
                    }
                    hostsTableLayoutPanel.ResumeLayout();
                }));
        }

        public void RemoveHost(LanHost host)
        {

        }

        public void AssociateWanHostWithLanHost(LanHost lanHost, WanHost wanHost)
        {
            hostControls[lanHost].WanHost = wanHost;
        }

        public bool IsHostIPAddressShown(LanHost host)
        {
            return hostControls[host].IsIPAddressShown;
        }

        public void ShowHostIPAddress(LanHost host)
        {
            hostControls[host].ShowIPAddress();
        }

        public void HideHostIPAddress(LanHost host)
        {
            hostControls[host].HideIPAddress();
        }

        public void MakeHostRouter(LanHost host)
        {
            hostControls[host].BecomeRouter();
        }

        public void MakeHostServer(LanHost host)
        {
            hostControls[host].BecomeServer();
        }

        //int width = Width - Padding.Left - Padding.Right;

        //hostsTableLayoutPanel.Width = width;

        //int numberOfHostsPerRows = width / hostWidth;

        //if (numberOfHostsPerRows != columns)
        //{
        //    columns = numberOfHostsPerRows;
        //    rows = numberOfHosts / numberOfHostsPerRows + 1;

        //    hostsTableLayoutPanel.Controls.Clear();

        //    hostsTableLayoutPanel.Height = (hostHeight + 6) * rows;
        //    hostsTableLayoutPanel.ColumnCount = columns;
        //    hostsTableLayoutPanel.RowCount = rows;

        //    hostsTableLayoutPanel.SuspendLayout();

        //    currentRow = 0;
        //    currentColumn = 0;
        //    foreach (var control in allHostControls.Values)
        //        PlaceHostContorl(control);

        //    hostsTableLayoutPanel.ResumeLayout();
        //}
    }
}
