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

        Color regularColor = Color.FromKnownColor(KnownColor.Control);
        Color selectedColor = Color.Yellow;

        bool tracert = false;

        public bool IsRouter { get; private set; } = false;

        public bool IsServer { get; private set; } = false;

        public WanHostControl()
        {
            InitializeComponent();
        }

        public event EventHandler SelectionStateChangedByUser = delegate { };

        public bool Marked { get; private set; } = false;

        public bool IsSelected { get; private set; } = false;

        public void MarkSelection()
        {
            IsSelected = true;
            selectToolStripMenuItem.Text = "Unselect";
            BackColor = selectedColor;
        }

        public void UnMarkSelection()
        {
            IsSelected = false;
            selectToolStripMenuItem.Text = "Select";
            BackColor = regularColor;
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

        public bool IsLanRouter { get; set; } = false;

        public void BecomeLanRouter()
        {
            IsLanRouter = true;

            regularColor = Color.DarkBlue;
            selectedColor = Color.DarkOrange;

            if (Marked)
                BackColor = selectedColor;
            else
                BackColor = regularColor;
        }

        public void BecomeWanRouter()
        {
            IsLanRouter = false;

            regularColor = Color.RoyalBlue;
            selectedColor = Color.Orange;

            if (Marked)
                BackColor = selectedColor;
            else
                BackColor = regularColor;
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

        private void TracertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            regularColor = Color.DeepSkyBlue;
            selectedColor = Color.Salmon;

            BackColor = IsSelected ? selectedColor : regularColor;

            PacketAnalyzer.Analyzer.TracertAsync(_host.IPAddress);
            PacketAnalyzer.Analyzer.TracertCompleted += Analyzer_TracertCompleted;
        }

        public event EventHandler TracertCompleted = delegate { };

        private void Analyzer_TracertCompleted(object sender, System.Net.IPAddress e)
        {
            if (Host.IPAddress.Equals(e))
            {
                PacketAnalyzer.Analyzer.TracertCompleted -= Analyzer_TracertCompleted;

                TracertCompleted.Invoke(this, new EventArgs());
            }
        }

        private void ShowTCPConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TcpStreamsForm()
            {
                Host = _host
            };

            form.Show();
            //form.StartRefreshing();
        }

        private void CopyIPAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_host.IPAddress.ToString());
        }

        private void SelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                UnMarkSelection();
                SelectionStateChangedByUser.Invoke(this, new EventArgs());
            }
            else
            {
                MarkSelection();
                SelectionStateChangedByUser.Invoke(this, new EventArgs());
            }
        }

        private void keepIPAddressShownToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
