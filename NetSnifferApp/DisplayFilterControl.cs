using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSnifferApp
{
    public partial class DisplayFilterControl : UserControl
    {
        static readonly Color blankColor = Color.FromKnownColor(KnownColor.Window);
        static readonly Color validColor = Color.FromArgb(0, 222, 11); //Color.FromArgb(124, Color.Green);
        static readonly Color invalidColor = Color.FromArgb(247, 95, 45); //Color.FromArgb(124, Color.Red);

        string _filter = string.Empty;
        
        public string Filter 
        {
            get => _filter;
            set
            {
                filterTextBox.Text = string.Empty;
            }
        } 

        public DisplayFilterControl()
        {
            InitializeComponent();
        }

        bool _isValidFilter = false;

        public event EventHandler<string> FilterChanged;

        private void clearButton_Click(object sender, EventArgs e)
        {
            filterTextBox.Clear();
            FilterChanged?.Invoke(this, string.Empty);
        }

        public bool IsValidFilter
        {
            get => _isValidFilter;
            set
            {
                _isValidFilter = value;

                if (_isValidFilter)
                    filterTextBox.BackColor = IsBlank()  ? blankColor : validColor; //Color.FromArgb(124, Color.Green);
                else
                    filterTextBox.BackColor = invalidColor; //Color.FromArgb(124, Color.Red);
            }
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            _filter = filterTextBox.Text;

            if (typingTimer.Enabled)
            {
                typingTimer.Stop();
            }

            typingTimer.Start();
            //FilterChanged?.Invoke(this, _filter);
        }

        bool IsBlank()
        {
            return string.IsNullOrWhiteSpace(Filter);
        }

        private void typingTimer_Tick(object sender, EventArgs e)
        {
            typingTimer.Stop();
            FilterChanged.Invoke(this, _filter);
        }
    }
}
