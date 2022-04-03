using System;
using System.Linq;
using System.Text;
using NetSnifferLib;
using PcapDotNet.Packets;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using System.Drawing;

using NetSnifferLib.General;
using NetSnifferLib.Analysis;
using NetSnifferLib.StatefulAnalysis;

namespace NetSnifferApp
{
    public partial class PacketViewer : UserControl
    {
        readonly ActionBlock<Packet> itemBuilder;
        
        ImageList smallImageList = new();

        public PacketViewer()
        {
            InitializeComponent();

            #region DoubleBuffered
            bool doubleBuffered = true;

            typeof(ListView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(this.packetsListView, doubleBuffered, null);
            #endregion

            itemBuilder = new ActionBlock<Packet>(packet => AddItemCore(packet));

            #region Columns
            packetsListView.Columns.Clear();

            packetsListView.Columns.Add(new ColumnHeader() { Text = "Index", Width = -2 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Time", Width = 100 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Protocol", Width = -2 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Source", Width = 150 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Destination", Width = 150 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Length", Width = 150 });
            packetsListView.Columns.Add(new ColumnHeader() { Text = "Info"});

            #endregion

            packetsListView.ListViewItemSorter = new ListViewItemComparer();
            packetsListView.Sorting = SortOrder.Ascending;

            smallImageList.Images.Add(Image.FromFile(@"E:\סייבר 2022\פרויקט\Sniffer\NetSniffer\NetSnifferApp\Resources\Danger.png"));
        }

        class ListViewItemComparer : IComparer
        {
            readonly IComparer<PacketData> packetDataCompater = PacketData.Comparer;

            public int Compare(object x, object y)
            {
                if (x is ListViewItem dataX && y is ListViewItem dataY)
                {
                    return packetDataCompater.Compare((PacketData)dataX.Tag, (PacketData)dataY.Tag);
                }

                throw new ArgumentException("Input must be of type PacketData");
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
            /*            var item = CreateViewItem(packet);
                        if (item == null)
                            return;

                        packetsListView.Invoke(new MethodInvoker(() => 
                        {
                            //Set item index
                            item.SubItems[0].Text = (packetsListView.Items.Count + 1).ToString();
                            packetsListView.Items.Add(item);
                        }));*/

            var item = CreateNewItem(packet);
            packetsListView.Invoke(
                new MethodInvoker(() =>
                {
                    packetsListView.Items.Add(item);
                    ((PacketData)item.Tag).StartAnalysis();
                }));
        }

        public void Clear()
        {
            packetsListView.Items.Clear();
            packetsListView.Invalidate();
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

        private ListViewItem CreateViewItem(Packet packet)
        {
            var items = GetSubitems(packet);
            if (items == null)
                return null;

            var listViewItem = new ListViewItem(items)
            {
                Tag = packet
            };

            return listViewItem;
        }

        ListViewItem CreateNewItem(Packet packet)
        {
            int id = IdManager.GetNewPacketId(packet);

            var packetData = new PacketData(id, packet);
            
            packetData.DescriptionReady += FillPacketDescription;
            packetData.AttackDetected += MarkAttack;

            var subItems = new string[7] { id.ToString(), "", "", "", "", "", "" };

            var item = new ListViewItem(subItems)
            {
                Tag = packetData
            };

            return item;
        }

        private string[] GetSubitems(Packet packet)
        {
            string[] subItems = new string[7];
            subItems[0] = "";

            
            if (PacketAnalyzer.IsEthernet(packet))
            {
                PacketDescription packetDescription = PacketAnalyzer.AnalyzePacket(packet, 0);

                subItems[1] = packetDescription.TimeStamp.ToString("HH:mm:ss:fff");
                subItems[2] = packetDescription.Protocol;
                subItems[3] = AddressFormat.ToString(packetDescription.Source);
                subItems[4] = AddressFormat.ToString(packetDescription.Destination);
                subItems[5] = packetDescription.Length.ToString();
                subItems[6] = packetDescription.Info;
            }
            else
            {
                subItems[1] = "";
                subItems[2] = "";
                subItems[3] = "";
                subItems[4] = "";
                subItems[5] = "";
                subItems[6] = "Only Ethernet packets are supported";
            }

            return subItems;
        }

        void FillPacketDescription(int pakcetId, PacketDescription description)
        {
            var item = (ListViewItem)packetsListView.Invoke(new Func<ListViewItem>(() => packetsListView.Items[pakcetId]));
            var subItems = item.SubItems;

            packetsListView.Invoke(new MethodInvoker(() =>
            {
                subItems[1].Text = description.TimeStamp.ToString();
                subItems[2].Text = description.Protocol;
                subItems[3].Text = AddressFormat.ToString(description.Source);
                subItems[4].Text = AddressFormat.ToString(description.Destination);
                subItems[5].Text = description.Length.ToString();
                subItems[6].Text = description.Info;
            }));
        }

        void MarkAttack(int packetId, Attack attack)
        {
            packetsListView.Invoke(new MethodInvoker(() =>
            { 
                var item =  packetsListView.Items[packetId]; 
                item.ImageIndex = 0;
                item.BackColor = Color.Red;
                item.ForeColor = Color.White;
                Update();
            }));

        }

        private void packetsListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (packetsListView.SelectedItems.Count == 1)
            {
                var item = packetsListView.SelectedItems[0];

                var packetData = (PacketData)item.Tag;
                
                byte[] buffer = packetData.Packet.Buffer;

                binaryDataTextBox.Text = Convert.ToHexString(buffer);

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
    }
}
