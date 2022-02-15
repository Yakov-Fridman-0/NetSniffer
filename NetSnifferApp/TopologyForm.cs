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
using NetSnifferLib.Analysis;

namespace NetSnifferApp
{
    public partial class TopologyForm : Form
    {
        public TopologyForm()
        {
            InitializeComponent();
            InitializeNodes();

            InitializeContextMenuStrips();
        }

        readonly LanMap lanMap = LanMap.Empty;
        readonly WanMap wanMap = WanMap.Empty;

        public event EventHandler TopologyUpdateRequired;

        readonly TreeNode lanHostsNode = new("Hosts") { Name = "lanHostsNode" };
        readonly TreeNode lanRoutersNode = new("Routers") { Name = "lanRoutersNode" };
        readonly TreeNode lanDhcpServersNode = new("DHCP Servers") { Name = "lanDhcpServersNode" };

        readonly TreeNode wanHostsNode = new("Hosts") { Name = "wanHostsNode" };
        readonly TreeNode wanLanRoutersNode = new("LAN Routers") { Name = "wanLanRoutersNode" };
        readonly TreeNode wanWanRoutersNode = new("WAN Routers") { Name = "wanWanRoutersNode" };
        readonly TreeNode wanDnsServersNode = new("DNS Servers") { Name = "wanHostsNode" };

        //readonly ContextMenuStrip lanHostWithIPAddressCMenu = new();

        readonly ContextMenuStrip wanHostContextMenuStrip = new();

        readonly ToolStripItem pingTSItem = new ToolStripMenuItem("ping");
        readonly ToolStripItem tracertTSItem = new ToolStripMenuItem("tracert");
        readonly ToolStripItem connectionsTSItem = new ToolStripMenuItem("View connections");

        List<ConnectionsForm> connectionForms = new();

        private void pingTSItem_Clicked(object sender, EventArgs e)
        {
            var treeNode = wanTreeView.SelectedNode;
            var addr = ((WanHost)treeNode.Tag).IPAddress;

            PacketAnalyzer.Ping(addr);
        }

        private void tracertTSItem_Click(object sender, EventArgs e)
        {
            var treeNode = wanTreeView.SelectedNode;
            var addr = ((WanHost)treeNode.Tag).IPAddress;

            Task.Run(() => PacketAnalyzer.Tracert(addr));
        }

        private void ConnectionsTSItem_Click(object sender, EventArgs e)
        {
            var host = (WanHost)wanTreeView.SelectedNode.Tag; 
            var form = ConnectionsForm.CreateConnectionsForm(host.IPAddress, host.ConnectedHosts);
            connectionForms.Add(form);
            form.FormClosed += Form_FormClosed;
            form.Show();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            var addr = ((ConnectionsForm)sender).IPAddress;
            connectionForms.Remove(connectionForms.Find((form) => form.IPAddress.Equals(addr)));
        }

        public void InitializeContextMenuStrips()
        {
            //lanHostWithIPAddressContextMenu.Items.Add(pingTStripItem);

            pingTSItem.Click += pingTSItem_Clicked;
            tracertTSItem.Click += tracertTSItem_Click;
            connectionsTSItem.Click += ConnectionsTSItem_Click;
            wanHostContextMenuStrip.Items.AddRange(new ToolStripItem[] { pingTSItem, tracertTSItem, connectionsTSItem});
        }


