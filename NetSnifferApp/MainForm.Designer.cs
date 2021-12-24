﻿
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
            this.CtrlPacketViewer = new NetSnifferApp.PacketViewer();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblNetInterface
            // 
            this.LblNetInterface.AutoSize = true;
            this.LblNetInterface.Location = new System.Drawing.Point(13, 13);
            this.LblNetInterface.Name = "LblNetInterface";
            this.LblNetInterface.Size = new System.Drawing.Size(138, 15);
            this.LblNetInterface.TabIndex = 0;
            this.LblNetInterface.Text = "Choise network interface";
            // 
            // CmbNetInterface
            // 
            this.CmbNetInterface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbNetInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbNetInterface.FormattingEnabled = true;
            this.CmbNetInterface.Location = new System.Drawing.Point(13, 32);
            this.CmbNetInterface.Name = "CmbNetInterface";
            this.CmbNetInterface.Size = new System.Drawing.Size(775, 23);
            this.CmbNetInterface.TabIndex = 1;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(13, 62);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 2;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(94, 62);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 3;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // CtrlPacketViewer
            // 
            this.CtrlPacketViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CtrlPacketViewer.Location = new System.Drawing.Point(12, 92);
            this.CtrlPacketViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CtrlPacketViewer.Name = "CtrlPacketViewer";
            this.CtrlPacketViewer.Size = new System.Drawing.Size(775, 347);
            this.CtrlPacketViewer.TabIndex = 4;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(174, 62);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(82, 22);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BtnOpen
            // 
            this.BtnOpen.Location = new System.Drawing.Point(262, 62);
            this.BtnOpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(82, 22);
            this.BtnOpen.TabIndex = 6;
            this.BtnOpen.Text = "Open";
            this.BtnOpen.UseVisualStyleBackColor = true;
            this.BtnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnOpen);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.CtrlPacketViewer);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.CmbNetInterface);
            this.Controls.Add(this.LblNetInterface);
            this.Name = "MainForm";
            this.Text = "Sniffer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label LblNetInterface;
        private System.Windows.Forms.ComboBox CmbNetInterface;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnStop;
        private PacketViewer CtrlPacketViewer;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnOpen;
    }
}
