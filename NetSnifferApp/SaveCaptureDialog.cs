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
    public partial class SaveCaptureDialog : Form
    {
        public string FileName { get; private set; }

        public string DispalyFilterString { get; private set; } = string.Empty;

        bool isValidFilter = true;
        bool isFileChosen = false;

        bool saveFile = false;

        public SaveCaptureDialog()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (saveFile)
            {
                DialogResult = DialogResult.OK;
            } 
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            base.OnFormClosing(e);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            saveFile = true;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            saveFile = false;
            Close();
        }

        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                FileName = "Select a file",
                Filter = "Pcap files (*.pcap)|*.pcap",
                Title = "Save pcap file",
            };

            var result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            FileName = saveFileDialog.FileName;

            fileNameTextBox.Text = FileName;

            isFileChosen = true;
            okButton.Enabled = isValidFilter;
        }

        private void displayFilterContorl_FilterChanged(object sender, string e)
        {
            DisplayFilter displayFilter = null;

            if (DisplayFilter.TryParse(displayFilterContorl.Filter, ref displayFilter))
            {
                DispalyFilterString = displayFilterContorl.Filter;
                displayFilterContorl.IsValidFilter = true;

                isValidFilter = true;
                okButton.Enabled = isFileChosen;
            }
            else
            {
                displayFilterContorl.IsValidFilter = false;
                okButton.Enabled = false;
            }
        }
    }
}
