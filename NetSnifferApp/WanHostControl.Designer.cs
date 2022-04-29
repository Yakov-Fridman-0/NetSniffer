
namespace NetSnifferApp
{
    partial class WanHostControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WanHostControl));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tracertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTCPConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::NetSnifferApp.Properties.Resources.Host;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(47, 41);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Location = new System.Drawing.Point(53, 10);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(114, 20);
            this.ipAddressLabel.TabIndex = 1;
            this.ipAddressLabel.Text = "000.000.000.000";
            this.ipAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ipAddressLabel.Visible = false;
            this.ipAddressLabel.TextChanged += new System.EventHandler(this.ipAddressLabel_TextChanged);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Host");
            this.imageList.Images.SetKeyName(1, "Router");
            this.imageList.Images.SetKeyName(2, "Server");
            this.imageList.Images.SetKeyName(3, "RouterAndServer");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tracertToolStripMenuItem,
            this.showTCPConnectionsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(226, 80);
            // 
            // tracertToolStripMenuItem
            // 
            this.tracertToolStripMenuItem.Name = "tracertToolStripMenuItem";
            this.tracertToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.tracertToolStripMenuItem.Text = "tracert";
            this.tracertToolStripMenuItem.Click += new System.EventHandler(this.tracertToolStripMenuItem_Click);
            // 
            // showTCPConnectionsToolStripMenuItem
            // 
            this.showTCPConnectionsToolStripMenuItem.Name = "showTCPConnectionsToolStripMenuItem";
            this.showTCPConnectionsToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.showTCPConnectionsToolStripMenuItem.Text = "Show TCP connections";
            this.showTCPConnectionsToolStripMenuItem.Click += new System.EventHandler(this.showTCPConnectionsToolStripMenuItem_Click);
            // 
            // WanHostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.ipAddressLabel);
            this.Controls.Add(this.pictureBox);
            this.Name = "WanHostControl";
            this.Size = new System.Drawing.Size(175, 45);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tracertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTCPConnectionsToolStripMenuItem;
    }
}
