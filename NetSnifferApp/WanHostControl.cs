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
                ipAddressLabel.Text = AddressFormat.ToString(_host.IPAddress);
            }
        }

        public int CenterX => pictureBox.Location.X + pictureBox.Width / 2;

        public int CenterY => pictureBox.Location.Y + pictureBox.Height / 2;

        public bool IsRouter { get; private set; } = false;

        public bool IsServer { get; private set; } = false;

        public WanHostControl()
        {
            InitializeComponent();
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

        private void ipAddressLabel_TextChanged(object sender, EventArgs e)
        {
            Width = ipAddressLabel.Location.X + ipAddressLabel.Width + 2;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);

            var graphics = e.Graphics;

            Image image;
            if (IsRouter)
            {
                if (IsServer)
                    image = imageList.Images["RouterAndServer"];
                else
                    image = imageList.Images["Router"];
            }
            else if (IsServer)
            {
                image = imageList.Images["Server"];
            }
            else
            {
                image = imageList.Images["Host"];
            }

            graphics.DrawImage(image, 0, 0);
            var font = ipAddressLabel.Font;

            var brush = new SolidBrush(Color.Black);
            graphics.DrawString(_host.IPAddress.ToString(), font, brush, new Point(50, 0));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            var graphics = e.Graphics;

            Image image;
            if (IsRouter)
            {
                if (IsServer)
                    image = imageList.Images["RouterAndServer"];
                else
                    image = imageList.Images["Router"];
            }
            else if (IsServer)
            {
                image = imageList.Images["Server"];
            }
            else
            {
                image = imageList.Images["Host"];
            }

            graphics.DrawImage(image, 0, 0);
            var font = ipAddressLabel.Font;

            var brush = new SolidBrush(Color.Black);
            graphics.DrawString(_host.IPAddress.ToString(), font, brush, new Point(50, 0));
        }

        private void tracertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PacketAnalyzer.Analyzer.Tracert(_host.IPAddress);
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
    }
}
