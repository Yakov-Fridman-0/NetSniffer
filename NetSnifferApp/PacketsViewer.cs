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
        private PacketAnalyzer _packetAnalyzer;

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

        public void SetAnalyzer(PacketAnalyzer packetAnalyzer)
        {
            _packetAnalyzer = packetAnalyzer;
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
                //Set item index
                item.SubItems[0].Text = (lstvPackets.Items.Count + 1).ToString();
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

        private string[] GetSubitems(Packet packet)
        {
            string[] subItems = new string[7];
            subItems[0] = "";

            
            if (PacketAnalyzer.IsEthernet(packet))
            {
                PacketDescription packetDescription = _packetAnalyzer.AnalyzePacket(packet);

                subItems[1] = packetDescription.TimeStamp;
                subItems[2] = packetDescription.Protocol;
                subItems[3] = packetDescription.Source;
                subItems[4] = packetDescription.Destination;
                subItems[5] = packetDescription.Length;
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
            /*var analyzer = PacketAnalyzer.GetAnalyzer(packet);

            if (analyzer != null)
            {
                //Time
                var timestamp = analyzer.GetTimeStamp(packet);
                subItems[1] = timestamp.ToString("HH:mm:ss.ffff");
                //Protocol
                subItems[2] = analyzer.GetProtocol(packet);
                //Source
                subItems[3] = analyzer.GetPacketSource(packet);
                //"Destination
                subItems[4] = analyzer.GetPacketDestination(packet);
                //Payload Length
                var payloadLength = analyzer.GetLength(packet);
                subItems[5] = payloadLength == 0 ? string.Empty : payloadLength.ToString();
                //Info
                subItems[6] = analyzer.GetPacketInfo(packet);
            }

            return subItems;*/
        }
    }
}
