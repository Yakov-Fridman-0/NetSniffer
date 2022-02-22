using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IpData;

using NetSnifferLib.Topology;
using NetSnifferLib.Analysis;

namespace NetSnifferApp
{
    public partial class TopologyForm : Form
    {
        const string ipdataKey = "914223c5df7081e91e71dbbffb0fe200e8b3e0713817ebb13c0232c5";
        readonly IpDataClient client = new(ipdataKey);

        const int hostImageIndex = 0;
        static readonly Image hostImage = new Bitmap(@"E:\סייבר 2022\פרויקט\Sniffer\NetSniffer\NetSnifferApp\Resources\Host.png");
        const int routerImageIndex = 1;
        static readonly Image routerImage = new Bitmap(@"E:\סייבר 2022\פרויקט\Sniffer\NetSniffer\NetSnifferApp\Resources\Router.png");
        const int serverImageIndex = 2;
        static readonly Image serverImage = new Bitmap(@"E:\סייבר 2022\פרויקט\Sniffer\NetSniffer\NetSnifferApp\Resources\Server.png");
        const int routerAndServerImageIndex = 3;
        static readonly Image routerAndServerImage = new Bitmap(@"E:\סייבר 2022\פרויקט\Sniffer\NetSniffer\NetSnifferApp\Resources\RouterAndServer.png");
        const int wanImageIndex = 4;
        static readonly Image wanImage = new Bitmap(@"E:\סייבר 2022\פרויקט\Sniffer\NetSniffer\NetSnifferApp\Resources\Wan.png");
        const int lanImageIndex = 5;
        static readonly Image lanImage = new Bitmap(@"E:\סייבר 2022\פרויקט\Sniffer\NetSniffer\NetSnifferApp\Resources\Lan.png");
        readonly ImageList imageList = new();


        public TopologyForm()
        {
            InitializeComponent();
            InitializeNodes();

            InitializeContextMenuStrips();

            imageList.Images.Add(hostImage);
            imageList.Images.Add(routerImage);
            imageList.Images.Add(serverImage);
            imageList.Images.Add(routerAndServerImage);
            imageList.Images.Add(wanImage);
            imageList.Images.Add(lanImage);

            lanTreeView.ImageList = imageList;
            wanTreeView.ImageList = imageList;

            lanTreeView.ImageIndex = lanImageIndex;
            lanTreeView.SelectedImageIndex = lanImageIndex;

            lanHostsNode.ImageIndex = hostImageIndex;
            lanHostsNode.SelectedImageIndex = hostImageIndex;

            lanRoutersNode.ImageIndex = routerImageIndex;
            lanRoutersNode.SelectedImageIndex = routerImageIndex;

            lanDhcpServersNode.ImageIndex = serverImageIndex;
            lanDhcpServersNode.SelectedImageIndex = serverImageIndex;


            wanTreeView.ImageIndex = wanImageIndex;
            wanTreeView.SelectedImageIndex = wanImageIndex;

            wanHostsNode.ImageIndex = hostImageIndex;
            wanHostsNode.SelectedImageIndex = hostImageIndex;

            wanLanRoutersNode.ImageIndex = routerImageIndex;
            wanLanRoutersNode.SelectedImageIndex = routerImageIndex;

            wanWanRoutersNode.ImageIndex = routerImageIndex;
            wanWanRoutersNode.SelectedImageIndex = routerImageIndex;

            wanDnsServersNode.ImageIndex = serverImageIndex;
            wanDnsServersNode.SelectedImageIndex = serverImageIndex; 
        }

        const double THRESHOLD = 0.90;

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


        readonly ContextMenuStrip wanHostContextMenuStrip = new();

        readonly ToolStripItem pingTSItem = new ToolStripMenuItem("ping");
        readonly ToolStripItem tracertTSItem = new ToolStripMenuItem("tracert");
        readonly ToolStripItem connectionsTSItem = new ToolStripMenuItem("View connections");

        readonly List<ConnectionsForm> connectionForms = new();

        readonly object subnetmaskLock = new();
        
        async private Task AddCountryCodeToWanTreeNode(TreeNode node)
        {
            IPAddress addr = ((WanHost)node.Tag).IPAddress;
            var countryCode = await client.Lookup(addr.ToString(), info => info.CountryCode);
            
            if (!string.IsNullOrEmpty(countryCode) && countryCode != "null")
                wanTreeView.Invoke(new Action(() => node.Text += $" ({countryCode})"));
        }

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
            pingTSItem.Click += pingTSItem_Clicked;
            tracertTSItem.Click += tracertTSItem_Click;
            connectionsTSItem.Click += ConnectionsTSItem_Click;
            wanHostContextMenuStrip.Items.AddRange(new ToolStripItem[] { pingTSItem, tracertTSItem, connectionsTSItem});
        }

