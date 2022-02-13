using System;
using System.Net;
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
    public partial class ConnectionsForm : Form
    {
        public IPAddress IPAddress { get; private set; }

        public ConnectionsForm()
        {
            InitializeComponent();
        }

        public EventHandler UpdateNeeded;

        private void SetHostAddress(IPAddress address)
        {
            IPAddress = address;
            hostAddressLabel.Text = $"{address} is connected to:";
        }

        private void SetConnections(List<WanHost> connections)
        {
            connectionsListBox.Items.AddRange(connections.ToArray());
        }

        public void UpdateConnections(List<WanHost> connections)
        {
            var oldConnections = new object[connectionsListBox.Items.Count];

            connectionsListBox.Items.CopyTo(oldConnections, 0);

            connectionsListBox.Items.AddRange(oldConnections.Select((conn) => (WanHost)conn).Except(connections, new WanHost.SameIPAddress()).ToArray());
        }

        public static ConnectionsForm CreateConnectionsForm(IPAddress hostAddress, List<WanHost> connections)
        {
            var form = new ConnectionsForm();

            form.SetHostAddress(hostAddress);
            form.SetConnections(connections);

            return form;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateNeeded?.Invoke(this, EventArgs.Empty);
        }

        private void ConnectionsForm_Load(object sender, EventArgs e)
        {
            updateTimer.Start();
        }

        private void ConnectionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateTimer.Stop();
        }
    }
}
