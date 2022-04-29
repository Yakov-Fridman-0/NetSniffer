
namespace NetSnifferApp
{
    partial class OpenCaptureDialog
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.captureFilter = new NetSnifferApp.CaptureFilter();
            this.packetNumberUpDown = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chooseFileButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.packetNumberUpDown)).BeginInit();
            flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(99, 39);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 20);
            label1.TabIndex = 0;
            label1.Text = "File:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(149, 191);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(136, 20);
            label2.TabIndex = 2;
            label2.Text = "Number of Packets:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(38, 113);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(101, 20);
            label3.TabIndex = 3;
            label3.Text = "Capture Filter:";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(138, 35);
            this.fileNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.ReadOnly = true;
            this.fileNameTextBox.Size = new System.Drawing.Size(305, 27);
            this.fileNameTextBox.TabIndex = 1;
            // 
            // captureFilter
            // 
            this.captureFilter.IsValidFilter = true;
            this.captureFilter.Location = new System.Drawing.Point(138, 99);
            this.captureFilter.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.captureFilter.Name = "captureFilter";
            this.captureFilter.Size = new System.Drawing.Size(422, 49);
            this.captureFilter.TabIndex = 4;
            this.captureFilter.FilterChanged += new System.EventHandler(this.captureFilter_FilterChanged);
            // 
            // packetNumberUpDown
            // 
            this.packetNumberUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.packetNumberUpDown.Location = new System.Drawing.Point(282, 185);
            this.packetNumberUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.packetNumberUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.packetNumberUpDown.Name = "packetNumberUpDown";
            this.packetNumberUpDown.Size = new System.Drawing.Size(137, 27);
            this.packetNumberUpDown.TabIndex = 5;
            this.packetNumberUpDown.ValueChanged += new System.EventHandler(this.packetNumberUpDown_ValueChanged);
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
            this.okButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(95, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 31);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(this.okButton);
            flowLayoutPanel.Controls.Add(this.button2);
            flowLayoutPanel.Location = new System.Drawing.Point(211, 252);
            flowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new System.Drawing.Size(192, 47);
            flowLayoutPanel.TabIndex = 8;
            // 
            // chooseFileButton
            // 
            this.chooseFileButton.Location = new System.Drawing.Point(459, 33);
            this.chooseFileButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chooseFileButton.Name = "chooseFileButton";
            this.chooseFileButton.Size = new System.Drawing.Size(101, 31);
            this.chooseFileButton.TabIndex = 9;
            this.chooseFileButton.Text = "Choose File";
            this.chooseFileButton.UseVisualStyleBackColor = true;
            this.chooseFileButton.Click += new System.EventHandler(this.chooseFileButton_Click);
            // 
            // OpenCaptureDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 332);
            this.Controls.Add(this.chooseFileButton);
            this.Controls.Add(flowLayoutPanel);
            this.Controls.Add(this.packetNumberUpDown);
            this.Controls.Add(this.captureFilter);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OpenCaptureDialog";
            this.Text = "Open Capture";
            ((System.ComponentModel.ISupportInitialize)(this.packetNumberUpDown)).EndInit();
            flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CaptureFilter captureFilter;
        private System.Windows.Forms.NumericUpDown packetNumberUpDown;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button chooseFileButton;
    }
}