        /*private int CaculateSubnetBits(IPAddress[] addresses)
        {
            byte[] addrBytes;
            byte[] subnetnetBytes = IPAddress.Parse("255.255.255.255").GetAddressBytes();
            byte[] netmaskBytes = IPAddress.Parse("0.0.0.0").GetAddressBytes();

            int bits = 32;

            foreach (var addr in addresses)
            {
                addrBytes = addr.GetAddressBytes();
                for (int i = 0; i < 4; i++)
                {
                    subnetnetBytes[i] = (byte)(subnetnetBytes[i] & addrBytes[i]);
                }

                int j;
                for (j = 0; j < 32; j++)
                {
                    byte subnetInPlaceJ = (byte)(subnetnetBytes[j / 8] & (byte)Math.Pow(2, 7 - j % 8));
                    byte addrInPlaceJ = (byte)(addrBytes[j / 8] & (byte)Math.Pow(2, 7 - j % 8));

                    if (subnetInPlaceJ != addrInPlaceJ)
                    {
                        bits = j - 1;
                        break;
                    }
                }
            }

            return bits;
        }*/

        IPAddress GetSubnet(IPAddress address, int bits)
        {
            byte[] subnetBytes = new byte[4];
            byte[] addrBytes = address.GetAddressBytes();

            for (int i = 0; i < bits; i++) 
            {
                subnetBytes[i / 8] += (byte)(addrBytes[i/8] & (byte)Math.Pow(2, 7 - i%8)); 
            }

            return new IPAddress(subnetBytes);
        }

        void UpdateSubnetMask()
        {
            lock(subnetmaskLock)
            {
                var addrs = lanMap.Hosts.Where(host => host.IPAddress != null).Select((host) => host.IPAddress).Where((addr) => addr != null).ToList();
                int addrsCount = addrs.Count;
                var specialAddrs = new List<IPAddress>();

                byte[] netmaskBytes = new byte[4];

                int bits;
                List<IPAddress> otherAddressess = new();

                for (bits = 31; bits >= 1; bits--)
                {
                    var subnets = addrs.Select(addr => new { Subnet = GetSubnet(addr, bits), Address = addr }).GroupBy(sub => sub.Subnet).ToList();
                    subnets.Sort((g1, g2) => g1.Count() - g2.Count());

                    var mostPrelevant = subnets[0];


                    if (mostPrelevant.Count() >= THRESHOLD * addrsCount)
                    {
                        otherAddressess = subnets.Skip(1).SelectMany(g => g.Select(group => group.Address)).ToList();
                        break;
                    }
                }

                for (int i = 0; i < bits; i++)
                {
                    netmaskBytes[i / 8] += (byte)Math.Pow(2, 7 - i % 8);
                }

                IPAddress mask = new(netmaskBytes);
                lanTreeView.Invoke(new Action(() => subnetMaskLabel.Text = mask.ToString()));


                TreeNode[] nodes = new TreeNode[lanHostsNode.Nodes.Count];
                lanHostsNode.Nodes.CopyTo(nodes, 0);

                var nodesToRed = nodes.Where(node => otherAddressess.Contains(((LanHost)(node.Tag)).IPAddress));

                void ColorNode(TreeNode node)
                {
                    node.ForeColor = System.Drawing.Color.Red;
                }

                foreach (var node in nodesToRed)
                    lanTreeView.Invoke(new Action(() => ColorNode(node)));
            }

            //byte[] addrBytes, netmaskBytes, subnetnetBytes;
            //subnetnetBytes = IPAddress.Parse("255.255.255.255").GetAddressBytes();
            //netmaskBytes = IPAddress.Parse("0.0.0.0").GetAddressBytes();
            //int bits = 32;

            //var addrs = lanMap.Hosts.Select((host) => host.IPAddress).Where((addr) => addr != null);

            //foreach (var addr in addrs)
            //{
            //    addrBytes = addr.GetAddressBytes();
            //    for (int i = 0; i < 4; i++)
            //    {
            //        subnetnetBytes[i] = (byte)(subnetnetBytes[i] & addrBytes[i]);
            //    }

            //    int j;
            //    for (j = 0; j < 32; j++)
            //    {
            //        byte subnetInPlaceJ = (byte)(subnetnetBytes[j / 8] & (byte)Math.Pow(2, 7 - j % 8));
            //        byte addrInPlaceJ = (byte)(addrBytes[j / 8] & (byte)Math.Pow(2, 7 - j % 8));

            //        if (subnetInPlaceJ != addrInPlaceJ)
            //        {
            //            bits = j - 1;
            //            break;
            //        }
            //    }

            //}

            //for (int j = 0; j < bits; j++)
            //{
            //    netmaskBytes[j / 8] += (byte)Math.Pow(2, 7 - j % 8);
            //}

            //subnetMaskLabel.Text = new IPAddress(netmaskBytes).ToString();
        }

