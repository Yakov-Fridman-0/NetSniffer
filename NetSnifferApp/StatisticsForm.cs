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
    public partial class StatisticsForm : Form
    {
        private const string PacketsFormat = "{0:N}";
        private const string BytesFormat = "{0:N} Bytes";

        public StatisticsForm(
            int tNum, int tBytes, 
            int eNum, int eBytes, 
            int ip4Num, int ip4Bytes, 
            int ip6Num, int ip6Bytes,
            int udpNum, int udpBytes,
            int tcpNum, int tcpBytes)
        {
            InitializeComponent();

            tNumLbl.Text = string.Format(PacketsFormat, tNum.ToString());

            eNumLbl.Text = string.Format(PacketsFormat, eNum.ToString());

            ip4NumLbl.Text = string.Format(PacketsFormat, ip4Num.ToString());
            ip4BytesLbl.Text = string.Format(BytesFormat, ip4Bytes.ToString());

            ip6NumLbl.Text = string.Format(PacketsFormat, ip6Num.ToString());
            ip6BytesLbl.Text = string.Format(BytesFormat, ip6Bytes.ToString());

            udpNumLbl.Text = string.Format(PacketsFormat, udpNum.ToString());
            udpBytesLbl.Text = string.Format(BytesFormat, udpBytes.ToString());

            tcpNumLbl.Text = string.Format(PacketsFormat, tcpNum.ToString());
            tcpBytesLbl.Text = string.Format(BytesFormat, tcpBytes.ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
