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

        public bool IsRouter { get; private set; } = false;

        public bool IsServer { get; private set; } = false;

        public WanHostControl()
        {
            InitializeComponent();
        }

        public void BecomeRouter()
        {
            pictureBox.Image = imageList.Images["Router"];
            IsRouter = true;
        }
        public void BecomeServer()
        {
            pictureBox.Image = imageList.Images["Server"];
            IsServer = true;
        }

        public void BecomeRouterAndServer()
        {
            pictureBox.Image = imageList.Images["RouterAndServer"];
            IsRouter = true;
            IsServer = true;
        }
    }
}
