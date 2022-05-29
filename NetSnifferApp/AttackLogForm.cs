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
            updateTimer.Start();
        }

        static readonly Dictionary<string, string> attackLinks = new()
        {
            { "Syn Flood (TCP)", @"https://www.imperva.com/learn/ddos/syn-flood" },
            { "Man-in-the-Middle (ARP)", @"https://www.imperva.com/learn/application-security/man-in-the-middle-attack-mitm" }
        };

        private void UpdateTime_Tick(object sender, EventArgs e)
        {
            StringBuilder strBuilder = new();

            attacksNumberLabel.Text = $"Attacks No.: {PacketData.AllAttacks.Length}";

            foreach (var (attack, index) in PacketData.AllAttacks.Select((attack, i) => (attack, i)))
            {
                StringBuilder attackStringBuilder = new();

                attackStringBuilder.Append($"\\line Attack Name: {{\\field{{\\*\\fldinst{{HYPERLINK \\\"{attackLinks[attack.Name]}\"}}}}{{\\fldrslt{{\\ul {attack.Name}}}}}}}");
                attackStringBuilder.Append($"\\line Packets: {string.Join<string>(", ", attack.PacketIds.Select(id => id.ToString()))}");
                attackStringBuilder.Append($"\\line Attackers: {string.Join<string>(", ", attack.Attackers.Select(address => AddressFormat.ToString(address)))}");
                attackStringBuilder.Append($"\\line Targets: {string.Join<string>(", ", attack.Targets.Select(address => AddressFormat.ToString(address)))}");
                attackStringBuilder.Append("\\line ");

                strBuilder.Append(attackStringBuilder);
            }

            string rtf = $"{{\\rtf1\\ansi\\deff0 {{\\fonttbl {{\\f0 Segoe UI;}}}} \\f0\\fs18 {strBuilder} }}";

            richTextBox.Rtf = rtf;
        }

        async private void Button1_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
            };

            string text = string.Empty;
            StringBuilder strBuilder = new();

            foreach (var (attack, index) in PacketData.AllAttacks.Select((attack, i) => (attack, i)))
            {
                strBuilder.AppendLine($"**** {index} ****");
                strBuilder.AppendLine($"Attack Name: {attack.Name}");
                strBuilder.AppendLine($"Packets: {string.Join<string>(", ", attack.PacketIds.Select(id => id.ToString()))}");
                strBuilder.AppendLine($"Attackers: {string.Join<string>(", ", attack.Attackers.Select(address => AddressFormat.ToString(address)))}");
                strBuilder.AppendLine($"Targets: {string.Join<string>(", ", attack.Targets.Select(address => AddressFormat.ToString(address)))}");
                strBuilder.AppendLine();
            }

            text += strBuilder.ToString();

            var result = dialog.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                var fileName = dialog.FileName;

                await File.WriteAllTextAsync(fileName, text);
            }
        }

        private void RichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            var uri = e.LinkText;
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = uri
            };

            System.Diagnostics.Process.Start(psi);
        }
    }
}
