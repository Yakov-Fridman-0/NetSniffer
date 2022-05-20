using ArpTable;
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
    public partial class RouteTabeSimple : Form
    {
        public RouteTabeSimple()
        {
            InitializeComponent();
            this.listBox1.Font = new Font("Consolas", this.listBox1.Font.Size);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var table = AprTableHelper.GetRouteTable();
            foreach (var item in table)
            {
                this.listBox1.Items.Add(item);
            }
        }
    }
}
