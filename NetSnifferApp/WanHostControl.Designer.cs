
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
            System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WanHostControl));
            this.tracertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showTCPConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyIPAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.addressToolTip = new System.Windows.Forms.ToolTip(this.components);
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tracertToolStripMenuItem,
            this.toolStripSeparator1,
            this.showTCPConnectionsToolStripMenuItem,
            this.toolStripSeparator2,
            this.copyIPAddressToolStripMenuItem,
            this.toolStripSeparator3,
            this.selectToolStripMenuItem});
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(226, 146);
            // 
            // tracertToolStripMenuItem
            // 
            this.tracertToolStripMenuItem.Name = "tracertToolStripMenuItem";
            this.tracertToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.tracertToolStripMenuItem.Text = "tracert";
            this.tracertToolStripMenuItem.Click += new System.EventHandler(this.TracertToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
            // 
            // showTCPConnectionsToolStripMenuItem
            // 
            this.showTCPConnectionsToolStripMenuItem.Name = "showTCPConnectionsToolStripMenuItem";
            this.showTCPConnectionsToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.showTCPConnectionsToolStripMenuItem.Text = "Show TCP connections";
            this.showTCPConnectionsToolStripMenuItem.Click += new System.EventHandler(this.ShowTCPConnectionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(222, 6);
            // 
            // copyIPAddressToolStripMenuItem
            // 
            this.copyIPAddressToolStripMenuItem.Name = "copyIPAddressToolStripMenuItem";
            this.copyIPAddressToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.copyIPAddressToolStripMenuItem.Text = "Copy IP Address";
            this.copyIPAddressToolStripMenuItem.Click += new System.EventHandler(this.CopyIPAddressToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.SelectToolStripMenuItem_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::NetSnifferApp.Properties.Resources.Host;
            this.pictureBox.Location = new System.Drawing.Point(7, 6);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(47, 41);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
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
            // WanHostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ContextMenuStrip = contextMenuStrip1;
            this.Controls.Add(this.pictureBox);
            this.Name = "WanHostControl";
            this.Size = new System.Drawing.Size(60, 52);
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tracertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTCPConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolTip addressToolTip;
        private System.Windows.Forms.ToolStripMenuItem copyIPAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
