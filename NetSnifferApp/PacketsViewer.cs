using NetSnifferLib;
using PcapDotNet.Packets;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace NetSnifferApp
{
    public partial class PacketViewer : UserControl
    {
        private readonly ActionBlock<Packet> _itemsBuilder;

        public PacketViewer()
        {
            InitializeComponent();

            #region DoubleBuffered
            bool doubleBuffered = true;

            typeof(ListView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(this.lstvPackets, doubleBuffered, null);
            #endregion

            _itemsBuilder = new ActionBlock<Packet>(packet => AddItemCore(packet));


            #region Columns
            lstvPackets.Columns.Clear();

            lstvPackets.Columns.Add(new ColumnHeader() { Text = "Index", Width = -2 });
            lstvPackets.Columns.Add(new ColumnHeader() { Text = "Time", Width = 100 });
            lstvPackets.Columns.Add(new ColumnHeader() { Text = "Protocol", Width = -2 });
            lstvPackets.Columns.Add(new ColumnHeader() { Text = "Source", Width = 150 });
            lstvPackets.Columns.Add(new ColumnHeader() { Text = "Destination", Width = 150 });
            lstvPackets.Columns.Add(new ColumnHeader() { Text = "Payload Length", Width = 150 });
            lstvPackets.Columns.Add(new ColumnHeader() { Text = "Info" });

            #endregion
        }

        public IEnumerable<Packet> GetSelectedPackets()
        {
            var packets = new List<Packet>();

            foreach (var item in lstvPackets.Items)
                packets.Add(((ListViewItem)item).Tag as Packet);
            return packets;
        }

        private void AddItemCore(Packet packet)
        {
            var item = CreateViewItem(packet);
            if (item == null)
                return;

            lstvPackets.Invoke(new MethodInvoker(delegate ()
            {
                //set item index
                item.SubItems[0].Text = (lstvPackets.Items.Count + 1).ToString();//(this.lstvPackets.Items.Count + 1).ToString();//(++_index).ToString()
                lstvPackets.Items.Add(item);
            }));
        }

        public void Clear()
        {
            lstvPackets.Items.Clear();
            lstvPackets.Invalidate();
        }

        public void Add(Packet packet)
        {
            _itemsBuilder.Post(packet);
        }

        public void AddRange(IEnumerable<Packet> packets)
        {
            foreach (var packet in packets)
                _itemsBuilder.Post(packet);
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

        private static string[] GetSubitems(Packet packet)
        {
            string[] subItems = new string[7];
            subItems[0] = "";

            var timestamp = PacketHelper.GetTimeStamp(packet);
            subItems[1] = timestamp.ToString("HH:mm:ss.ffff");

            var layer = PacketHelper.GetLayer(packet);
            bool result;

            if (layer == PacketHelper.OSILayer.TransportLayer)
            {
                result = PacketHelper.GetTransportData(packet, out var protocol, out var payloadLength, out var sourcePort, out var destPort);
                if (!result)
                    return null;

                subItems[2] = protocol.ToString();
                if (sourcePort != 0 && destPort != 0)
                {
                    result = PacketHelper.GetIpData(packet, out payloadLength, out var sourceIp, out var destIp);
                    if (!result)
                        return null;
                    subItems[3] = sourceIp + ":" + sourcePort.ToString();
                    subItems[4] = destIp + ":" + destPort.ToString();
                }
                subItems[5] = payloadLength != 0 ? payloadLength.ToString() : "";
            }

            if (layer == PacketHelper.OSILayer.NetworkLayer)
            {
                result = PacketHelper.GetIpData(packet, out var payloadLength, out var source, out var dest);
                if (!result)
                    return null;
                subItems[2] = "IP";
                subItems[3] = source.ToString();
                subItems[4] = dest.ToString();
                subItems[5] = payloadLength != 0 ? payloadLength.ToString() : "";
            }

            if (layer == PacketHelper.OSILayer.BetweenDataLinkAndNetwork)
            {
                result = PacketHelper.GetArpData(packet, out var source, out var dest);
                if (!result)
                    return null;

                subItems[2] = "ARP";
                subItems[3] = source.ToString();
                subItems[4] = dest.ToString();
            }

            return subItems;
        }
    }
}
