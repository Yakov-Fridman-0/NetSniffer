using System;
using System.Threading;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace NetSnifferApp
{
    public partial class CaptureFilter : UserControl
    {
        static readonly Color blankColor = Color.FromKnownColor(KnownColor.Window);
        static readonly Color validColor = Color.FromArgb(0, 222, 11); //Color.FromArgb(124, Color.Green);
        static readonly Color invalidColor = Color.FromArgb(247, 95, 45); //Color.FromArgb(124, Color.Red);

        public event EventHandler FilterChanged = delegate { };

        public string Filter { get; private set; }

        public CaptureFilter()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            filterTextBox.Clear();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            Filter = filterTextBox.Text;

            if (typingTimer.Enabled)
            {
                typingTimer.Stop();
            }

            typingTimer.Start();
        }

        bool _isValidFilter = false;

        public bool IsValidFilter
        {
            get => _isValidFilter;
            set
            {
                _isValidFilter = value;

                if (_isValidFilter)
                {
                    filterTextBox.BackColor = IsBlank() ? blankColor : validColor;
                }
                else
                {
                    filterTextBox.BackColor = invalidColor;
                }
            }
        }

        bool IsBlank()
        {
            return string.IsNullOrWhiteSpace(Filter);
        }

        private void typingTimer_Tick(object sender, EventArgs e)
        {
            typingTimer.Stop();
            FilterChanged.Invoke(this, new EventArgs());
        }
    }
}
