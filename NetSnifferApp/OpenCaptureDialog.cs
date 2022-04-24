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
    public partial class OpenCaptureDialog : Form
    {
        public string FileName { get; private set; }

        public string CaptureFilterString { get; private set; }

        public int NumberOfPackets { get; private set; }

        bool isValidFilter = true;
        bool isFileChosen = false;

        bool openFile = true;

        public OpenCaptureDialog()
        {
            InitializeComponent();

            okButton.Enabled = false;
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

        private void packetNumberUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumberOfPackets = (int)packetNumberUpDown.Value;
        }

        private void captureFilter_FilterChanged(object sender, string e)
        {
            string filter = e;

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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            openFile = false;
            Close();
        }


        private void openButton_Click(object sender, EventArgs e)
        {
            openFile = true;
            Close();
        }

        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                FileName = "Select a file",
                Filter = "Pcap files (*.pcap)|*.pcap",
                Title = "Open pcap file",
            };

            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;
            FileName = openFileDialog.FileName;

            fileNameTextBox.Text = FileName;

            isFileChosen = true;
            okButton.Enabled = isValidFilter;
        }
    }
}
