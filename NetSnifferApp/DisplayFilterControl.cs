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
                    filterTextBox.BackColor = string.IsNullOrWhiteSpace(filterTextBox.Text)  ? Color.FromKnownColor(KnownColor.Window) : Color.Green; //Color.FromArgb(124, Color.Green);
                else
                    filterTextBox.BackColor = Color.Red; //Color.FromArgb(124, Color.Red);
            }
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            _filter = filterTextBox.Text;

            FilterChanged?.Invoke(this, _filter);
        }
    }
}
