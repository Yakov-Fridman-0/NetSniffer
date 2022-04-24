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
    public partial class CaptureFilter : UserControl
    {
        bool _isValidFilter = false;

        public event EventHandler<string> FilterChanged;

        public string Filter { get; private set; }

        public CaptureFilter()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            filterTextBox.Clear();
            FilterChanged?.Invoke(this, string.Empty);
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            Filter = filterTextBox.Text;
           
/*            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                return;*/

            FilterChanged?.Invoke(this, Filter);
        }

        public bool IsValidFilter
        {
            get => _isValidFilter;
            set
            {
                _isValidFilter = value;

                if (_isValidFilter)
                {
                    filterTextBox.BackColor = string.IsNullOrWhiteSpace(filterTextBox.Text) ? Color.FromKnownColor(KnownColor.Window) : Color.Green; 
                    //Color.FromArgb(124, Color.Green);
                }
                else
                {
                    filterTextBox.BackColor = Color.Red; 
                    //Color.FromArgb(124, Color.Red);
                }

            }
        }
    }
}
