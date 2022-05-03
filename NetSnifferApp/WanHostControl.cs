using System;
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
using NetSnifferLib.Analysis;

namespace NetSnifferApp
{
    public partial class WanHostControl : UserControl, IHostControl
    {
        WanHost _host;

        public WanHost Host
        {
            get => _host;
            set
            {
                _host = value;

                addressToolTip.SetToolTip(pictureBox, _host.IPAddress.ToString());
            }
        }

        public Point Center
        {
            get => new((Location.X + Width) / 2, (Location.Y + Height) / 2);
            set
            {
                var x = value.X;
                var y = value.Y;

                var locationX = x - Width / 2;
                var locationY = y - Height / 2;

                Location = new Point(locationX, locationY);
            }
        }

        public int CenterX
        {
            get => Location.X + Width / 2;
        }

        public int CenterY
        {
            get => Location.Y + Height / 2;
        }

        bool _isLive;

        public bool IsLive
        {
            get => _isLive;
            set
            {
                _isLive = value;
                
                if (_isLive)
                {
                    //showTCPConnectionsToolStripMenuItem.Visible = true;
                    tracertToolStripMenuItem.Visible = true;
                }
                else
                {
                    //showTCPConnectionsToolStripMenuItem.Visible = false;
                    tracertToolStripMenuItem.Visible = false;
                }
            }
        }

        public bool IsRouter { get; private set; } = false;

        public bool IsServer { get; private set; } = false;

        public WanHostControl()
        {
            InitializeComponent();
        }

        public bool Marked { get; private set; } = false;

        public void Mark()
        {
            Marked = true;
            markToolStripMenuItem.Text = "Unmark";
            BackColor = Color.Yellow;
        }

        public void UnMark()
        {
            Marked = false;
            markToolStripMenuItem.Text = "Mark";
            BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        public void BecomeRouter()
        {
            IsRouter = true;

            if (IsServer)
                BecomeRouterAndServer();
            else
                pictureBox.Image = imageList.Images["Router"];

            Invalidate();
        }

        public void BecomeServer()
        {
            IsServer = true;

            if (IsRouter)
                BecomeRouterAndServer();
            else
                pictureBox.Image = imageList.Images["Server"];

            Invalidate();
        }

        void BecomeRouterAndServer()
        {
            pictureBox.Image = imageList.Images["RouterAndServer"];
            IsRouter = true;
            IsServer = true;
            Invalidate();
        }

        async private void tracertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await PacketAnalyzer.Analyzer.TracertAsync(_host.IPAddress);
        }

        private void showTCPConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TcpStreamsForm()
            {
                Host = _host
            };

            form.Show();
            form.StartRefreshing();
        }

        private void copyIPAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_host.IPAddress.ToString());
        }

        private void markToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Marked)
            {
                UnMark();
            }
            else
            {
                Mark();
            }
        }

        private void keepIPAddressShownToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
