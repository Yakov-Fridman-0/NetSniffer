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
    public partial class ArpTableEntityControl : UserControl
    {
        public ArpTableEntityControl()
        {
            InitializeComponent();
        }

        internal void SetEntity(ArpTabeEntity entity)
        {
            this.SuspendLayout();

            this.lblNic.Text = entity.NetworkInterface.Name;


            foreach (var item in entity.Items)
            {
                this.ltvTable.Items.Add(new ListViewItem(new string[] { item.IPAddress.ToString(), item.PhysicalAddress.ToString(), item.ArpType.ToString() }));
            }

            var totalHeigh = 0;
            foreach (var item in this.ltvTable.Items)
            {
                totalHeigh += (item as ListViewItem).Bounds.Height;
            }


            this.lblNic.Location = new Point(
                this.lblNic.Margin.Left,
                this.lblNic.Margin.Top);

            this.ltvTable.Location = new Point(
                this.ltvTable.Margin.Left,
                this.lblNic.Bottom + this.lblNic.Margin.Bottom + this.ltvTable.Margin.Top);

            var totalWidth = 0;
            for (var index = 0; index < this.ltvTable.Columns.Count; index++) {
                this.ltvTable.AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.HeaderSize);
                totalWidth += this.ltvTable.Columns[index].Width;
            }
          
            this.ltvTable.ClientSize = new Size(totalWidth, totalHeigh);

            this.Size = new Size(this.ltvTable.Margin.Left + this.ltvTable.Width + this.ltvTable.Margin.Right,
            this.lblNic.Margin.Top +this.lblNic.Height+ this.lblNic.Margin.Bottom+
                this.ltvTable.Margin.Top + this.ltvTable.Height + this.ltvTable.Margin.Bottom 
            );
   
            //this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.AutoSize = false;


            this.ResumeLayout(true);
        }
    }
}
