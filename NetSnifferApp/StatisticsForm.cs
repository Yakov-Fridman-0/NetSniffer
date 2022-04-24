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

        public event EventHandler StatisticsUpdateRequested;

        GeneralStatistics baseStatistics = default;
        DateTime baseTime;

        GeneralStatistics currentStatistics;

        bool frozen = false;


        bool _isLive = true;

        public bool IsLive 
        {
            get => _isLive;
            set
            {
                _isLive = value;
                
                if (_isLive)
                {
                    titleLabel.Visible = false;

                    freezeButtom.Visible = true;
                    freezeButtom.Enabled = true;

                    zeroButton.Visible = true;
                    zeroButton.Enabled = true;
                }
                else
                {

                    titleLabel.Visible = true;
                    titleLabel.Text = "Statistics of dump file";

                    freezeButtom.Visible = false;

                    zeroButton.Visible = false;

                    updateTimer.Interval = 2000;

                    elapsedTimeTitleLabel.Text = "Total Time";
                }
            }
        }

        public GeneralStatisticsForm()
        {
            InitializeComponent();

            baseTime = DateTime.Now;

            ShowStatistics(default);
        }

        public void UpdateStatistics(GeneralStatistics newStatistics)
        {
            currentStatistics = newStatistics;

            if (_isLive)
            {
                ShowStatistics(newStatistics - baseStatistics);
            }
        }

        public void StartRequestingUpdates()
        {
            if (_isLive)
            {
                frozen = false;

                elapsedTimeTimer.Start();
                updateTimer.Start();
            }
            else
            {
                updateTimer.Start();
            }
        }

        public void StopRequestingUpdates()
        {
            if (_isLive)
            {
                titleLabel.Text = "Capture stopped";
                titleLabel.Visible = true;

                freezeButtom.Enabled = false;
                zeroButton.Enabled = false;

                elapsedTimeTimer.Stop();
                updateTimer.Stop();
            }
            else
            {
                //timeLabel.Text = string.Format(SecondsFormat, (DateTime.Now - startingTime).TotalSeconds);
                ShowStatistics(currentStatistics);
            }    
        }

        public void Clear()
        {
            timeLabel.Text = string.Format(SecondsFormat, 0);
            baseStatistics = default;
            ShowStatistics(default);
        }

        public void SetBaseTime(DateTime time)
        {
            baseTime = time;
        }

        private void ShowStatistics(GeneralStatistics statistics)
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
            StatisticsUpdateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void zeroButton_Click(object sender, EventArgs e)
        {
            baseStatistics = currentStatistics;
            baseTime = DateTime.Now;

            ShowStatistics(default);
            timeLabel.Text = string.Format(SecondsFormat, 0);
        }

        private void GeneralStatisticsForm_Load(object sender, EventArgs e)
        {
            if (IsLive)
            {
                frozen = false;

                elapsedTimeTitleLabel.Visible = true;
                timeLabel.Visible = true;

                freezeButtom.Visible = true;
                zeroButton.Visible = true;
            }
            else
            {
                elapsedTimeTitleLabel.Visible = false;
                timeLabel.Visible = false;

                freezeButtom.Visible = false;
                zeroButton.Visible = false;
            }
        }

        private void elapsedTimeTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = string.Format(SecondsFormat, (DateTime.Now - baseTime).TotalSeconds);
        }

        private void freezeButtom_Click(object sender, EventArgs e)
        {
            if (frozen)
            {
                freezeButtom.Text = "Freeze";
                updateTimer.Start();
                elapsedTimeTimer.Start();
            }
            else
            {
                freezeButtom.Text = "Unfreeze";
                updateTimer.Stop();
                elapsedTimeTimer.Stop();
            }

            frozen = !frozen;
        }
    }
}
