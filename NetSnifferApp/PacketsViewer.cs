using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

using NetSnifferLib;
using NetSnifferLib.StatefulAnalysis;

using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;

namespace NetSnifferApp
{
    public partial class PacketViewer : UserControl
    {
        readonly SortedDictionary<PacketData, ListViewItem> allPacketsData = new(PacketData.IdComparer);
        
        readonly ActionBlock<Packet> itemBuilder;

        string displayFilterString = string.Empty;
        DisplayFilter displayFilter = DisplayFilter.EmptyFilter;

        public PacketViewer()
        {
            InitializeComponent();

            #region DoubleBuffered
            bool doubleBuffered = true;

            typeof(ListView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(this.packetsListView, doubleBuffered, null);
            #endregion

            itemBuilder = new ActionBlock<Packet>(packet => AddItemCoreAsync(packet));

            #region Columns
/*            packetsListView.Columns.Clear();

            packetsListView.Columns.Add(new ColumnHeader() { Text = "Index", Width = -2 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Time", Width = 100 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Protocol", Width = -2 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Source", Width = 150 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Destination", Width = 150 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Length", Width = 150 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Info"});*/

            #endregion

            packetsListView.ListViewItemSorter = new ListViewItemComparer();
            packetsListView.Sorting = SortOrder.Ascending;
        }

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

        public IEnumerable<Packet> GetSelectedPackets()
        {
            var packets = new List<Packet>();

            foreach (var item in packetsListView.Items)
                packets.Add(((ListViewItem)item).Tag as Packet);
            return packets;
        }

        private void AddItemCore(Packet packet)
        {
            var packetData = CreatePacketData(packet);
            var item = CreateNewItem(packetData);
            
            lock (allPacketsData)
                allPacketsData.Add(packetData, item);

            //packetsListView.Invoke( 
            //    new MethodInvoker(() =>
            //    {
            //        packetsListView.Items.Add(item);
            //        packetsListView.Update();
            //    }));

            //if (packetsListView.Items.Count != 0)
            //    packetsListView.BeginInvoke(new MethodInvoker(() => packetsListView.Items[^1].EnsureVisible()));

            packetData.Analyze();
        }

        private Task AddItemCoreAsync(Packet packet)
        {
            return Task.Run(() => AddItemCore(packet));
        }

        public void Clear()
        {
            packetsListView.Items.Clear();
            packetsListView.Invalidate();

            lock (allPacketsData)
                allPacketsData.Clear();

            displayFilterString = string.Empty;
            displayFilter = DisplayFilter.EmptyFilter;

            displayFilterControl.Filter = string.Empty;
            displayFilterControl.IsValidFilter = true;
        }

        public void Add(Packet packet)
        {
            itemBuilder.Post(packet);
        }

        public void AddRange(IEnumerable<Packet> packets)
        {
            foreach (var packet in packets)
                itemBuilder.Post(packet);
        }

        PacketData CreatePacketData(Packet packet)
        {
            int id = IdManager.GetNewPacketId(packet);

            var packetData = new PacketData(id, packet);

            packetData.DescriptionReady += FillPacketDescription;
            packetData.AttackDetected += MarkAttack;

            return packetData;
        }

        static ListViewItem CreateNewItem(PacketData packetData)
        {
            var subItems = new string[7] { packetData.PacketId.ToString(), "", "", "", "", "", "" };

            var item = new ListViewItem(subItems)
            {
                Tag = packetData
            };

            return item;
        }


        void FillPacketDescription1(int pakcetId, PacketDescription description)
        {

        }

        async void FillPacketDescription(int pakcetId, PacketDescription description)
        {
            var packetData = PacketData.GetPacketDataByPacketId(pakcetId);

            ListViewItem item;
            lock (allPacketsData)
                item = allPacketsData[packetData];

            var subItems = item.SubItems;

            var matches = displayFilter.PacketMatches(packetData);

            Color foreColor = Color.White;

            packetsListView.Invoke(new MethodInvoker(() =>
            {
                item.BackColor = (packetData.Attacks.Length == 0) ? GetPacketColors(IdManager.GetPacket(pakcetId), out foreColor) : Color.Red;
                item.ForeColor = (packetData.Attacks.Length == 0) ? foreColor : Color.White;
                subItems[1].Text = description.TimeStamp.ToString();
                subItems[2].Text = description.Protocol;
                subItems[3].Text = AddressFormat.ToString(description.Source);
                subItems[4].Text = AddressFormat.ToString(description.Destination);
                subItems[5].Text = description.Length.ToString();
                subItems[6].Text = description.Info;
            }));

            IAsyncResult result = packetsListView.BeginInvoke((MethodInvoker)delegate
            {
                if (matches && item.ListView == null)
                {
                    //packetsListView.Items.Add(item);
                }
            });

            await Task.Factory.FromAsync(result, new Action<IAsyncResult>(asyncResult => { }));
        }

        static Color GetPacketColors(Packet packet, out Color foreColor)
        {
            foreColor = Color.Black;

            if (packet.DataLink.Kind != DataLinkKind.Ethernet)
                return Color.Gray;

            Color color = Color.White;
             
            if (packet.Ethernet.EtherType == EthernetType.Arp)
            {
                if (packet.Ethernet.Arp.Operation == ArpOperation.Request)
                    color = Color.Gold;
                else
                    color = Color.Goldenrod;
            }
            else if (packet.Ethernet.EtherType == EthernetType.IpV4)
            {
                color = Color.Olive;
                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp)
                {
                    color = Color.DarkViolet;
                    foreColor = Color.White;
                }
                else if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp)
                {
                    color = Color.ForestGreen;
                    foreColor = Color.White;
                }
            }
            else if (packet.Ethernet.EtherType == EthernetType.IpV6)
            {
                color = Color.OliveDrab;

                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Udp)
                {
                    color = Color.DarkViolet;
                    foreColor = Color.White;
                }
                else if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp)
                {
                    color = Color.ForestGreen;
                    foreColor = Color.White;
                }
            }
            
            return color;
        }

        void MarkAttack(int packetId, Attack attack)
        {
            packetsListView.Invoke(new MethodInvoker(() =>
            {
                ListViewItem item;
                lock (allPacketsData)
                    item = allPacketsData[PacketData.GetPacketDataByPacketId(packetId)];

                item.ImageIndex = 0;
                item.BackColor = Color.Red;
                item.ForeColor = Color.White;
                Update();
            }));
        }

        private void packetsListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (packetsListView.SelectedIndices.Count == 1)
            {
                var item = packetsListView.SelectedItems[0];

                var packetData = (PacketData)item.Tag;
                
                byte[] buffer = packetData.Packet.Buffer;

                FillBinaryDataTextBox(Convert.ToHexString(buffer));
                Attack[] attacks = packetData.Attacks;

                attacksComboBox.Items.AddRange(attacks);
            }
            else if (packetsListView.SelectedItems.Count == 0)
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

        private void attacksComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        Task UpdateVisiblePacketsAsync()
        {
            return Task.Run(() => UpdateVisiblePackets());
        }

        void UpdateVisiblePackets()
        {
            lock (allPacketsData)
            {
                int i = 0;

                packetsListView.Invoke(new MethodInvoker(() => packetsListView.BeginUpdate()));
                foreach (var (packetData, listViewItem) in allPacketsData)
                {
                    i++;

                    if (i == 50)
                    {
                        packetsListView.BeginInvoke((MethodInvoker)delegate
                            {
                                packetsListView.EndUpdate();
                                packetsListView.BeginUpdate();
                            });

                        i = 0;
                    }

                    if (displayFilter.PacketMatches(packetData))
                    {
                        if (listViewItem.ListView == null)
                        {
                            packetsListView.Invoke(new MethodInvoker(() => packetsListView.Items.Add(listViewItem)));
                        }
                    }
                    else
                    {
                        if (listViewItem.ListView != null)
                            packetsListView.Invoke(new MethodInvoker(() => packetsListView.Items.Remove(listViewItem)));
                    }
                }
                packetsListView.Invoke(new MethodInvoker(() => packetsListView.EndUpdate()));
            }
        }

        private async void displayFilter1_FilterChanged(object sender, string e)
        {
             displayFilterString = e;

            if (DisplayFilter.TryParse(displayFilterString, ref displayFilter))
            {
                displayFilterControl.IsValidFilter = true;
                await UpdateVisiblePacketsAsync();
            }
            else
            {
                displayFilterControl.IsValidFilter = false;
            }
        }

        private void binaryDataTextBox_Resize(object sender, EventArgs e)
        {
            FillBinaryDataTextBox(binaryDataTextBox.Text);
        }

        int prevWidth = 0;

        private void packetsListView_Resize(object sender, EventArgs e)
        {
            
            info.Width = packetsListView.Width - packetsListView.Columns.Cast<ColumnHeader>().Aggregate(0, (sum, header) => sum + header.Width) + info.Width;
        }
    }
}
