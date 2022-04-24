
using System;

namespace NetSnifferApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblNetInterface = new System.Windows.Forms.Label();
            this.CmbNetInterface = new System.Windows.Forms.ComboBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tcpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetFilter = new NetSnifferApp.CaptureFilter();
            this.label1 = new System.Windows.Forms.Label();
            this.packetViewerControl = new NetSnifferApp.PacketViewer();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblNetInterface
            // 
            this.LblNetInterface.AutoSize = true;
            this.LblNetInterface.Location = new System.Drawing.Point(13, 33);
            this.LblNetInterface.Name = "LblNetInterface";
            this.LblNetInterface.Size = new System.Drawing.Size(142, 15);
            this.LblNetInterface.TabIndex = 0;
            this.LblNetInterface.Text = "Choose network interface";
            // 
            // CmbNetInterface
            // 
            this.CmbNetInterface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbNetInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbNetInterface.FormattingEnabled = true;
            this.CmbNetInterface.Location = new System.Drawing.Point(13, 51);
            this.CmbNetInterface.Name = "CmbNetInterface";
            this.CmbNetInterface.Size = new System.Drawing.Size(775, 23);
            this.CmbNetInterface.TabIndex = 1;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(13, 81);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 2;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(94, 81);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 3;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.statisticsToolStripMenuItem,
            this.topologyToolStripMenuItem,
            this.tcpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalToolStripMenuItem});
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            // 
            // generalToolStripMenuItem
            // 
            this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
            this.generalToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.generalToolStripMenuItem.Text = "General";
            this.generalToolStripMenuItem.Click += new System.EventHandler(this.GeneralToolStripMenuItem_Click);
            // 
            // topologyToolStripMenuItem
            // 
            this.topologyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalToolStripMenuItem1});
            this.topologyToolStripMenuItem.Name = "topologyToolStripMenuItem";
            this.topologyToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.topologyToolStripMenuItem.Text = "Topology";
            // 
            // generalToolStripMenuItem1
            // 
            this.generalToolStripMenuItem1.Name = "generalToolStripMenuItem1";
            this.generalToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.generalToolStripMenuItem1.Text = "General";
            this.generalToolStripMenuItem1.Click += new System.EventHandler(this.generalToolStripMenuItem1_Click);
            // 
            // tcpToolStripMenuItem
            // 
            this.tcpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.streamsToolStripMenuItem});
            this.tcpToolStripMenuItem.Name = "tcpToolStripMenuItem";
            this.tcpToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.tcpToolStripMenuItem.Text = "TCP";
            // 
            // streamsToolStripMenuItem
            // 
            this.streamsToolStripMenuItem.Name = "streamsToolStripMenuItem";
            this.streamsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.streamsToolStripMenuItem.Text = "TCP Streams";
            this.streamsToolStripMenuItem.Click += new System.EventHandler(this.streamsToolStripMenuItem_Click);
            // 
            // packetFilter
            // 
            this.packetFilter.Location = new System.Drawing.Point(13, 136);
            this.packetFilter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.packetFilter.Name = "packetFilter";
            this.packetFilter.Size = new System.Drawing.Size(774, 37);
            this.packetFilter.TabIndex = 8;
            this.packetFilter.IsValidFilter = true;
            this.packetFilter.FilterChanged += new System.EventHandler<string>(this.PacketFilter_FilterChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Choose capture filter:";
            // 
            // packetViewerControl
            // 
            this.packetViewerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packetViewerControl.Location = new System.Drawing.Point(12, 192);
            this.packetViewerControl.Name = "packetViewerControl";
            this.packetViewerControl.Size = new System.Drawing.Size(775, 432);
            this.packetViewerControl.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 636);
            this.Controls.Add(this.packetViewerControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.packetFilter);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.CmbNetInterface);
            this.Controls.Add(this.LblNetInterface);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Sniffer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label LblNetInterface;
        private System.Windows.Forms.ComboBox CmbNetInterface;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnStop;
        private PacketViewer CtrlPacketViewer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalToolStripMenuItem;
        private CaptureFilter packetFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem topologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tcpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamsToolStripMenuItem;
        private PacketViewer packetViewerControl;
    }
}

