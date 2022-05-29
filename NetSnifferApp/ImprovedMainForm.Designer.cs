
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel panel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImprovedMainForm));
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
            this.openFSFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPrivateFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFSFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePrivateFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalTopologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arpTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.routingTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ipconfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
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
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            panel1.Size = new System.Drawing.Size(1004, 81);
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
            this.controlFlowLayoutPanel.Location = new System.Drawing.Point(788, 16);
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
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
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
            this.restartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.statisticsToolStripMenuItem,
            this.topologyToolStripMenuItem,
            this.attackToolStripMenuItem,
            this.utilitiesToolStripMenuItem});
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
            this.openToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFSFileToolStripMenuItem,
            this.openPrivateFileToolStripMenuItem});
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem1.Text = "Open";
            // 
            // openFSFileToolStripMenuItem
            // 
            this.openFSFileToolStripMenuItem.Name = "openFSFileToolStripMenuItem";
            this.openFSFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openFSFileToolStripMenuItem.Text = "Open FS File ";
            this.openFSFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFSFileToolStripMenuItem_Click);
            // 
            // openPrivateFileToolStripMenuItem
            // 
            this.openPrivateFileToolStripMenuItem.Enabled = false;
            this.openPrivateFileToolStripMenuItem.Name = "openPrivateFileToolStripMenuItem";
            this.openPrivateFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openPrivateFileToolStripMenuItem.Text = "Open Private File";
            this.openPrivateFileToolStripMenuItem.Click += new System.EventHandler(this.OpenPrivateFileToolStripMenuItem_Click);
            // 
            // newCaptureToolStripMenuItem
            // 
            this.newCaptureToolStripMenuItem.Enabled = false;
            this.newCaptureToolStripMenuItem.Name = "newCaptureToolStripMenuItem";
            this.newCaptureToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.newCaptureToolStripMenuItem.Text = "New Capture";
            this.newCaptureToolStripMenuItem.Click += new System.EventHandler(this.NewCaptureToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveFSFileToolStripMenuItem,
            this.savePrivateFileToolStripMenuItem});
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // saveFSFileToolStripMenuItem
            // 
            this.saveFSFileToolStripMenuItem.Name = "saveFSFileToolStripMenuItem";
            this.saveFSFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveFSFileToolStripMenuItem.Text = "Save FS File";
            this.saveFSFileToolStripMenuItem.Click += new System.EventHandler(this.SaveFSFileToolStripMenuItem_Click);
            // 
            // savePrivateFileToolStripMenuItem
            // 
            this.savePrivateFileToolStripMenuItem.Enabled = false;
            this.savePrivateFileToolStripMenuItem.Name = "savePrivateFileToolStripMenuItem";
            this.savePrivateFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.savePrivateFileToolStripMenuItem.Text = "Save Private File";
            this.savePrivateFileToolStripMenuItem.Click += new System.EventHandler(this.SavePrivateFileToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalStatisticsToolStripMenuItem});
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            // 
            // generalStatisticsToolStripMenuItem
            // 
            this.generalStatisticsToolStripMenuItem.Name = "generalStatisticsToolStripMenuItem";
            this.generalStatisticsToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.generalStatisticsToolStripMenuItem.Text = "General Statistics";
            this.generalStatisticsToolStripMenuItem.Click += new System.EventHandler(this.GeneralStatisticsToolStripMenuItem_Click);
            // 
            // topologyToolStripMenuItem
            // 
            this.topologyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalTopologyToolStripMenuItem});
            this.topologyToolStripMenuItem.Name = "topologyToolStripMenuItem";
            this.topologyToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.topologyToolStripMenuItem.Text = "Topology";
            // 
            // generalTopologyToolStripMenuItem
            // 
            this.generalTopologyToolStripMenuItem.Name = "generalTopologyToolStripMenuItem";
            this.generalTopologyToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.generalTopologyToolStripMenuItem.Text = "General Topology";
            this.generalTopologyToolStripMenuItem.Click += new System.EventHandler(this.GeneralTopologyToolStripMenuItem_Click);
            // 
            // attackToolStripMenuItem
            // 
            this.attackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem});
            this.attackToolStripMenuItem.Name = "attackToolStripMenuItem";
            this.attackToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.attackToolStripMenuItem.Text = "Attacks";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(117, 26);
            this.logToolStripMenuItem.Text = "Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.LogToolStripMenuItem_Click);
            // 
            // utilitiesToolStripMenuItem
            // 
            this.utilitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arpTableToolStripMenuItem,
            this.routingTableToolStripMenuItem,
            this.ipconfigToolStripMenuItem});
            this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            this.utilitiesToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.utilitiesToolStripMenuItem.Text = "Utilities";
            // 
            // arpTableToolStripMenuItem
            // 
            this.arpTableToolStripMenuItem.Name = "arpTableToolStripMenuItem";
            this.arpTableToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.arpTableToolStripMenuItem.Text = "CAM Table";
            this.arpTableToolStripMenuItem.Click += new System.EventHandler(this.ArpTableToolStripMenuItem_Click);
            // 
            // routingTableToolStripMenuItem
            // 
            this.routingTableToolStripMenuItem.Name = "routingTableToolStripMenuItem";
            this.routingTableToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.routingTableToolStripMenuItem.Text = "Routing Table";
            this.routingTableToolStripMenuItem.Click += new System.EventHandler(this.RoutingTableToolStripMenuItem_Click);
            // 
            // ipconfigToolStripMenuItem
            // 
            this.ipconfigToolStripMenuItem.Name = "ipconfigToolStripMenuItem";
            this.ipconfigToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
            this.ipconfigToolStripMenuItem.Text = "IP Configurations";
            this.ipconfigToolStripMenuItem.Click += new System.EventHandler(this.IpconfigToolStripMenuItem_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.capturePanel);
            this.mainPanel.Controls.Add(this.startPanel);
            this.mainPanel.Location = new System.Drawing.Point(3, 44);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1010, 879);
            this.mainPanel.TabIndex = 2;
            // 
            // capturePanel
            // 
            this.capturePanel.Controls.Add(this.tableLayoutPanel1);
            this.capturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capturePanel.Location = new System.Drawing.Point(0, 0);
            this.capturePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.capturePanel.Name = "capturePanel";
            this.capturePanel.Size = new System.Drawing.Size(1010, 879);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1010, 879);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.packetViewer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 93);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1004, 782);
            this.panel2.TabIndex = 1;
            // 
            // packetViewer
            // 
            this.packetViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetViewer.Location = new System.Drawing.Point(0, 0);
            this.packetViewer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.packetViewer.Name = "packetViewer";
            this.packetViewer.Size = new System.Drawing.Size(1004, 782);
            this.packetViewer.TabIndex = 0;
            this.packetViewer.AttackAdded += new System.EventHandler(this.PacketViewer_AttackAdded);
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
            this.startPanel.Size = new System.Drawing.Size(1010, 879);
            this.startPanel.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 770);
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
            this.packetNumberUpDown.Location = new System.Drawing.Point(442, 765);
            this.packetNumberUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.packetNumberUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.packetNumberUpDown.Name = "packetNumberUpDown";
            this.packetNumberUpDown.Size = new System.Drawing.Size(137, 27);
            this.packetNumberUpDown.TabIndex = 7;
            this.packetNumberUpDown.ValueChanged += new System.EventHandler(this.PacketNumberUpDown_ValueChanged);
            // 
            // promiscuousCheckBox
            // 
            this.promiscuousCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.promiscuousCheckBox.AutoSize = true;
            this.promiscuousCheckBox.Checked = true;
            this.promiscuousCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.promiscuousCheckBox.Location = new System.Drawing.Point(619, 768);
            this.promiscuousCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.promiscuousCheckBox.Name = "promiscuousCheckBox";
            this.promiscuousCheckBox.Size = new System.Drawing.Size(114, 24);
            this.promiscuousCheckBox.TabIndex = 6;
            this.promiscuousCheckBox.Text = "Promiscuous";
            this.promiscuousCheckBox.UseVisualStyleBackColor = true;
            this.promiscuousCheckBox.CheckedChanged += new System.EventHandler(this.PromiscuousCheckBox_CheckedChanged);
            // 
            // interfaceInfo
            // 
            this.interfaceInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.interfaceInfo.Interface = null;
            this.interfaceInfo.Location = new System.Drawing.Point(205, 134);
            this.interfaceInfo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.interfaceInfo.Name = "interfaceInfo";
            this.interfaceInfo.Size = new System.Drawing.Size(596, 623);
            this.interfaceInfo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Capture Filter:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 29);
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
            this.interfaceComboBox.Location = new System.Drawing.Point(396, 24);
            this.interfaceComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.interfaceComboBox.Name = "interfaceComboBox";
            this.interfaceComboBox.Size = new System.Drawing.Size(219, 28);
            this.interfaceComboBox.TabIndex = 0;
            this.interfaceComboBox.SelectedIndexChanged += new System.EventHandler(this.InterfaceComboBox_SelectedIndexChanged);
            // 
            // captureFilter
            // 
            this.captureFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.captureFilter.IsValidFilter = true;
            this.captureFilter.Location = new System.Drawing.Point(396, 75);
            this.captureFilter.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.captureFilter.Name = "captureFilter";
            this.captureFilter.Size = new System.Drawing.Size(249, 49);
            this.captureFilter.TabIndex = 1;
            this.captureFilter.FilterChanged += new System.EventHandler(this.CaptureFilter_FilterChanged);
            // 
            // startButton
            // 
            this.startButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(457, 813);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(86, 31);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "explamation_mark.jpg");
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.mainPanel, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1016, 927);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.usernameLabel);
            this.panel3.Controls.Add(this.linkLabel1);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1010, 34);
            this.panel3.TabIndex = 7;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(457, 6);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(100, 20);
            this.usernameLabel.TabIndex = 5;
            this.usernameLabel.Text = "Not Signed In";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(899, 6);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(54, 20);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Sign In";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // ImprovedMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 957);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ImprovedMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NetSniffer";
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
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arpTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routingTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ipconfigToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.ToolStripMenuItem openFSFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPrivateFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFSFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePrivateFileToolStripMenuItem;
    }
}