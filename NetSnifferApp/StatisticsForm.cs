using System;
using System.Windows.Forms;
using NetSnifferLib.Statistics;

namespace NetSnifferApp
{
    public partial class GeneralStatisticsForm : Form
    {
        const string NumberFormat = "{0:N}";
        const string BytesFormat = "{0:N} B";
        const string SecondsFormat = "{0:N} s";

        public event EventHandler NewStatisticsRequired;

        GeneralStatistics baseStatistics = default;
        DateTime baseTime;

        GeneralStatistics currentStatistics;

        bool frozen = false;

        public GeneralStatisticsForm()
        {
            InitializeComponent();

            baseTime = DateTime.Now;

            SetStatistics(default);
        }

        public void SendNewStatistics(GeneralStatistics newStatistics)
        {
            currentStatistics = newStatistics;
            SetStatistics(newStatistics - baseStatistics);
        }

        private void SetStatistics(GeneralStatistics statistics)
        {
            tNumLbl.Text = string.Format(NumberFormat, statistics.TransmittedPackets.ToString());
            tBytesLbl.Text = string.Format(BytesFormat, statistics.TransmittedBytes.ToString());

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

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            NewStatisticsRequired.Invoke(this, EventArgs.Empty);
        }

        private void zeroButton_Click(object sender, EventArgs e)
        {
            baseStatistics = currentStatistics;
            baseTime = DateTime.Now;

            SetStatistics(default);
            elapsedTimeLabel.Text = string.Format(SecondsFormat, 0);
        }

        private void GeneralStatisticsForm_Load(object sender, EventArgs e)
        {
            frozen = false;
            refreshTimer.Start();
            elapsedTimeTimer.Start();
        }

        private void elapsedTimeTimer_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabel.Text = string.Format(SecondsFormat, (DateTime.Now - baseTime).TotalSeconds);
        }

        private void freezeButtom_Click(object sender, EventArgs e)
        {
            if (frozen)
            {
                freezeButtom.Text = "Freeze";
                refreshTimer.Start();
                elapsedTimeTimer.Start();
            }
            else
            {
                freezeButtom.Text = "Unfreeze";
                refreshTimer.Stop();
                elapsedTimeTimer.Stop();
            }

            frozen = !frozen;
        }
    }
}