        Task UpdateSubnetMaskAsync()
        {
            return Task.Run(() => UpdateSubnetMask());
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

                UpdateSubnetMaskAsync();

                var newNode = new TreeNode(addedHost.ToString())
                {
                    Name = addedHost.PhysicalAddress.ToString(),
                    Tag = addedHost,
                    ImageIndex = hostImageIndex,
                    SelectedImageIndex = hostImageIndex
                };

                lanHostsNode.Nodes.Add(newNode);
            }

            // removed hosts
            foreach (var removedHost in mapDiff.HostsRemoved)
            {
                lanMap.Hosts.Remove(removedHost);
                lanTreeView.Nodes[0].Nodes["lanHostsNode"].Nodes.RemoveByKey(removedHost.PhysicalAddress.ToString());

                if (removedHost.IPAddress != null)
                    UpdateSubnetMaskAsync();
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

                UpdateSubnetMaskAsync();
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

                var hostNode = lanHostsNode.Nodes[addedRouter.PhysicalAddress.ToString()];
                var dhcpServerNode = lanDhcpServersNode.Nodes[addedRouter.PhysicalAddress.ToString()];

                int imageIndex = routerImageIndex, selectedImagedIndex = routerImageIndex;

                if (dhcpServerNode != null)
                {
                    imageIndex = routerAndServerImageIndex;
                    selectedImagedIndex = routerAndServerImageIndex;

                    dhcpServerNode.ImageIndex = imageIndex;
                    dhcpServerNode.SelectedImageIndex = selectedImagedIndex;
                }

                var newNode = new TreeNode(addedRouter.ToString())
                {
                    Name = addedRouter.PhysicalAddress.ToString(),
                    Tag = host,
                    ImageIndex = imageIndex,
                    SelectedImageIndex = selectedImagedIndex
                };

                hostNode.ImageIndex = imageIndex;
                hostNode.SelectedImageIndex = selectedImagedIndex;

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

                var hostNode = lanHostsNode.Nodes[addedServer.PhysicalAddress.ToString()];
                var routerNode = lanRoutersNode.Nodes[addedServer.PhysicalAddress.ToString()];

                int imageIndex = serverImageIndex, selectedImagedIndex = serverImageIndex;

                if (routerNode != null)
                {
                    imageIndex = routerAndServerImageIndex;
                    selectedImagedIndex = routerAndServerImageIndex;

                    routerNode.ImageIndex = imageIndex;
                    routerNode.SelectedImageIndex = selectedImagedIndex;
                }

                var newNode = new TreeNode(host.ToString())
                {
                    Name = addedServer.PhysicalAddress.ToString(),
                    Tag = host,
                    ImageIndex = imageIndex,
                    SelectedImageIndex = selectedImagedIndex
                };

                hostNode.ImageIndex = imageIndex;
                hostNode.SelectedImageIndex = selectedImagedIndex;

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

            // Hosts added
            foreach (var addedHost in mapDiff.HostsAdded)
            {
                wanMap.Hosts.Add(addedHost);

                var newNode = new TreeNode(addedHost.IPAddress.ToString())
                {
                    Name = addedHost.IPAddress.ToString(),
                    Tag = addedHost,
                    ImageIndex = hostImageIndex,
                    SelectedImageIndex = hostImageIndex
                };
                wanHostsNode.Nodes.Add(newNode);

                Task.Run(() => AddCountryCodeToWanTreeNode(newNode));
            }
            // Hosts  removed
            foreach (var removedHost in mapDiff.HostRemoved)
            {
                wanMap.Hosts.Remove(removedHost);
                wanHostsNode.Nodes.RemoveByKey(removedHost.IPAddress.ToString());
            }

            // Connections added
            foreach (var addedConnection in mapDiff.ConnectionsAdded)
            {
                var addr1 = addedConnection.Address1;
                var addr2 = addedConnection.Address2;

                var host1 = wanMap.Hosts.Find((host) => host.IPAddress.Equals(addr1));
                var host2 = wanMap.Hosts.Find((host) => host.IPAddress.Equals(addr2));

                host1.ConnectedHosts.Add(host2);
                host2.ConnectedHosts.Add(host1);
            }

            // Connections removed
            foreach (var removedConnection in mapDiff.ConnectionsAdded)
            {
                var addr1 = removedConnection.Address1;
                var addr2 = removedConnection.Address2;

                var host1 = wanMap.Hosts.Find((host) => host.IPAddress.Equals(addr1));
                var host2 = wanMap.Hosts.Find((host) => host.IPAddress.Equals(addr2));

                host1.ConnectedHosts.Remove(host2);
                host2.ConnectedHosts.Remove(host1);
            }

            // Connections added
            foreach (var addedRouter in mapDiff.LanRouterAdded)
            {
                wanMap.LanRouters.Add(addedRouter);

                var hostNode = wanHostsNode.Nodes[addedRouter.IPAddress.ToString()];
                var dnsServerNode = wanDnsServersNode.Nodes[addedRouter.IPAddress.ToString()];

                int imageIndex = routerImageIndex, selectedImageIndex = routerImageIndex;

                if (dnsServerNode != null)
                {
                    imageIndex = routerAndServerImageIndex;
                    selectedImageIndex = routerAndServerImageIndex;

                    dnsServerNode.ImageIndex = imageIndex;
                    dnsServerNode.SelectedImageIndex = imageIndex;
                }

                var newNode = new TreeNode(addedRouter.IPAddress.ToString())
                {
                    Name = addedRouter.IPAddress.ToString(),
                    Tag = addedRouter,
                    ImageIndex = imageIndex,
                    SelectedImageIndex = selectedImageIndex
                };

                wanLanRoutersNode.Nodes.Add(newNode);

                hostNode.ImageIndex = imageIndex;
                hostNode.SelectedImageIndex = imageIndex;


                Task.Run(() => AddCountryCodeToWanTreeNode(newNode));
            }

            // LAN  routers  removed
            foreach (var removedRouter in mapDiff.LanRouterRemoved)
            {
                wanMap.LanRouters.Remove(removedRouter);
                wanWanRoutersNode.Nodes.RemoveByKey(removedRouter.IPAddress.ToString());
            }

            // WAN routers added
            foreach (var addedRouter in mapDiff.WanRouterAdded)
            {
                wanMap.WanRouters.Add(addedRouter);

                var hostNode = wanHostsNode.Nodes[addedRouter.IPAddress.ToString()];
                var dnsServerNode = wanDnsServersNode.Nodes[addedRouter.IPAddress.ToString()];

                int imageIndex = routerImageIndex, selectedImageIndex = routerImageIndex;

                if (dnsServerNode != null)
                {
                    imageIndex = routerAndServerImageIndex;
                    selectedImageIndex = routerAndServerImageIndex;

                    dnsServerNode.ImageIndex = imageIndex;
                    dnsServerNode.SelectedImageIndex = imageIndex;
                }

                var newNode = new TreeNode(addedRouter.IPAddress.ToString())
                {
                    Name = addedRouter.IPAddress.ToString(),
                    Tag = addedRouter,
                    ImageIndex = imageIndex,
                    SelectedImageIndex = selectedImageIndex
                };

                wanWanRoutersNode.Nodes.Add(newNode);

                hostNode.ImageIndex = imageIndex;
                hostNode.SelectedImageIndex = imageIndex;

                Task.Run(() => AddCountryCodeToWanTreeNode(newNode));
            }
            // WAN routers  removed
            foreach (var removedRouter in mapDiff.WanRouterRemoved)
            {
                wanMap.WanRouters.Remove(removedRouter);
                wanWanRoutersNode.Nodes.RemoveByKey(removedRouter.IPAddress.ToString());
            }

            // DNS Servers  added
            foreach (var addedServer in mapDiff.DnsServersAdded)
            {
                wanMap.DnsServers.Add(addedServer);

                var hostNode = wanHostsNode.Nodes[addedServer.IPAddress.ToString()];
                var lanRouterNode = wanLanRoutersNode.Nodes[addedServer.IPAddress.ToString()];
                var wanRouterNode = wanWanRoutersNode.Nodes[addedServer.IPAddress.ToString()];

                int imageIndex = serverImageIndex, selectedImageIndex = serverImageIndex;

                if (lanRouterNode != null || wanRouterNode != null)
                {
                    imageIndex = routerAndServerImageIndex;
                    selectedImageIndex = routerAndServerImageIndex;

                    if (lanRouterNode != null)
                    {
                        lanRouterNode.ImageIndex = imageIndex;
                        lanRouterNode.SelectedImageIndex = imageIndex;
                    }

                    if (wanRouterNode != null)
                    {
                        wanRouterNode.ImageIndex = imageIndex;
                        wanRouterNode.SelectedImageIndex = imageIndex;
                    }
                }

                var newNode = new TreeNode(addedServer.IPAddress.ToString())
                {
                    Name = addedServer.IPAddress.ToString(),
                    Tag = addedServer,
                    ImageIndex = imageIndex,
                    SelectedImageIndex = selectedImageIndex
                };

                wanDnsServersNode.Nodes.Add(newNode);

                hostNode.ImageIndex = imageIndex;
                hostNode.SelectedImageIndex = imageIndex;

                Task.Run(() => AddCountryCodeToWanTreeNode(newNode));
            }
            // DNS Servers  removed
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
