
namespace NetSnifferApp
{
    partial class ConnectionsForm
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
            this.components = new System.ComponentModel.Container();
            this.hostAddressLabel = new System.Windows.Forms.Label();
            this.connectionsListBox = new System.Windows.Forms.ListBox();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // hostAddressLabel
            // 
            this.hostAddressLabel.AutoSize = true;
            this.hostAddressLabel.Location = new System.Drawing.Point(13, 13);
            this.hostAddressLabel.Name = "hostAddressLabel";
            this.hostAddressLabel.Size = new System.Drawing.Size(158, 20);
            this.hostAddressLabel.TabIndex = 0;
            this.hostAddressLabel.Text = "0.0.0.0 is connected to:";
            // 
            // connectionsListBox
            // 
            this.connectionsListBox.FormattingEnabled = true;
            this.connectionsListBox.ItemHeight = 20;
            this.connectionsListBox.Location = new System.Drawing.Point(13, 45);
            this.connectionsListBox.Name = "connectionsListBox";
            this.connectionsListBox.Size = new System.Drawing.Size(291, 344);
            this.connectionsListBox.TabIndex = 1;
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // ConnectionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 416);
            this.Controls.Add(this.connectionsListBox);
            this.Controls.Add(this.hostAddressLabel);
            this.Name = "ConnectionsForm";
            this.Text = "Connections";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionsForm_FormClosing);
            this.Load += new System.EventHandler(this.ConnectionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hostAddressLabel;
        private System.Windows.Forms.ListBox connectionsListBox;
        private System.Windows.Forms.Timer updateTimer;
    }
}