using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using NetSnifferLib;
using NetSnifferLib.StatefulAnalysis;

using PcapDotNet.Packets;
using PcapDotNet.Packets.Dns;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;

namespace NetSnifferApp
{
    public partial class PacketViewer : UserControl
    {
        class ListViewItemComparer : IComparer, IComparer<ListViewItem>
        {
            readonly IComparer<PacketData> packetDataCompater = PacketData.IdComparer;

            public int Compare(object x, object y)
            {
                if (x is ListViewItem dataX && y is ListViewItem dataY)
                {
                    return packetDataCompater.Compare((PacketData)dataX.Tag, (PacketData)dataY.Tag);
                }

                throw new ArgumentException("Input must be of type PacketData");
            }

            public int Compare(ListViewItem x, ListViewItem y)
            {
                return packetDataCompater.Compare((PacketData)x.Tag, (PacketData)y.Tag);
            }
        }

        //System.Threading.Timer timer;
        readonly SortedDictionary<int, ListViewItem> allListViewItems = new();

        string displayFilterString = string.Empty;
        DisplayFilter displayFilter = DisplayFilter.EmptyFilter;

        //readonly ActionBlock<PacketData> itemCreatorActionBlock;

        public PacketViewer()
        {
            InitializeComponent();

            #region DoubleBuffered
            bool doubleBuffered = true;

            typeof(ListView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(this.packetsListView, doubleBuffered, null);
            #endregion

            packetsListView.VirtualMode = false;
            //itemCreatorActionBlock = new ActionBlock<PacketData>(packetData => AddPacketDataCore(packetData));

            packetsListView.ListViewItemSorter = new ListViewItemComparer();
            packetsListView.Sorting = SortOrder.Ascending;

           
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            timer1.Start();
            //timer = new(AddPendingPackets, null, 0, 500);
        }

        public IEnumerable<Packet> GetSelectedPackets()
        {
            var packets = new List<Packet>();

            foreach (var item in packetsListView.Items)
                packets.Add(((ListViewItem)item).Tag as Packet);
            return packets;
        }

        private void AddPacketDataCore(PacketData packetData)
        {
            ListViewItem item;

            packetData.AttackDetected += MarkAttack;
            item = CreateItemWithoutAnalysis(packetData);

            //Invoke(new MethodInvoker(delegate
            //{
            //    packetsListView.Items.Add(item);
            //}));

            lock(allListViewItems)
            {
                if (!allListViewItems.ContainsKey(packetData.PacketId))
                    allListViewItems.Add(packetData.PacketId, item);
            }
                

            if (packetData.Description == null)
                packetData.DescriptionReady += FillPacketDescription;
            else
                FillPacketDescription(packetData.PacketId, packetData.Description);
        }

        bool cleared = false;

        public void Clear()
        {
            lock (allListViewItems)
            {
                allListViewItems.Clear();
            }

            cleared = true;

            if (packetsListView.InvokeRequired)
            {
                packetsListView.Invoke(new MethodInvoker(
                delegate
                {
                    packetsListView.Items.Clear();
                }));
            }
            else
            {
                packetsListView.Items.Clear();
            }

            displayFilterString = string.Empty;
            displayFilter = DisplayFilter.EmptyFilter;

            displayFilterControl.Filter = string.Empty;
            displayFilterControl.IsValidFilter = true;
        }

        public void AddPacket(PacketData packetData)
        {
            cleared = false;

            AddPacketDataCore(packetData);
            //itemCreatorActionBlock.Post(packetData);
        }

        ListViewItem CreateItemWithoutAnalysis(PacketData packetData)
        {
            var subItems = new string[7] { packetData.PacketId.ToString(), "", "", "", "", "", "" };

            ListViewItem item = null;

            Invoke(new MethodInvoker(
            delegate
            {
                item = new ListViewItem(subItems)
                {
                    //Text = packetData.PacketId.ToString(),
                    Tag = packetData
                };
            }));

            return item;
        }

        readonly object fillPacketSync = new();

        public bool IsFillingPacket = false;

        void FillPacketDescription(int packetId, PacketDescription description)
        {
            if (cleared)
                return;

            var packetData = PacketData.GetPacketDataByPacketId(packetId);

            ListViewItem item;

            lock (allListViewItems)
                item = allListViewItems[packetId];

            bool matches;

            bool hasAttacks = packetData.Attacks.Length != 0;

            matches = displayFilter.PacketMatches(packetData);

            if (hasAttacks)
            {
                packetsListView.BeginInvoke(new MethodInvoker(
                delegate
                {
                    //lock (fillPacketSync)
                        MarkItemAsAttack(item);
                }
                ));

            }
            else
            {
                GetPacketColors(packetData, out Color foreColor, out Color backColor);

                packetsListView.BeginInvoke(new MethodInvoker(
                delegate
                {
                    //lock (fillPacketSync)
                        SetItemColors(item, foreColor, backColor);
                }
                ));
            }

            packetsListView.BeginInvoke(new MethodInvoker(() =>
            {
                //lock (fillPacketSync)
                    UpdateItem(item, description);
            }));

            if (matches)
            {
                packetsListView.BeginInvoke(new MethodInvoker(() =>
                {
                    //lock (updateSync)
                    //{
                    if (item.ListView == null)
                        pendingListViewItems.Add(item);
                            //packetsListView.Items.Add(item);
                    //}
                }));
            }
        }

        readonly HashSet<ListViewItem> pendingListViewItems = new();

        static void SetItemColors(ListViewItem item, Color foreColor, Color backColor)
        {
            item.ForeColor = foreColor;
            item.BackColor = backColor;
        }

        static void UpdateItem(ListViewItem item, PacketDescription description)
        {
            var subItems = item.SubItems;

            subItems[1].Text = description.TimeStamp.ToString();
            subItems[2].Text = description.Protocol;
            subItems[3].Text = AddressFormat.ToString(description.Source);
            subItems[4].Text = AddressFormat.ToString(description.Destination);
            subItems[5].Text = description.Length.ToString();
            subItems[6].Text = description.Info;
        }

        static void GetPacketColors(PacketData packetData, out Color foreColor, out Color backColor)
        {
            var protocols = packetData.Protocols;

            if (protocols.Length == 0)
            {
                foreColor = Color.White;
                backColor = Color.Gray;

                return;
            }

            var lastProtocol = protocols[^1];

            switch (lastProtocol)
            {
                case "eth":
                    foreColor = Color.Black;
                    backColor = Color.White;
                    break;

                case "arp":
                    foreColor = Color.Black;

                    if (((ArpDatagram)packetData["arp"]).Operation == ArpOperation.Reply)
                        backColor = Color.Goldenrod;
                    else
                        backColor = Color.Gold;
                    break;

                case "ip":
                    foreColor = Color.Black;
                    backColor = Color.OliveDrab;
                    break;

                case "udp":
                    foreColor = Color.White;
                    backColor = Color.DarkViolet;
                    break;

                case "tcp":
                    foreColor = Color.White;
                    backColor = Color.ForestGreen;
                    break;

                case "dns":
                    foreColor = Color.White;

                    if (((DnsDatagram)packetData["dns"]).IsResponse)
                        backColor = Color.FromArgb(0x663399); // Rebecca purple
                    else
                        backColor = Color.FromArgb(0x6a5acd);
                    break;

                default:
                    foreColor = Color.Black;
                    backColor = Color.White;
                    break;
            }
        }

        static void GetPacketColors(Packet packet, out Color foreColor, out Color backColor)
        {
            if (packet.DataLink.Kind != DataLinkKind.Ethernet)
            {
                foreColor = Color.White;
                backColor = Color.Gray;

                return;
            }

            
            if (packet.Ethernet.EtherType == EthernetType.Arp)
            {
                foreColor = Color.Black;

                if (packet.Ethernet.Arp.Operation == ArpOperation.Request)
                    backColor = Color.Gold;
                else
                    backColor = Color.Goldenrod;
            }
            else if (packet.Ethernet.EtherType == EthernetType.IpV4)
            {
                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp)
                {
                    foreColor = Color.White;
                    backColor = Color.DarkViolet;
                }
                else if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp)
                {
                    foreColor = Color.White;
                    backColor = Color.ForestGreen;
                }
                else
                {
                    foreColor = Color.Black;
                    backColor = Color.Olive;
                }
            }
            else if (packet.Ethernet.EtherType == EthernetType.IpV6)
            {
                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp)
                {
                    foreColor = Color.White;
                    backColor = Color.DarkViolet;
                }
                else if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp)
                {
                    foreColor = Color.White;
                    backColor = Color.ForestGreen;
                }
                else
                {
                    foreColor = Color.Black;
                    backColor = Color.OliveDrab;
                }
            }
            else
            {
                foreColor = Color.Black;
                backColor = Color.White;
            }
        }

        void MarkAttack(int packetId, Attack attack)
        {
            AttackAdded.Invoke(this, new EventArgs());

            ListViewItem item;

            lock (allListViewItems)
                item = allListViewItems[packetId];

           //if (packetsListView.InvokeRequired)
           //{
                packetsListView.Invoke(new MethodInvoker(() =>
                {
                    MarkItemAsAttack(item);
                }));
            //}
            //else
            //{
            //    MarkItemAsAttack(item);
            //}
        }

        static void MarkItemAsAttack(ListViewItem item)
        {
            SetItemColors(item, Color.White, Color.Red);
        }

        private void PacketsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numberOfSelectedPackets = packetsListView.SelectedIndices.Count;

            if (numberOfSelectedPackets == 1)
            {
                var index = packetsListView.SelectedIndices[0];

                var packetData = (PacketData)packetsListView.Items[index].Tag;

                byte[] buffer = packetData.Packet.Buffer;

                FillBinaryDataTextBox(Convert.ToHexString(buffer));
                Attack[] attacks = packetData.Attacks;

                attacksComboBox.Items.AddRange(attacks);
            }
            else if (numberOfSelectedPackets == 0)
            {
                binaryDataTextBox.Text = string.Empty;

                attacksComboBox.Items.Clear();
                attackTextBox.Text = string.Empty;
            }
        }

        void FillBinaryDataTextBox(string binaryStirng)
        {
            var width = binaryDataTextBox.Width;

            var font = binaryDataTextBox.Font;

            var charsInGroup = 4;
            var widthsOfGroup = TextRenderer.MeasureText(new string('F', charsInGroup + 1), font).Width;

            int numberOfGroupsPerLine = width / widthsOfGroup;

            int i = 0;

            foreach (var group in Enumerable.Range(0, binaryStirng.Length / charsInGroup).Select(i => binaryStirng.Substring(i * charsInGroup, charsInGroup)))
            {
                if (i == numberOfGroupsPerLine)
                {
                    binaryDataTextBox.Text += group;
                    binaryDataTextBox.Text += "\r\n";
                    i = 0;
                }
                else
                {
                    binaryDataTextBox.Text += group;
                    binaryDataTextBox.Text += " ";
                    i++;
                }
            }
        }

        private void AttacksComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (attacksComboBox.SelectedItem != null)
            {
                var attack = (Attack)attacksComboBox.SelectedItem;

                var strBuilder = new StringBuilder();
                strBuilder.AppendLine($"Packets: {string.Join<string>(", ", attack.PacketIds.Select(id => id.ToString()))}");
                strBuilder.AppendLine($"Attackers: {string.Join<string>(", ", attack.Attackers.Select(address => AddressFormat.ToString(address)))}");
                strBuilder.AppendLine($"Targets: {string.Join<string>(", ", attack.Targets.Select(address => AddressFormat.ToString(address)))}");
                attackTextBox.Text = strBuilder.ToString();
            }
            else
            {
                attackTextBox.Text = string.Empty;
            }
        }

        readonly object updateSync = new();

        public event EventHandler AttackAdded = delegate { };

        void UpdateVisiblePackets()
        {
            ListViewItem[] itemsToShow;

            lock (allListViewItems)
                itemsToShow = allListViewItems.Values.Where(item => displayFilter.PacketMatches((PacketData)item.Tag)).ToArray();

            pendingListViewItems.Clear();

            lock (updateSync)
            {
                packetsListView.BeginUpdate();
                packetsListView.Items.Clear();
                packetsListView.EndUpdate();

                //packetsListView.Items.AddRange(itemsToShow);

                foreach (var item in itemsToShow)
                    pendingListViewItems.Add(item);
            }
        }

        private void DisplayFilter_FilterChanged(object sender, string e)
        {
            displayFilterString = e;

            if (DisplayFilter.TryParse(displayFilterString, ref displayFilter))
            {
                displayFilterControl.IsValidFilter = true;
                UpdateVisiblePackets();
            }
            else
            {
                displayFilterControl.IsValidFilter = false;
            }
        }

        private void BinaryDataTextBox_Resize(object sender, EventArgs e)
        {
            FillBinaryDataTextBox(binaryDataTextBox.Text);
        }

        private void PacketsListView_Resize(object sender, EventArgs e)
        {
            info.Width = packetsListView.Width - packetsListView.Columns.Cast<ColumnHeader>().Aggregate(0, (sum, header) => sum + header.Width) + info.Width;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (updateSync)
            {
                var notShown = pendingListViewItems.Except(packetsListView.Items.Cast<ListViewItem>());
                packetsListView.Items.AddRange(notShown.ToArray());

                pendingListViewItems.Clear();
            }
        }
    }
}
