using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSnifferLib;

namespace NetSnifferApp
{
    public partial class AttackLogForm : Form
    {
        public AttackLogForm()
        {
            InitializeComponent();
        }

        private void AttackLogForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = "";

            foreach (var attack in PacketData.AllAttacks)
            {
                var strBuilder = new StringBuilder();
                strBuilder.AppendLine($"Packets: {string.Join<string>(", ", attack.PacketIds.Select(id => id.ToString()))}");
                strBuilder.AppendLine($"Attackers: {string.Join<string>(", ", attack.Attackers.Select(address => AddressFormat.ToString(address)))}");
                strBuilder.AppendLine($"Targets: {string.Join<string>(", ", attack.Targets.Select(address => AddressFormat.ToString(address)))}");
                
                textBox1.Text += strBuilder.ToString();
                textBox1.Text += "\r\n";
            }
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
            };

            var result = dialog.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                var fileName = dialog.FileName;

                await File.WriteAllTextAsync(fileName, textBox1.Text);
            }
        }
    }
}
