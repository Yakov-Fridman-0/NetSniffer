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
    public partial class LanHostControl : UserControl, IHostControl
    {
        LanHost _host = null;
        public LanHost Host
        {
            get => _host;
            set
            {
                _host = value;
                physicalAddressLabel.Text = AddressFormat.ToString(_host.PhysicalAddress);

                if (_host.IPAddress != null)
                    ipAddressLabel.Text = AddressFormat.ToString(_host.IPAddress);
            }
        }

        WanHost _wanHost = null;

        public WanHost WanHost 
        { 
            get
            {
                if (IsRouter)
                    return _wanHost;
                else
                    throw new InvalidOperationException();
            }
            set
            {
                if (IsRouter)
                    _wanHost = value;
                else
                    throw new InvalidOperationException();
            }
        }

        public bool IsRouter { get; private set; } = false;

        public bool IsServer { get; private set; } = false;

        public LanHostControl()
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

        public void SetIPAddress(IPAddress address)
        {
            ipAddressLabel.Text = AddressFormat.ToString(address);
        }
    }
}
