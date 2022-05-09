using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetSnifferLib;
using NetSnifferLib.Topology;


namespace NetSnifferApp
{
    public partial class SimpleLanHostControl : UserControl
    {
        public SimpleLanHostControl()
        {
            InitializeComponent();
        }

        bool _isIPAddressShown = false;

        bool _isLive;

        public bool IsLive
        {
            get => _isLive;
            set
            {
                _isLive = value;

                if (_isLive)
                {

                }
                else
                {

                }
            }
        }

        public bool IsIPAddressShown
        {
            get => _isIPAddressShown;
            set
            {
                _isIPAddressShown = value;

                if (_isIPAddressShown)
                {
                    copyIPAddressToolStripMenuItem.Visible = false;
                    ipAddressLabel.Visible = false;
                }
                else
                {
                    copyIPAddressToolStripMenuItem.Visible = false;
                    ipAddressLabel.Visible = false;
                }
            }
        }

        LanHost _host = null;

        public LanHost Host
        {
            get => _host;
            set
            {
                _host = value;
                physicalAddressLabel.Text = AddressFormat.ToString(_host.PhysicalAddress);

                if (_host.IPAddress != IPAddress.Any)
                {
                    ShowIPAddress();
                    _isIPAddressShown = true;
                }
            }
        }

        WanHost _wanHost = null;

        public WanHost WanHost
        {
            get => _wanHost;
            set
            {
                _wanHost = value;

                if (_wanHost != null)
                {

                }
                else
                {

                }
            }
        }

        public bool IsRouter { get; private set; } = false;

        public bool IsServer { get; private set; } = false;

        public void BecomeRouter()
        {
            IsRouter = true;

/*            if (IsServer)
                BecomeRouterAndServer();
            else
                pictureBox.Image = imageList.Images["Router"];*/
        }

        public void BecomeServer()
        {
            IsServer = true;

/*            if (IsRouter)
                BecomeRouterAndServer();
            else
                pictureBox.Image = imageList.Images["Server"];*/
        }

        protected void BecomeRouterAndServer()
        {
/*            pictureBox.Image = imageList.Images["RouterAndServer"];*/
        }

        public void ShowIPAddress()
        {
            _isIPAddressShown = true;

            ipAddressLabel.Visible = true;
            ipAddressLabel.Text = AddressFormat.ToString(_host.IPAddress);

            showTCPConnectionsToolStripMenuItem.Visible = true;

            copyIPAddressToolStripMenuItem.Visible = true;
        }

        public void HideIPAddress()
        {
            _isIPAddressShown = false;

            ipAddressLabel.Visible = false;
            copyIPAddressToolStripMenuItem.Visible = false;
        }

        private void copyMACAdrressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_host.PhysicalAddress.ToString());
        }

        private void copyIPAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_host.IPAddress.ToString());
        }

        private void showTCPConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TcpStreamsForm()
            {
                Host = _wanHost
            };

            form.Show();
            form.StartRefreshing();
        }
    }
}
