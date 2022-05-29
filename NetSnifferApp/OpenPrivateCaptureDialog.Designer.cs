
namespace NetSnifferApp
{
    partial class OpenPrivateCaptureDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.packetNumberUpDown = new System.Windows.Forms.NumericUpDown();
            this.captureFilter = new NetSnifferApp.CaptureFilter();
            this.filesComboBox = new System.Windows.Forms.ComboBox();
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            flowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetNumberUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(this.okButton);
            flowLayoutPanel.Controls.Add(this.cancelButton);
            flowLayoutPanel.Location = new System.Drawing.Point(221, 252);
            flowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new System.Drawing.Size(192, 47);
            flowLayoutPanel.TabIndex = 16;
            // 
            // okButton
            // 
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(3, 4);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(86, 31);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(95, 4);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 31);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(48, 113);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(101, 20);
            label3.TabIndex = 13;
            label3.Text = "Capture Filter:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(159, 191);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(136, 20);
            label2.TabIndex = 12;
            label2.Text = "Number of Packets:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(109, 39);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 20);
            label1.TabIndex = 10;
            label1.Text = "File:";
            // 
            // packetNumberUpDown
            // 
            this.packetNumberUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.packetNumberUpDown.Location = new System.Drawing.Point(292, 185);
            this.packetNumberUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.packetNumberUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.packetNumberUpDown.Name = "packetNumberUpDown";
            this.packetNumberUpDown.Size = new System.Drawing.Size(137, 27);
            this.packetNumberUpDown.TabIndex = 15;
            this.packetNumberUpDown.ValueChanged += new System.EventHandler(this.PacketNumberUpDown_ValueChanged_1);
            // 
            // captureFilter
            // 
            this.captureFilter.IsValidFilter = true;
            this.captureFilter.Location = new System.Drawing.Point(148, 99);
            this.captureFilter.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.captureFilter.Name = "captureFilter";
            this.captureFilter.Size = new System.Drawing.Size(422, 49);
            this.captureFilter.TabIndex = 14;
            this.captureFilter.FilterChanged += new System.EventHandler(this.CaptureFilter_FilterChanged);
            // 
            // filesComboBox
            // 
            this.filesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filesComboBox.FormattingEnabled = true;
            this.filesComboBox.Location = new System.Drawing.Point(151, 39);
            this.filesComboBox.Name = "filesComboBox";
            this.filesComboBox.Size = new System.Drawing.Size(409, 28);
            this.filesComboBox.TabIndex = 17;
            this.filesComboBox.SelectedIndexChanged += new System.EventHandler(this.FilesComboBox_SelectedIndexChanged);
            // 
            // OpenPrivateCaptureDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 332);
            this.Controls.Add(this.filesComboBox);
            this.Controls.Add(flowLayoutPanel);
            this.Controls.Add(this.packetNumberUpDown);
            this.Controls.Add(this.captureFilter);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Name = "OpenPrivateCaptureDialog";
            this.Text = "Open Private Capture";
            flowLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.packetNumberUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown packetNumberUpDown;
        private CaptureFilter captureFilter;
        private System.Windows.Forms.ComboBox filesComboBox;
    }
}