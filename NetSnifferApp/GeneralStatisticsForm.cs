using System;
using System.Windows.Forms;
using NetSnifferLib.Statistics;

namespace NetSnifferApp
{
    public partial class GeneralStatisticsForm : Form
    {
        const string NumberFormat = "{0:N}";
        const string BytesFormat = "{0:N} B";
        const string SecondsFormat = "{0:0.0} s";

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

                    titleLabel.Text = "Statistics of dump file";
                    titleLabel.Visible = true;
                    CenterTitleLabel();

                    freezeButtom.Visible = false;

                    zeroButton.Visible = false;

                    elapsedTimeTitleLabel.Text = "Total Time";
                }
            }
        }


        bool _captureStarted = false;
        public bool CaptureStarted
        {
            get => _captureStarted;
            set
            {
                _captureStarted = value;

                if (_captureStarted)
                {
                    titleLabel.Visible = false;
                }
                else
                {
                    titleLabel.Text = "Waiting for capture to start";
                    titleLabel.Visible = true;
                    CenterTitleLabel();
                }
            }
        }


        bool _captureStopped = false;
        public bool CaptureStopped
        {
            get => _captureStopped;
            set
            {
                _captureStopped = value;

                if (_captureStopped)
                {
                    titleLabel.Text = "Capture Stopped";
                    titleLabel.Visible = true;
                    CenterTitleLabel();

                    freezeButtom.Enabled = false;
                    zeroButton.Enabled = false;
                    ShowElapsedTime(DateTime.Now - baseTime);
                }
                else
                {
                    titleLabel.Visible = false;
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

            ShowStatistics(newStatistics - baseStatistics);
        }

        private void CenterTitleLabel()
        {
            var y = titleLabel.Location.Y;

            int newX = (ClientRectangle.Width - titleLabel.Width) / 2;

            titleLabel.Location = new System.Drawing.Point(newX, y);
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
            titleLabel.Text = "Capture stopped";
            titleLabel.Visible = true;
            CenterTitleLabel();

            elapsedTimeTimer.Stop();
            updateTimer.Stop();
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
            SuspendLayout();
            tableLayoutPanel1.SuspendLayout();

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

            tableLayoutPanel1.ResumeLayout();
            ResumeLayout();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            StatisticsUpdateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ZeroButton_Click(object sender, EventArgs e)
        {
            updateTimer.Stop();
            baseStatistics = currentStatistics;
            updateTimer.Start();

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

        private void ElapsedTimeTimer_Tick(object sender, EventArgs e)
        {
            ShowElapsedTime(DateTime.Now - baseTime);
        }

        void ShowElapsedTime(TimeSpan timeSpan)
        {
            timeLabel.Text = string.Format(SecondsFormat, timeSpan.TotalSeconds);
        }

        private void FreezeButtom_Click(object sender, EventArgs e)
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