        public void UpdateLanMap(LanMap map)
        {
            var mapDiff = map.GetDiff(lanMap);

            if (mapDiff.IsEmpty)
                return;

            lanTreeView.BeginUpdate();

            // new hosts
            foreach (var addedHost in mapDiff.HostsAdded)
            {
                lanMap.Hosts.Add(addedHost);

                var newNode = new TreeNode(addedHost.ToString())
                {
                    Name = addedHost.PhysicalAddress.ToString(),
                    Tag = addedHost
                };

                lanHostsNode.Nodes.Add(newNode);
            }

            // removed hosts
            foreach (var removedHost in mapDiff.HostsRemoved)
            {
                lanMap.Hosts.Remove(removedHost);
                lanTreeView.Nodes[0].Nodes["lanHostsNode"].Nodes.RemoveByKey(removedHost.PhysicalAddress.ToString());
            }

            foreach (var modifiedAddressMapping in mapDiff.PhysicalAddressIPAddressMappingModified)
            {
                var physicalAddress = modifiedAddressMapping.PhysicalAddress;
                var ipAddress = modifiedAddressMapping.IPAddress;
                
                var host = lanMap.Hosts.Find((host) => host.PhysicalAddress.Equals(physicalAddress));
                host.IPAddress = ipAddress;

                var physicalAddressString = physicalAddress.ToString();
                lanHostsNode.Nodes[physicalAddressString].Text = host.ToString();

                var routerNode = lanRoutersNode.Nodes[physicalAddressString];
                if (routerNode != null)
                    routerNode.Text = host.ToString();

                var dhcpServerNode = lanDhcpServersNode.Nodes[physicalAddressString];

                if (dhcpServerNode != null)
                    dhcpServerNode.Text = host.ToString();
            }
            // modified hosts
            /*foreach (var modifiedHost in mapDiff.HostsModified)
            {
                var physicalAddr = modifiedHost.PhysicalAddress;
                var host = lanMap.Hosts.Find((host) => host.PhysicalAddress.Equals(physicalAddr));

                var changedNode = lanHostsNode.Nodes[modifiedHost.PhysicalAddress.ToString()];
                if (changedNode == null)
                {
                    MessageBox.Show($"Can't find host {modifiedHost.PhysicalAddress}", "Error");
                }
                else
                {
                    changedNode.Text = modifiedHost.ToString();
                }
            }*/

            // new routers
            foreach (var addedRouter in mapDiff.RoutersAdded)
            {
                var host = lanMap.Hosts.Find((aHost) => LanHost.PhysicalAddressComparer.Equals(aHost, addedRouter));
                lanMap.Routers.Add(host);

                var newNode = new TreeNode(addedRouter.ToString())
                {
                    Name = addedRouter.PhysicalAddress.ToString(),
                    Tag = host
                };

               lanRoutersNode.Nodes.Add(newNode);
            }

            // removed routers
            foreach (var removedRouter in mapDiff.RoutersRemoved)
            {
                lanMap.Routers.Remove(removedRouter);
                lanTreeView.Nodes[0].Nodes["lanRoutersNode"].Nodes.RemoveByKey(removedRouter.PhysicalAddress.ToString());
            }

            // new DHCP server
            foreach (var addedServer in mapDiff.DhcpServersAdded)
            {
                var host = lanMap.Hosts.Find((aHost) => LanHost.PhysicalAddressComparer.Equals(aHost, addedServer));
                lanMap.DhcpServers.Add(host);

                var newNode = new TreeNode(host.ToString())
                {
                    Name = addedServer.PhysicalAddress.ToString(),
                    Tag = host
                };

                lanDhcpServersNode.Nodes.Add(newNode);
            }

            // removed DHCP servers
            foreach (var removedServer in mapDiff.DhcpServersRemoved)
            {
                lanMap.DhcpServers.Remove(removedServer);
                lanTreeView.Nodes[0].Nodes["lanDhcpServerNode"].Nodes.RemoveByKey(removedServer.PhysicalAddress.ToString());
            }

            lanTreeView.EndUpdate();
        }

