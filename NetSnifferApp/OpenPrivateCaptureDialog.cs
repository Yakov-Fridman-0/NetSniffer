using System;
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
    public partial class OpenPrivateCaptureDialog : Form
    {
        public string Username { get; init; }

        public string FileName { get; private set; }

        public string CaptureFilterString { get; private set; }

        public int NumberOfPackets { get; private set; }

        bool isValidFilter = true;
        bool isFileChosen = false;

        bool openFile = true;

        public OpenPrivateCaptureDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            okButton.Enabled = false;

            filesComboBox.Items.AddRange(AccountManager.GetUserFiles(Username));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (openFile)
            {
                if (isFileChosen && isValidFilter)
                {
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }

            base.OnFormClosing(e);
        }

        private void PacketNumberUpDown_ValueChanged_1(object sender, EventArgs e)
        {
            NumberOfPackets = (int)packetNumberUpDown.Value;
        }

        private void CaptureFilter_FilterChanged(object sender, EventArgs e)
        {
            string filter = captureFilter.Filter;

            if (NetSniffer.IsValidCaptureFilter(filter))
            {
                CaptureFilterString = filter;
                captureFilter.IsValidFilter = true;

                isValidFilter = true;
                okButton.Enabled = isFileChosen;
            }
            else
            {
                okButton.Enabled = false;
                captureFilter.IsValidFilter = false;

                isValidFilter = false;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            openFile = true;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            openFile = false;
            Close();
        }

        private void FilesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filename = (string)filesComboBox.SelectedItem;

            if (filename == null)
            {
                isFileChosen = false;
                okButton.Enabled = false;
            }
            else
            {
                isFileChosen = true;
                FileName = filename;

                okButton.Enabled = isValidFilter;
            }
        }
    }
}
