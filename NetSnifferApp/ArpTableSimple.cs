using ArpTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArpTableWinFormsApp.ArpTable
{
    public partial class ArpTableSimple : Form
    {
        public ArpTableSimple()
        {
            InitializeComponent();
            this.listBox1.Font = new Font("Consolas", this.listBox1.Font.Size);
        }

        private void LoadArpTable()
        {
            this.listBox1.Items.Clear();
            var table = AprTableHelper.GetArpTable();
            foreach (var item in table)
            {
                this.listBox1.Items.Add(item);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadArpTable();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            var addressForm = new AddressForm();
            if (addressForm.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrEmpty(addressForm.IPAddress)||  IPAddress.TryParse(addressForm.IPAddress, out var ipAddress)==false)
            {
                MessageBox.Show("IP Address is invalid", "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(addressForm.PhysicalAddress) || PhysicalAddress.TryParse(addressForm.PhysicalAddress, out var physicalAddress) == false)
            {
                MessageBox.Show("Physical Address is invalid", "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AprTableHelper.AddEntryToArpCache(ipAddress, physicalAddress, out string message, out string error);
            if (string.IsNullOrEmpty(error) == false)
            {
                MessageBox.Show(error, "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            if (string.IsNullOrEmpty(message) == false)
            {
                MessageBox.Show(message, "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            LoadArpTable();

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var addressForm = new AddressForm();
            addressForm.DisplayPhysicalAddress = false;
            if (addressForm.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrEmpty(addressForm.IPAddress) || IPAddress.TryParse(addressForm.IPAddress, out var ipAddress) == false)
            {
                MessageBox.Show("IP Address is invalid", "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          

            AprTableHelper.DeleteEntireArpCache(ipAddress, out string message, out string error);
            if (string.IsNullOrEmpty(error) == false)
            {
                MessageBox.Show(error, "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(message) == false)
            {
                MessageBox.Show(message, "Arp Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            LoadArpTable();
        }
    }


}
