
namespace NetSnifferApp
{
    partial class SavePrivateCaptureDialog
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.displayFilterContorl = new NetSnifferApp.DisplayFilterControl();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(51, 66);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 20);
            label2.TabIndex = 17;
            label2.Text = "File:";
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(this.okButton);
            flowLayoutPanel.Controls.Add(this.cancelButton);
            flowLayoutPanel.Location = new System.Drawing.Point(187, 225);
            flowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new System.Drawing.Size(192, 47);
            flowLayoutPanel.TabIndex = 15;
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
            // displayFilterContorl
            // 
            this.displayFilterContorl.Filter = "";
            this.displayFilterContorl.IsValidFilter = true;
            this.displayFilterContorl.Location = new System.Drawing.Point(156, 107);
            this.displayFilterContorl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.displayFilterContorl.MaximumSize = new System.Drawing.Size(1000, 49);
            this.displayFilterContorl.MinimumSize = new System.Drawing.Size(0, 49);
            this.displayFilterContorl.Name = "displayFilterContorl";
            this.displayFilterContorl.Size = new System.Drawing.Size(323, 49);
            this.displayFilterContorl.TabIndex = 20;
            this.displayFilterContorl.FilterChanged += new System.EventHandler<string>(this.DisplayFilterContorl_FilterChanged);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(156, 63);
            this.fileNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(323, 27);
            this.fileNameTextBox.TabIndex = 18;
            this.fileNameTextBox.TextChanged += new System.EventHandler(this.fileNameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Display Filter:";
            // 
            // SavePrivateCaptureDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 332);
            this.Controls.Add(this.displayFilterContorl);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(flowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "SavePrivateCaptureDialog";
            this.Text = "Save Private Capture";
            flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DisplayFilterControl displayFilterContorl;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}