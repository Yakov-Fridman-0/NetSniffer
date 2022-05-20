
namespace NetSnifferApp
{
    partial class ImprovedMainForm
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
            System.Windows.Forms.Panel panel1;
            this.moreInfoLabel = new System.Windows.Forms.Label();
            this.interfaceNameTextBox = new System.Windows.Forms.TextBox();
            this.interfaceTitleLabel = new System.Windows.Forms.Label();
            this.captureFilterTitleLabel = new System.Windows.Forms.Label();
            this.captureFilterTextBox = new System.Windows.Forms.TextBox();
            this.controlFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.stopButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalTopologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.capturePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.packetViewer = new NetSnifferApp.PacketViewer();
            this.startPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.packetNumberUpDown = new System.Windows.Forms.NumericUpDown();
            this.promiscuousCheckBox = new System.Windows.Forms.CheckBox();
            this.interfaceInfo = new NetSnifferApp.InterfaceInfo();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.interfaceComboBox = new System.Windows.Forms.ComboBox();
            this.captureFilter = new NetSnifferApp.CaptureFilter();
            this.startButton = new System.Windows.Forms.Button();
            this.utilitisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arpTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.routingTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ipconfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            this.controlFlowLayoutPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.capturePanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.startPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetNumberUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(this.moreInfoLabel);
            panel1.Controls.Add(this.interfaceNameTextBox);
            panel1.Controls.Add(this.interfaceTitleLabel);
            panel1.Controls.Add(this.captureFilterTitleLabel);
            panel1.Controls.Add(this.captureFilterTextBox);
            panel1.Controls.Add(this.controlFlowLayoutPanel);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 4);
            panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1010, 78);
            panel1.TabIndex = 0;
            // 
            // moreInfoLabel
            // 
            this.moreInfoLabel.AutoSize = true;
            this.moreInfoLabel.Location = new System.Drawing.Point(432, 15);
            this.moreInfoLabel.Name = "moreInfoLabel";
            this.moreInfoLabel.Size = new System.Drawing.Size(165, 20);
            this.moreInfoLabel.TabIndex = 9;
            this.moreInfoLabel.Text = "Capture form dump file";
            // 
            // interfaceNameTextBox
            // 
            this.interfaceNameTextBox.Location = new System.Drawing.Point(175, 11);
            this.interfaceNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.interfaceNameTextBox.Name = "interfaceNameTextBox";
            this.interfaceNameTextBox.ReadOnly = true;
            this.interfaceNameTextBox.Size = new System.Drawing.Size(139, 27);
            this.interfaceNameTextBox.TabIndex = 8;
            // 
            // interfaceTitleLabel
            // 
            this.interfaceTitleLabel.AutoSize = true;
            this.interfaceTitleLabel.Location = new System.Drawing.Point(10, 16);
            this.interfaceTitleLabel.Name = "interfaceTitleLabel";
            this.interfaceTitleLabel.Size = new System.Drawing.Size(131, 20);
            this.interfaceTitleLabel.TabIndex = 7;
            this.interfaceTitleLabel.Text = "Selected Interface:";
            // 
            // captureFilterTitleLabel
            // 
            this.captureFilterTitleLabel.AutoSize = true;
            this.captureFilterTitleLabel.Location = new System.Drawing.Point(10, 53);
            this.captureFilterTitleLabel.Name = "captureFilterTitleLabel";
            this.captureFilterTitleLabel.Size = new System.Drawing.Size(162, 20);
            this.captureFilterTitleLabel.TabIndex = 2;
            this.captureFilterTitleLabel.Text = "Selected Capture Filter:";
            // 
            // captureFilterTextBox
            // 
            this.captureFilterTextBox.Location = new System.Drawing.Point(175, 48);
            this.captureFilterTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.captureFilterTextBox.Name = "captureFilterTextBox";
            this.captureFilterTextBox.ReadOnly = true;
            this.captureFilterTextBox.Size = new System.Drawing.Size(139, 27);
            this.captureFilterTextBox.TabIndex = 3;
            // 
            // controlFlowLayoutPanel
            // 
            this.controlFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlFlowLayoutPanel.Controls.Add(this.stopButton);
            this.controlFlowLayoutPanel.Controls.Add(this.restartButton);
            this.controlFlowLayoutPanel.Location = new System.Drawing.Point(794, 16);
            this.controlFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.controlFlowLayoutPanel.Name = "controlFlowLayoutPanel";
            this.controlFlowLayoutPanel.Size = new System.Drawing.Size(187, 43);
            this.controlFlowLayoutPanel.TabIndex = 5;
            // 
            // stopButton
            // 
            this.stopButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.stopButton.Location = new System.Drawing.Point(3, 4);
            this.stopButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(86, 31);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(95, 4);
            this.restartButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(86, 31);
            this.restartButton.TabIndex = 4;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.statisticsToolStripMenuItem,
            this.topologyToolStripMenuItem,
            this.attackToolStripMenuItem,
            this.utilitisToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1016, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.newCaptureToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.openToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(178, 26);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // newCaptureToolStripMenuItem
            // 
            this.newCaptureToolStripMenuItem.Enabled = false;
            this.newCaptureToolStripMenuItem.Name = "newCaptureToolStripMenuItem";
            this.newCaptureToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.newCaptureToolStripMenuItem.Text = "New Capture";
            this.newCaptureToolStripMenuItem.Click += new System.EventHandler(this.newCaptureToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalStatisticsToolStripMenuItem});
            this.statisticsToolStripMenuItem.Enabled = false;
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            // 
            // generalStatisticsToolStripMenuItem
            // 
            this.generalStatisticsToolStripMenuItem.Name = "generalStatisticsToolStripMenuItem";
            this.generalStatisticsToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.generalStatisticsToolStripMenuItem.Text = "General Statistics";
            this.generalStatisticsToolStripMenuItem.Click += new System.EventHandler(this.generalStatisticsToolStripMenuItem_Click);
            // 
            // topologyToolStripMenuItem
            // 
            this.topologyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalTopologyToolStripMenuItem});
            this.topologyToolStripMenuItem.Enabled = false;
            this.topologyToolStripMenuItem.Name = "topologyToolStripMenuItem";
            this.topologyToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.topologyToolStripMenuItem.Text = "Topology";
            // 
            // generalTopologyToolStripMenuItem
            // 
            this.generalTopologyToolStripMenuItem.Name = "generalTopologyToolStripMenuItem";
            this.generalTopologyToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.generalTopologyToolStripMenuItem.Text = "General Topology";
            this.generalTopologyToolStripMenuItem.Click += new System.EventHandler(this.generalTopologyToolStripMenuItem_Click);
            // 
            // attackToolStripMenuItem
            // 
            this.attackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem});
            this.attackToolStripMenuItem.Name = "attackToolStripMenuItem";
            this.attackToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.attackToolStripMenuItem.Text = "Attack";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(117, 26);
            this.logToolStripMenuItem.Text = "Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.capturePanel);
            this.mainPanel.Controls.Add(this.startPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 30);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1016, 852);
            this.mainPanel.TabIndex = 2;
            // 
            // capturePanel
            // 
            this.capturePanel.Controls.Add(this.tableLayoutPanel1);
            this.capturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capturePanel.Location = new System.Drawing.Point(0, 0);
            this.capturePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.capturePanel.Name = "capturePanel";
            this.capturePanel.Size = new System.Drawing.Size(1016, 852);
            this.capturePanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.13413F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.86588F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1016, 852);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.packetViewer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 90);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 758);
            this.panel2.TabIndex = 1;
            // 
            // packetViewer
            // 
            this.packetViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetViewer.Location = new System.Drawing.Point(0, 0);
            this.packetViewer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.packetViewer.Name = "packetViewer";
            this.packetViewer.Size = new System.Drawing.Size(1010, 758);
            this.packetViewer.TabIndex = 0;
            // 
            // startPanel
            // 
            this.startPanel.Controls.Add(this.label6);
            this.startPanel.Controls.Add(this.packetNumberUpDown);
            this.startPanel.Controls.Add(this.promiscuousCheckBox);
            this.startPanel.Controls.Add(this.interfaceInfo);
            this.startPanel.Controls.Add(this.label2);
            this.startPanel.Controls.Add(this.label1);
            this.startPanel.Controls.Add(this.interfaceComboBox);
            this.startPanel.Controls.Add(this.captureFilter);
            this.startPanel.Controls.Add(this.startButton);
            this.startPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPanel.Location = new System.Drawing.Point(0, 0);
            this.startPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startPanel.Name = "startPanel";
            this.startPanel.Size = new System.Drawing.Size(1016, 852);
            this.startPanel.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(303, 770);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Number of Packets:";
            // 
            // packetNumberUpDown
            // 
            this.packetNumberUpDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.packetNumberUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.packetNumberUpDown.Location = new System.Drawing.Point(445, 765);
            this.packetNumberUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.packetNumberUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.packetNumberUpDown.Name = "packetNumberUpDown";
            this.packetNumberUpDown.Size = new System.Drawing.Size(137, 27);
            this.packetNumberUpDown.TabIndex = 7;
            this.packetNumberUpDown.ValueChanged += new System.EventHandler(this.packetNumberUpDown_ValueChanged);
            // 
            // promiscuousCheckBox
            // 
            this.promiscuousCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.promiscuousCheckBox.AutoSize = true;
            this.promiscuousCheckBox.Checked = true;
            this.promiscuousCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.promiscuousCheckBox.Location = new System.Drawing.Point(622, 768);
            this.promiscuousCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.promiscuousCheckBox.Name = "promiscuousCheckBox";
            this.promiscuousCheckBox.Size = new System.Drawing.Size(114, 24);
            this.promiscuousCheckBox.TabIndex = 6;
            this.promiscuousCheckBox.Text = "Promiscuous";
            this.promiscuousCheckBox.UseVisualStyleBackColor = true;
            this.promiscuousCheckBox.CheckedChanged += new System.EventHandler(this.promiscuousCheckBox_CheckedChanged);
            // 
            // interfaceInfo
            // 
            this.interfaceInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.interfaceInfo.Interface = null;
            this.interfaceInfo.Location = new System.Drawing.Point(208, 134);
            this.interfaceInfo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.interfaceInfo.Name = "interfaceInfo";
            this.interfaceInfo.Size = new System.Drawing.Size(596, 623);
            this.interfaceInfo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Capture Filter:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(278, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Interface:";
            // 
            // interfaceComboBox
            // 
            this.interfaceComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.interfaceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.interfaceComboBox.FormattingEnabled = true;
            this.interfaceComboBox.Location = new System.Drawing.Point(399, 24);
            this.interfaceComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.interfaceComboBox.Name = "interfaceComboBox";
            this.interfaceComboBox.Size = new System.Drawing.Size(219, 28);
            this.interfaceComboBox.TabIndex = 0;
            this.interfaceComboBox.SelectedIndexChanged += new System.EventHandler(this.interfaceComboBox_SelectedIndexChanged);
            // 
            // captureFilter
            // 
            this.captureFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.captureFilter.IsValidFilter = true;
            this.captureFilter.Location = new System.Drawing.Point(399, 75);
            this.captureFilter.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.captureFilter.Name = "captureFilter";
            this.captureFilter.Size = new System.Drawing.Size(249, 49);
            this.captureFilter.TabIndex = 1;
            this.captureFilter.FilterChanged += new System.EventHandler(this.captureFilter_FilterChanged);
            // 
            // startButton
            // 
            this.startButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(460, 813);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(86, 31);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // utilitisToolStripMenuItem
            // 
            this.utilitisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arpTableToolStripMenuItem,
            this.routingTableToolStripMenuItem,
            this.ipconfigToolStripMenuItem});
            this.utilitisToolStripMenuItem.Name = "utilitisToolStripMenuItem";
            this.utilitisToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.utilitisToolStripMenuItem.Text = "Utilitis";
            // 
            // arpTableToolStripMenuItem
            // 
            this.arpTableToolStripMenuItem.Name = "arpTableToolStripMenuItem";
            this.arpTableToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.arpTableToolStripMenuItem.Text = "CAM Table";
            this.arpTableToolStripMenuItem.Click += new System.EventHandler(this.ArpTableToolStripMenuItem_Click);
            // 
            // routingTableToolStripMenuItem
            // 
            this.routingTableToolStripMenuItem.Name = "routingTableToolStripMenuItem";
            this.routingTableToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.routingTableToolStripMenuItem.Text = "Routing Table";
            this.routingTableToolStripMenuItem.Click += new System.EventHandler(this.RoutingTableToolStripMenuItem_Click);
            // 
            // ipconfigToolStripMenuItem
            // 
            this.ipconfigToolStripMenuItem.Name = "ipconfigToolStripMenuItem";
            this.ipconfigToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.ipconfigToolStripMenuItem.Text = "IP Configurations";
            this.ipconfigToolStripMenuItem.Click += new System.EventHandler(this.IpconfigToolStripMenuItem_Click);
            // 
            // ImprovedMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 882);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ImprovedMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NetSniffer";
            this.Load += new System.EventHandler(this.ImprovedMainForm_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.controlFlowLayoutPanel.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.capturePanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.startPanel.ResumeLayout(false);
            this.startPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetNumberUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalTopologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCaptureToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel startPanel;
        private System.Windows.Forms.Button startButton;
        private InterfaceInfo interfaceInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox interfaceComboBox;
        private CaptureFilter captureFilter;
        private System.Windows.Forms.Panel capturePanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox interfaceNameTextBox;
        private System.Windows.Forms.Label interfaceTitleLabel;
        private System.Windows.Forms.TextBox captureFilterTextBox;
        private System.Windows.Forms.FlowLayoutPanel controlFlowLayoutPanel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Panel panel2;
        private PacketViewer packetViewer;
        private System.Windows.Forms.Label moreInfoLabel;
        private System.Windows.Forms.CheckBox promiscuousCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown packetNumberUpDown;
        private System.Windows.Forms.Label captureFilterTitleLabel;
        private System.Windows.Forms.ToolStripMenuItem attackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilitisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arpTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routingTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ipconfigToolStripMenuItem;
    }
}