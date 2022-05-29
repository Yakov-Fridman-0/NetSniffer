﻿
namespace NetSnifferApp
{
    partial class SimpleLanHostControl
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
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.physicalAddressLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyMACAdrressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyIPAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTCPConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Location = new System.Drawing.Point(5, 118);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(114, 20);
            this.ipAddressLabel.TabIndex = 4;
            this.ipAddressLabel.Text = "000.000.000.000";
            this.ipAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ipAddressLabel.Visible = false;
            // 
            // physicalAddressLabel
            // 
            this.physicalAddressLabel.AutoSize = true;
            this.physicalAddressLabel.Location = new System.Drawing.Point(5, 98);
            this.physicalAddressLabel.Name = "physicalAddressLabel";
            this.physicalAddressLabel.Size = new System.Drawing.Size(120, 20);
            this.physicalAddressLabel.TabIndex = 3;
            this.physicalAddressLabel.Text = "00:00:00:00:00:00";
            this.physicalAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMACAdrressToolStripMenuItem,
            this.copyIPAddressToolStripMenuItem,
            this.showTCPConnectionsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(226, 76);
            // 
            // copyMACAdrressToolStripMenuItem
            // 
            this.copyMACAdrressToolStripMenuItem.Name = "copyMACAdrressToolStripMenuItem";
            this.copyMACAdrressToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.copyMACAdrressToolStripMenuItem.Text = "Copy MAC adrress";
            // 
            // copyIPAddressToolStripMenuItem
            // 
            this.copyIPAddressToolStripMenuItem.Name = "copyIPAddressToolStripMenuItem";
            this.copyIPAddressToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.copyIPAddressToolStripMenuItem.Text = "Copy IP address";
            this.copyIPAddressToolStripMenuItem.Visible = false;
            // 
            // showTCPConnectionsToolStripMenuItem
            // 
            this.showTCPConnectionsToolStripMenuItem.Name = "showTCPConnectionsToolStripMenuItem";
            this.showTCPConnectionsToolStripMenuItem.Size = new System.Drawing.Size(225, 24);
            this.showTCPConnectionsToolStripMenuItem.Text = "Show TCP connections";
            this.showTCPConnectionsToolStripMenuItem.Visible = false;
            // 
            // SimpleLanHostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.ipAddressLabel);
            this.Controls.Add(this.physicalAddressLabel);
            this.Name = "SimpleLanHostControl";
            this.Size = new System.Drawing.Size(128, 143);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.Label physicalAddressLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyMACAdrressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyIPAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTCPConnectionsToolStripMenuItem;
    }
}