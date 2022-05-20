using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArpTableWinFormsApp.ArpTable
{
    public partial class AddressForm : Form
    {
        bool _displayPhysicalAddress = true;
        public AddressForm()
        {
            InitializeComponent();
        }

        public bool DisplayPhysicalAddress
        {
            get => _displayPhysicalAddress;
            set
            {
                _displayPhysicalAddress = value;
                this.textBox2.Visible = _displayPhysicalAddress;
                this.label2.Visible = _displayPhysicalAddress;
            }
        }

        public string IPAddress { get => this.textBox1.Text; }
        public string PhysicalAddress { get => this.textBox2.Text; }
    }
}
