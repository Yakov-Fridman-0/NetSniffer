using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
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

        //List<PacketData> filteredPacketDatas = new();

        readonly SortedDictionary<int, ListViewItem> allListViewItems = new();
        // readonly SortedDictionary<int, ListViewItem> filteredListViewItems = new();
        //readonly ActionBlock<Packet> itemBuilder;

        string displayFilterString = string.Empty;
        DisplayFilter displayFilter = DisplayFilter.EmptyFilter;

        readonly object virtualListSizeLock = new();

        ListViewItem[] cache;

        //readonly object cacheLock = new();

        int firstItem;

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
            //itemBuilder = new ActionBlock<Packet>(packet => AddPacketCoreAsync(packet));

            packetsListView.ListViewItemSorter = new ListViewItemComparer();
            //packetsListView.Sorting = SortOrder.Ascending;
            packetsListView.Sorting = SortOrder.None;


            /*            packetsListView.RetrieveVirtualItem += PacketsListView_RetrieveVirtualItem;
                        packetsListView.CacheVirtualItems += PacketsListView_CacheVirtualItems;*/
            //packetsListView.SearchForVirtualItem += PacketsListView_SearchForVirtualItem;

            //packetsListView.VirtualListSize = 0;
        }

/*        private void PacketsListView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really neccesary.

            if (cache != null && e.StartIndex >= firstItem && e.EndIndex <= firstItem + cache.Length)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }

            //int length = e.EndIndex - e.StartIndex + 1; //indexes are inclusive
            int length = e.EndIndex - e.StartIndex;
            if (firstItem == e.StartIndex && cache != null && cache.Length == length) return;

            firstItem = e.StartIndex;

            cache = new ListViewItem[length];
            for (int index = 0; index < length; index++)
                cache[index] = filteredListViewItems.Values.ElementAt(firstItem + index);

            //cache = filteredListViewItems.Values.Skip(firstItem).Take(length).ToArray();

            //for (int i = 0; i < length; i++)
            //{
            //    //var packetData = filteredPacketDatas[i + firstItem];

            //        //packetData.Description != null ? CreateItemWithAnalysis(packetData) : CreateItemWithoutAnalysis(packetData);
            //}
        }

        private void PacketsListView_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            throw new NotImplementedException();
        }

        //int lastIndexShown;

        private void PacketsListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //lastIndexShown = e.ItemIndex;
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.

            //check to see if the requested item is currently in the cache
            if (cache != null && e.ItemIndex >= firstItem && e.ItemIndex < firstItem + cache.Length)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                e.Item = cache[e.ItemIndex - firstItem];
            }
            else
            {
                    e.Item = filteredListViewItems.Values.ElementAt( e.ItemIndex);

                //A cache miss, so create a new ListViewItem and pass it back.
                //int index = e.ItemIndex;

                //PacketData packetData;

                //lock (filteredPacketDatas)
                //{
                //    packetData = filteredPacketDatas[index];
                //}

                //if (packetData.Description != null)
                //    e.Item = CreateItemWithAnalysis(packetData);
                //else
                //    e.Item = CreateItemWithoutAnalysis(packetData);
            }
        }*/

        public IEnumerable<Packet> GetSelectedPackets()
        {
            var packets = new List<Packet>();

            foreach (var item in packetsListView.Items)
                packets.Add(((ListViewItem)item).Tag as Packet);
            return packets;
        }

        private void AddPacketCore(Packet packet)
        {
            var packetData = CreatePacketData(packet);

            //int indexToInsert;

            /*            lock (allPacketDatas)
                        {
                            indexToInsert = ~allPacketDatas.BinarySearch(
                                0, 
                                allPacketDatas.Count, 
                                PacketData.CreatePacketDataWithId(packetData.PacketId), 
                                PacketData.IdComparer);

                            allPacketDatas.Insert(indexToInsert, packetData);
                        }*/

            var item = CreateItemWithoutAnalysis(packetData);

            lock (updateSync)
            {
                allListViewItems.Add(packetData.PacketId, item);
            }

            //packetsListView.Invoke( 
            //    new MethodInvoker(() =>
            //    {
            //        packetsListView.Items.Add(item);
            //        packetsListView.Update();
            //    }));

            //if (packetsListView.Items.Count != 0)
            //    packetsListView.BeginInvoke(new MethodInvoker(() => packetsListView.Items[^1].EnsureVisible()));

            packetData.AnalyzeAsync();
        }


        private Task AddPacketCoreAsync(Packet packet)
        {
            return Task.Run(() => AddPacketCore(packet));
        }

        public void Clear()
        {
            //packetsListView.Invalidate();

            /*            lock (allPacketDatas)
                        {   
                            allPacketDatas.Clear();
                            filteredPacketDatas.Clear();
                        }*/

            //lock (virtualListSizeLock)

            lock (updateSync)
            {
                allListViewItems.Clear();
            }

/*            lock (filteredListViewItems)
            {
                filteredListViewItems.Clear();
                cache = null;
            }*/

            if (packetsListView.InvokeRequired)
            {
                packetsListView.Invoke(new MethodInvoker(
                delegate
                {
                    packetsListView.VirtualListSize = 0;
                }));
            }
            else
            {
                packetsListView.VirtualListSize = 0;
            }

            displayFilterString = string.Empty;
            displayFilter = DisplayFilter.EmptyFilter;

            displayFilterControl.Filter = string.Empty;
            displayFilterControl.IsValidFilter = true;
        }

        public async void AddPacket(Packet packet)
        {
            await AddPacketCoreAsync(packet);
            //itemBuilder.Post(packet);
        }

        //public void AddRange(IEnumerable<Packet> packets)
        //{
        //    foreach (var packet in packets)

        //        itemBuilder.Post(packet);
        //}

        PacketData CreatePacketData(Packet packet)
        {
            int id = IdManager.GetNewPacketId(packet);

            var packetData = new PacketData(id, packet);

            packetData.DescriptionReady += FillPacketDescription;

            packetData.AttackDetected += MarkAttack;

            return packetData;
        }

        static ListViewItem CreateItemWithoutAnalysis(PacketData packetData)
        {
            var subItems = new string[7] { packetData.PacketId.ToString(), "", "", "", "", "", "" };

            var item = new ListViewItem(subItems)
            {
                Tag = packetData
            };

            return item;
        }

        //static ListViewItem CreateItemWithAnalysis(PacketData packetData)
        //{
        //    var description = packetData.Description;

        //    var subItems = new string[7]
        //    {
        //         packetData.PacketId.ToString(),
        //        description.TimeStamp.ToString(),
        //        description.Protocol,
        //        AddressFormat.ToString(description.Source),
        //        AddressFormat.ToString(description.Destination),
        //        description.Length.ToString(),
        //        description.Info
        //    };

        //    Color foreColor = Color.Empty;

        //    return new ListViewItem(subItems)
        //    {
        //        BackColor = (packetData.Attacks.Length == 0) ? GetPacketColors(packetData.Packet, out foreColor) : Color.Red,
        //        ForeColor = (packetData.Attacks.Length == 0) ? foreColor : Color.White
        //    };
        //}

        void FillPacketDescription(int packetId, PacketDescription description)
        {
            lock (fillPacketSync)
            { 
          

            /*            var packetData = PacketData.GetPacketDataByPacketId(packetId);

                        if (displayFilter.PacketMatches(packetData))
                        {
                            lock (filteredPacketDatas)
                            {
                                int indexToInsert = ~filteredPacketDatas.BinarySearch(
                                    0,
                                    filteredPacketDatas.Count,
                                    PacketData.CreatePacketDataWithId(packetData.PacketId),
                                    PacketData.IdComparer);

                                if (indexToInsert != filteredPacketDatas.Count - 1)
                                    filteredPacketDatas.Insert(indexToInsert, packetData);
                                else
                                    filteredPacketDatas.Add(packetData);
                            }

                            packetsListView.Invoke(new MethodInvoker(delegate
                            {
                                packetsListView.BeginUpdate();
                                //lock (virtualListSizeLock)

                                F.VirtualListSize++;

                                packetsListView.EndUpdate();
                            }));F

                            //await Task.Factory.FromAsync(result, new Action<IAsyncResult>(result => { }));
                        }*/

                var packetData = PacketData.GetPacketDataByPacketId(packetId);
                bool hasAttacks = packetData.Attacks.Length != 0;

                ListViewItem item;

                item = allListViewItems[packetId];

                var matches = displayFilter.PacketMatches(packetData);

                bool invokeRequired = packetsListView.InvokeRequired;

                if (hasAttacks)
                {
                    if (invokeRequired)
                    {
                        packetsListView.Invoke(new MethodInvoker(
                        delegate
                        {
                            MarkItemAsAttack(item);
                        }
                        ));
                    }
                    else
                    {
                        MarkItemAsAttack(item);
                    }
                }
                else
                {
                    GetPacketColors(IdManager.GetPacket(packetId), out Color foreColor, out Color backColor);

                    if (invokeRequired)
                    {
                        packetsListView.Invoke(new MethodInvoker(
                        delegate
                        {
                            SetItemColors(item, foreColor, backColor);
                        }
                        ));
                    }
                    else
                    {
                        SetItemColors(item, foreColor, backColor);
                    }
                }

                if (invokeRequired)
                {
                    packetsListView.Invoke(new MethodInvoker(() =>
                    {
                        UpdateItem(item, description);
                    }));
                }
                else
                {
                    UpdateItem(item, description);
                }

                //lock (updateSync)
                //{
                    if (matches && item.ListView == null)
                    {
                        // filteredListViewItems.Add(packetId, item);
                        //packetsListView.VirtualListSize = filteredListViewItems.Count;

                        int index;

                        index = allListViewItems.Keys.ToList().BinarySearch(packetId);

                        index = index < 0 ? ~index : index;
                        if (invokeRequired)
                        {
                            packetsListView.Invoke(new MethodInvoker(() =>
                            {
                                if (index > packetsListView.Items.Count)
                                {
                                    if (item.ListView == null)
                                        packetsListView.Items.Add(item); // TODO: check synchronization error
                                }
                                else
                                {
                                    if (item.ListView == null)
                                        packetsListView.Items.Insert(index, item);
                                }
                                //packetsListView.Items.Add(item);
                                //packetsListView.VirtualListSize = filteredListViewItems.Count;
                            }));
                        }
                        else
                        {
                            if (index > packetsListView.Items.Count)
                                packetsListView.Items.Add(item);
                            else
                                packetsListView.Items.Insert(index, item);

                            //packetsListView.Items.Add(item);
                            //packetsListView.VirtualListSize = filteredListViewItems.Count;
                        }
                        /*if (packetsListView.InvokeRequired)
                        {
                            packetsListView.Invoke(new MethodInvoker(
                                delegate
                                {
                                    packetsListView.VirtualListSize++;
                                }
                                ));
                        }
                        else
                        {
                            packetsListView.VirtualListSize++;
                        }*/
                    }
                //}

                //IAsyncResult result = packetsListView.BeginInvoke((MethodInvoker)delegate
                //{
                //    if (matches && item.ListView == null)
                //    {
                //        packetsListView.Items.Add(item);
                //    }
                //});

                //await Task.Factory.FromAsync(result, new Action<IAsyncResult>(asyncResult => { }));
            }
        }

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
            /*            packetsListView.Invoke(new MethodInvoker(() =>
                        {
                            //cache = null;
                            packetsListView.Invalidate();
                        }));*/

            ListViewItem item;

            lock (updateSync)
                item = allListViewItems[packetId];

            if (packetsListView.InvokeRequired)
            {
                packetsListView.Invoke(new MethodInvoker(() =>
                {
                    MarkItemAsAttack(item);
                }));
            }
            else
            {
                MarkItemAsAttack(item);
            }
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
                //var packetId = filteredListViewItems.Keys.ElementAt(index);
                //var packetData = PacketData.GetPacketDataByPacketId(packetId);

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

        Task UpdateVisiblePacketsAsync()
        {
            return Task.Run(() => UpdateVisiblePackets());
        }

        object updateSync = new object();
        private object fillPacketSync= new object ();

        void UpdateVisiblePackets()
        {
            lock (updateSync)
            {
                packetsListView.Items.Clear();
                packetsListView.BeginUpdate();

                packetsListView.Items.AddRange(allListViewItems.Values.Where(item => displayFilter.PacketMatches((PacketData)item.Tag)).ToArray());
/*                foreach (var item  in allListViewItems.Values)
                {
                    if (displayFilter.PacketMatches((PacketData)item.Tag))
                    {
                        packetsListView.Items.Add(item);
                    }
                }*/

                packetsListView.EndUpdate();
/*                filteredListViewItems.Clear();

                foreach (var (index, item) in allListViewItems)
                {
                    var packetData = PacketData.GetPacketDataByPacketId(index);

                    if (displayFilter.PacketMatches(packetData))
                    {
                        filteredListViewItems.Add(index, item);
                    }
                }

                cache = null;
                packetsListView.VirtualListSize = filteredListViewItems.Count;*/
            }

            //lock (allPacketDatas)
            //{
            //    cache = null;

            //    lock (filteredPacketDatas)
            //    {
            //        filteredPacketDatas = allPacketDatas.Where(packetData => displayFilter.PacketMatches(packetData)).ToList();
            //    }

            //    packetsListView.Invoke(new MethodInvoker(
            //        delegate
            //        {
            //            packetsListView.SuspendLayout();

            //            lock (virtualListSizeLock)
            //                packetsListView.VirtualListSize = filteredPacketDatas.Count;

            //            packetsListView.ResumeLayout();
            //            packetsListView.Invalidate();
            //        }));

            //    int i = 0;

            //    packetsListView.Invoke(new MethodInvoker(() => packetsListView.BeginUpdate()));
            //    foreach (var )
            //    {
            //        i++;

            //        if (i == 50)
            //        {
            //            packetsListView.BeginInvoke((MethodInvoker)delegate
            //                {
            //                    packetsListView.EndUpdate();
            //                    packetsListView.BeginUpdate();
            //                });

            //            i = 0;
            //        }

            //        if (displayFilter.PacketMatches(packetData))
            //        {
            //            if (listViewItem.ListView == null)
            //            {
            //                packetsListView.Invoke(new MethodInvoker(() => packetsListView.Items.Add(listViewItem)));
            //            }
            //        }
            //        else
            //        {
            //            if (listViewItem.ListView != null)
            //                packetsListView.Invoke(new MethodInvoker(() => packetsListView.Items.Remove(listViewItem)));
            //        }
            //    }
            //    packetsListView.Invoke(new MethodInvoker(() => packetsListView.EndUpdate()));
            //}
        }

        private void DisplayFilter1_FilterChanged(object sender, string e)
        {
            displayFilterString = e;

            if (DisplayFilter.TryParse(displayFilterString, ref displayFilter))
            {
                displayFilterControl.IsValidFilter = true;
                UpdateVisiblePackets();
                //await UpdateVisiblePacketsAsync();
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

        //int prevWidth = 0;

        private void PacketsListView_Resize(object sender, EventArgs e)
        {
            info.Width = packetsListView.Width - packetsListView.Columns.Cast<ColumnHeader>().Aggregate(0, (sum, header) => sum + header.Width) + info.Width;
        }
    }
}
