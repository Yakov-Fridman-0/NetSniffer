using System;
using System.Windows.Forms;
using NetSnifferLib.Statistics;

namespace NetSnifferApp
{
    public partial class GeneralStatisticsForm : Form
    {
        private const string NumberFormat = "{0:N}";
        private const string BytesFormat = "{0:N} B";
        private const string MsFormat = "{0:N} ms";

        public event EventHandler NewStatisticsRequired;

        GeneralStatistics baseStatistics = default;
        DateTime baseTime = default;

        GeneralStatistics currentStatistics;

        public GeneralStatisticsForm()
        {
            InitializeComponent();

            SetStatistics(default);
        }

        public void SendNewStatistics(GeneralStatistics newStatistics)
        {
            currentStatistics = newStatistics;
            SetStatistics(newStatistics - baseStatistics);
        }

        private void SetStatistics(GeneralStatistics statistics)
        {
            tNumLbl.Text = string.Format(NumberFormat, statistics.Packets.ToString());
            tBytesLbl.Text = string.Format(BytesFormat, statistics.Bytes.ToString());

            eNumLbl.Text = string.Format(NumberFormat, statistics.EthernetPackets.ToString());
            eBytesLbl.Text = string.Format(BytesFormat, statistics.EthernetPayloadBytes.ToString());

            ip4NumLbl.Text = string.Format(NumberFormat, statistics.IpV4Packets.ToString());
            ip4BytesLbl.Text = string.Format(BytesFormat, statistics.IpV4PayloadBytes.ToString());

            ip6NumLbl.Text = string.Format(NumberFormat, statistics.IpV6Packets.ToString());
            ip6BytesLbl.Text = string.Format(BytesFormat, statistics.IpV6PayloadBytes.ToString());

            udpNumLbl.Text = string.Format(NumberFormat, statistics.UdpPackets.ToString());
            udpBytesLbl.Text = string.Format(BytesFormat, statistics.UdpPayloadBytes.ToString());

            tcpNumLbl.Text = string.Format(NumberFormat, statistics.TcpPackets.ToString());
            tcpBytesLbl.Text = string.Format(BytesFormat, statistics.TcpPayloadBytes.ToString());
        }

        private void refreshStatisicsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (refreshStatisicsCheckBox.Checked)
            {
                refreshTimer.Start();
                elapsedTimeTimer.Start();
            }
            else
            {
                refreshTimer.Stop();
                elapsedTimeTimer.Stop();
            }               
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            NewStatisticsRequired.Invoke(this, EventArgs.Empty);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            baseStatistics = currentStatistics;
            baseTime = DateTime.Now;

            SetStatistics(default);
        }

        private void GeneralStatisticsForm_Load(object sender, EventArgs e)
        {
            refreshStatisicsCheckBox.Checked = true;
            refreshTimer.Start();
        }

        private void elapsedTimeTimer_Tick(object sender, EventArgs e)
        {
            elpasedTimeLabel.Text = string.Format(MsFormat, (DateTime.Now - baseTime).TotalMilliseconds);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
