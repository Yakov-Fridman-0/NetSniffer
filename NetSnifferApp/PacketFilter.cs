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
    public partial class PacketFilter : UserControl
    {
        private bool validFilter = false;

        public event EventHandler<string> FilterChanged;

        public PacketFilter()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            FilterTextBox.Clear();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = FilterTextBox.Text;
           
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                return;

            EventHandler<string> handler = FilterChanged;
            handler?.Invoke(this, text);
        }

        public bool ValidFilter
        {
            get => validFilter;
            set
            {
                if (value)
                    FilterTextBox.BackColor = Color.Green; //Color.FromArgb(124, Color.Green);
                else
                    FilterTextBox.BackColor = Color.Red; //Color.FromArgb(124, Color.Red);

                validFilter = value;
            }
        }

    }
}
