using ArpTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArpTableWinFormsApp.ArpTable
{
    public partial class ArpTableControl : UserControl
    {

        public ArpTableControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadArpTable();
        }

        private void LoadArpTable()
        {
            var arpTabe = AprTableHelper.GetTable();

            var top = 0;
            foreach (var item in arpTabe.Items)
            {
                var arpTableEntityControl = new ArpTableEntityControl();

                arpTableEntityControl.SetEntity(item);
                arpTableEntityControl.Location = new Point(0, top);
                top += arpTableEntityControl.Height;

                panel2.Controls.Add(arpTableEntityControl);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.128.100");
            PhysicalAddress physicalAddress = PhysicalAddress.Parse("001122AABBCC");

            AprTableHelper.AddEntryToArpCache(ipAddress, physicalAddress, out string message, out string error);
            if (string.IsNullOrEmpty(error) == false)
            {
                MessageBox.Show(error, "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(message) == false)
            {
                MessageBox.Show(message, "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
               
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.128.100");
            AprTableHelper.DeleteEntireArpCache(ipAddress, out string message, out string error);
            if (string.IsNullOrEmpty(error) == false)
            {
                MessageBox.Show(error, "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(message) == false)
            {
                MessageBox.Show(message, "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
