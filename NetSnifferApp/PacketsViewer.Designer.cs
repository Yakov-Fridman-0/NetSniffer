﻿
namespace NetSnifferApp
{
    partial class PacketViewer
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.packetPage = new System.Windows.Forms.TabPage();
            this.binaryDataTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.attackPage = new System.Windows.Forms.TabPage();
            this.attackTextBox = new System.Windows.Forms.TextBox();
            this.attacksComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.packetsListView = new System.Windows.Forms.ListView();
            this.Index = new System.Windows.Forms.ColumnHeader();
            this.Time = new System.Windows.Forms.ColumnHeader();
            this.Protocol = new System.Windows.Forms.ColumnHeader();
            this.Source = new System.Windows.Forms.ColumnHeader();
            this.Destination = new System.Windows.Forms.ColumnHeader();
            this.PayloadLength = new System.Windows.Forms.ColumnHeader();
            this.Description = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl.SuspendLayout();
            this.packetPage.SuspendLayout();
            this.attackPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.packetPage);
            this.tabControl.Controls.Add(this.attackPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(634, 129);
            this.tabControl.TabIndex = 2;
            // 
            // packetPage
            // 
            this.packetPage.Controls.Add(this.binaryDataTextBox);
            this.packetPage.Controls.Add(this.label1);
            this.packetPage.Location = new System.Drawing.Point(4, 24);
            this.packetPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.packetPage.Name = "packetPage";
            this.packetPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.packetPage.Size = new System.Drawing.Size(626, 101);
            this.packetPage.TabIndex = 0;
            this.packetPage.Text = "Packet";
            this.packetPage.UseVisualStyleBackColor = true;
            // 
            // binaryDataTextBox
            // 
            this.binaryDataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.binaryDataTextBox.Location = new System.Drawing.Point(3, 22);
            this.binaryDataTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.binaryDataTextBox.Multiline = true;
            this.binaryDataTextBox.Name = "binaryDataTextBox";
            this.binaryDataTextBox.ReadOnly = true;
            this.binaryDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.binaryDataTextBox.Size = new System.Drawing.Size(568, 68);
            this.binaryDataTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Binary Data:";
            // 
            // attackPage
            // 
            this.attackPage.Controls.Add(this.attackTextBox);
            this.attackPage.Controls.Add(this.attacksComboBox);
            this.attackPage.Controls.Add(this.label2);
            this.attackPage.Location = new System.Drawing.Point(4, 24);
            this.attackPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.attackPage.Name = "attackPage";
            this.attackPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.attackPage.Size = new System.Drawing.Size(626, 101);
            this.attackPage.TabIndex = 1;
            this.attackPage.Text = "Attacks";
            this.attackPage.UseVisualStyleBackColor = true;
            // 
            // attackTextBox
            // 
            this.attackTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attackTextBox.Location = new System.Drawing.Point(7, 30);
            this.attackTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.attackTextBox.Multiline = true;
            this.attackTextBox.Name = "attackTextBox";
            this.attackTextBox.ReadOnly = true;
            this.attackTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.attackTextBox.Size = new System.Drawing.Size(616, 70);
            this.attackTextBox.TabIndex = 2;
            // 
            // attacksComboBox
            // 
            this.attacksComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attacksComboBox.FormattingEnabled = true;
            this.attacksComboBox.Location = new System.Drawing.Point(131, 4);
            this.attacksComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.attacksComboBox.Name = "attacksComboBox";
            this.attacksComboBox.Size = new System.Drawing.Size(133, 23);
            this.attacksComboBox.TabIndex = 1;
            this.attacksComboBox.SelectedIndexChanged += new System.EventHandler(this.attacksComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Associated Attacks:";
            // 
            // packetsListView
            // 
            this.packetsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.Time,
            this.Protocol,
            this.Source,
            this.Destination,
            this.PayloadLength,
            this.Description});
            this.packetsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetsListView.FullRowSelect = true;
            this.packetsListView.GridLines = true;
            this.packetsListView.HideSelection = false;
            this.packetsListView.Location = new System.Drawing.Point(0, 0);
            this.packetsListView.Name = "packetsListView";
            this.packetsListView.ShowGroups = false;
            this.packetsListView.Size = new System.Drawing.Size(634, 308);
            this.packetsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.packetsListView.TabIndex = 0;
            this.packetsListView.UseCompatibleStateImageBehavior = false;
            this.packetsListView.View = System.Windows.Forms.View.Details;
            this.packetsListView.SelectedIndexChanged += new System.EventHandler(this.packetsListView_SelectedIndexChanged);
            // 
            // Index
            // 
            this.Index.Text = "Index";
            this.Index.Width = 80;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 45;
            // 
            // Protocol
            // 
            this.Protocol.Text = "Protocol";
            this.Protocol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Protocol.Width = 80;
            // 
            // Source
            // 
            this.Source.Text = "Source";
            this.Source.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Source.Width = 57;
            // 
            // Destination
            // 
            this.Destination.Text = "Destination";
            this.Destination.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Destination.Width = 100;
            // 
            // PayloadLength
            // 
            this.PayloadLength.Text = "Payload Length";
            this.PayloadLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PayloadLength.Width = 114;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 150;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.packetsListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(634, 440);
            this.splitContainer1.SplitterDistance = 308;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // PacketViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PacketViewer";
            this.Size = new System.Drawing.Size(634, 440);
            this.tabControl.ResumeLayout(false);
            this.packetPage.ResumeLayout(false);
            this.packetPage.PerformLayout();
            this.attackPage.ResumeLayout(false);
            this.attackPage.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage packetPage;
        private System.Windows.Forms.TextBox binaryDataTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage attackPage;
        private System.Windows.Forms.TextBox attackTextBox;
        private System.Windows.Forms.ComboBox attacksComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView packetsListView;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Protocol;
        private System.Windows.Forms.ColumnHeader Source;
        private System.Windows.Forms.ColumnHeader Destination;
        private System.Windows.Forms.ColumnHeader PayloadLength;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