        public void UpdateWanMap(WanMap map)
        {
            var mapDiff = map.GetDiff(wanMap);

            if (mapDiff.IsEmpty)
                return;

            wanTreeView.BeginUpdate();

            //Hosts
            //  added
            foreach (var addedHost in mapDiff.HostsAdded)
            {
                wanMap.Hosts.Add(addedHost);
                wanHostsNode.Nodes.Add(new TreeNode(addedHost.IPAddress.ToString())
                {
                    Name = addedHost.IPAddress.ToString(),
                    Tag = addedHost,
                    
                    //ContextMenuStrip = wanHostContextMenuStrip
                });
            }
            //  removed
            foreach (var removedHost in mapDiff.HostRemoved)
            {
                wanMap.Hosts.Remove(removedHost);
                wanHostsNode.Nodes.RemoveByKey(removedHost.IPAddress.ToString());
            }

            //Connections
            //  added
            foreach (var addedConnection in mapDiff.ConnectionsAdded)
            {
                var addr1 = addedConnection.Address1;
                var addr2 = addedConnection.Address2;

                var host1 = wanMap.Hosts.Find((host) => host.IPAddress.Equals(addr1));
                var host2 = wanMap.Hosts.Find((host) => host.IPAddress.Equals(addr2));

                host1.ConnectedHosts.Add(host2);
                host2.ConnectedHosts.Add(host1);
            }

            //  removed
            foreach (var removedConnection in mapDiff.ConnectionsAdded)
            {
                var addr1 = removedConnection.Address1;
                var addr2 = removedConnection.Address2;

                var host1 = wanMap.Hosts.Find((host) => host.IPAddress.Equals(addr1));
                var host2 = wanMap.Hosts.Find((host) => host.IPAddress.Equals(addr2));

                host1.ConnectedHosts.Remove(host2);
                host2.ConnectedHosts.Remove(host1);
            }

            //LAN routers
            //  added
            foreach (var addedRouter in mapDiff.LanRouterAdded)
            {
                wanMap.LanRouters.Add(addedRouter);
                wanLanRoutersNode.Nodes.Add(new TreeNode(addedRouter.IPAddress.ToString())
                {
                    Name = addedRouter.IPAddress.ToString(),
                    Tag = addedRouter,

                    //ContextMenuStrip = wanHostContextMenuStrip
                });
            }

            //  removed
            foreach (var removedRouter in mapDiff.LanRouterRemoved)
            {
                wanMap.LanRouters.Remove(removedRouter);
                wanWanRoutersNode.Nodes.RemoveByKey(removedRouter.IPAddress.ToString());
            }

            //WAN routers
            //  added
            foreach (var addedRouter in mapDiff.WanRouterAdded)
            {
                wanMap.WanRouters.Add(addedRouter);
                wanWanRoutersNode.Nodes.Add(new TreeNode(addedRouter.IPAddress.ToString())
                {
                    Name = addedRouter.IPAddress.ToString(),
                    Tag = addedRouter,

                    //ContextMenuStrip = wanHostContextMenuStrip
                });
            }
            //  removed
            foreach (var removedRouter in mapDiff.WanRouterRemoved)
            {
                wanMap.WanRouters.Remove(removedRouter);
                wanWanRoutersNode.Nodes.RemoveByKey(removedRouter.IPAddress.ToString());
            }

            // DNS Servers
            //  added
            foreach (var addedServer in mapDiff.DnsServersAdded)
            {
                wanMap.DnsServers.Add(addedServer);
                wanDnsServersNode.Nodes.Add(new TreeNode(addedServer.IPAddress.ToString())
                {
                    Name = addedServer.IPAddress.ToString(),
                    Tag = addedServer,

                    //ContextMenuStrip = wanHostContextMenuStrip
                });
            }
            //  removed
            foreach (var removedServer in mapDiff.DnsServersRemoved)
            {
                wanMap.DnsServers.Remove(removedServer);
                wanDnsServersNode.Nodes.RemoveByKey(removedServer.IPAddress.ToString());
            }

            wanTreeView.EndUpdate();
        }

        private void InitializeNodes()
        {
            lanTreeView.BeginUpdate();
            lanTreeView.Nodes[0].Nodes.AddRange(new[] { lanHostsNode, lanRoutersNode, lanDhcpServersNode });
            lanTreeView.EndUpdate();

            wanTreeView.BeginUpdate();
            wanTreeView.Nodes[0].Nodes.AddRange(new[] { wanHostsNode, wanLanRoutersNode, wanWanRoutersNode, wanDnsServersNode });
            wanTreeView.EndUpdate();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => TopologyUpdateRequired?.Invoke(this, EventArgs.Empty));
        }

        private void TopologyForm_Load(object sender, EventArgs e)
        {
            refreshTimer.Start();
        }

        private void wanTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                wanTreeView.SelectedNode = wanTreeView.GetNodeAt(e.X, e.Y);
                
                if (wanTreeView.SelectedNode != null) 
                    wanHostContextMenuStrip.Show(wanTreeView, e.X, e.Y);
            }
        }
    }
}
