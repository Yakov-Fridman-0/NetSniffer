
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
            this.length = new System.Windows.Forms.ColumnHeader();
            this.info = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.displayFilterControl = new NetSnifferApp.DisplayFilterControl();
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
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(725, 174);
            this.tabControl.TabIndex = 2;
            // 
            // packetPage
            // 
            this.packetPage.Controls.Add(this.binaryDataTextBox);
            this.packetPage.Controls.Add(this.label1);
            this.packetPage.Location = new System.Drawing.Point(4, 29);
            this.packetPage.Name = "packetPage";
            this.packetPage.Padding = new System.Windows.Forms.Padding(3);
            this.packetPage.Size = new System.Drawing.Size(717, 141);
            this.packetPage.TabIndex = 0;
            this.packetPage.Text = "Packet";
            this.packetPage.UseVisualStyleBackColor = true;
            // 
            // binaryDataTextBox
            // 
            this.binaryDataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.binaryDataTextBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.binaryDataTextBox.Location = new System.Drawing.Point(8, 40);
            this.binaryDataTextBox.Multiline = true;
            this.binaryDataTextBox.Name = "binaryDataTextBox";
            this.binaryDataTextBox.ReadOnly = true;
            this.binaryDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.binaryDataTextBox.Size = new System.Drawing.Size(703, 92);
            this.binaryDataTextBox.TabIndex = 1;
            this.binaryDataTextBox.Resize += new System.EventHandler(this.binaryDataTextBox_Resize);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Binary Data:";
            // 
            // attackPage
            // 
            this.attackPage.Controls.Add(this.attackTextBox);
            this.attackPage.Controls.Add(this.attacksComboBox);
            this.attackPage.Controls.Add(this.label2);
            this.attackPage.Location = new System.Drawing.Point(4, 29);
            this.attackPage.Name = "attackPage";
            this.attackPage.Padding = new System.Windows.Forms.Padding(3);
            this.attackPage.Size = new System.Drawing.Size(717, 141);
            this.attackPage.TabIndex = 1;
            this.attackPage.Text = "Attacks";
            this.attackPage.UseVisualStyleBackColor = true;
            // 
            // attackTextBox
            // 
            this.attackTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attackTextBox.Location = new System.Drawing.Point(8, 40);
            this.attackTextBox.Multiline = true;
            this.attackTextBox.Name = "attackTextBox";
            this.attackTextBox.ReadOnly = true;
            this.attackTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.attackTextBox.Size = new System.Drawing.Size(703, 92);
            this.attackTextBox.TabIndex = 2;
            // 
            // attacksComboBox
            // 
            this.attacksComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attacksComboBox.FormattingEnabled = true;
            this.attacksComboBox.Location = new System.Drawing.Point(150, 5);
            this.attacksComboBox.Name = "attacksComboBox";
            this.attacksComboBox.Size = new System.Drawing.Size(151, 28);
            this.attacksComboBox.TabIndex = 1;
            this.attacksComboBox.SelectedIndexChanged += new System.EventHandler(this.attacksComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Associated Attacks:";
            // 
            // packetsListView
            // 
            this.packetsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packetsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.Time,
            this.Protocol,
            this.Source,
            this.Destination,
            this.length,
            this.info});
            this.packetsListView.FullRowSelect = true;
            this.packetsListView.GridLines = true;
            this.packetsListView.HideSelection = false;
            this.packetsListView.Location = new System.Drawing.Point(0, 68);
            this.packetsListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.packetsListView.MultiSelect = false;
            this.packetsListView.Name = "packetsListView";
            this.packetsListView.ShowGroups = false;
            this.packetsListView.Size = new System.Drawing.Size(724, 336);
            this.packetsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.packetsListView.TabIndex = 0;
            this.packetsListView.UseCompatibleStateImageBehavior = false;
            this.packetsListView.View = System.Windows.Forms.View.Details;
            this.packetsListView.VirtualMode = true;
            this.packetsListView.SelectedIndexChanged += new System.EventHandler(this.packetsListView_SelectedIndexChanged);
            this.packetsListView.Resize += new System.EventHandler(this.packetsListView_Resize);
            // 
            // Index
            // 
            this.Index.Text = "Index";
            this.Index.Width = 80;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 200;
            // 
            // Protocol
            // 
            this.Protocol.Text = "Protocol";
            this.Protocol.Width = 100;
            // 
            // Source
            // 
            this.Source.Text = "Source";
            this.Source.Width = 120;
            // 
            // Destination
            // 
            this.Destination.Text = "Destination";
            this.Destination.Width = 120;
            // 
            // length
            // 
            this.length.Text = "Length";
            this.length.Width = 80;
            // 
            // info
            // 
            this.info.Text = "Info";
            this.info.Width = 130;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.displayFilterControl);
            this.splitContainer1.Panel1.Controls.Add(this.packetsListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(725, 587);
            this.splitContainer1.SplitterDistance = 409;
            this.splitContainer1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Display Filter:";
            // 
            // displayFilterControl
            // 
            this.displayFilterControl.Filter = "";
            this.displayFilterControl.IsValidFilter = true;
            this.displayFilterControl.Location = new System.Drawing.Point(104, 14);
            this.displayFilterControl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.displayFilterControl.MaximumSize = new System.Drawing.Size(1000, 49);
            this.displayFilterControl.MinimumSize = new System.Drawing.Size(0, 49);
            this.displayFilterControl.Name = "displayFilterControl";
            this.displayFilterControl.Size = new System.Drawing.Size(561, 49);
            this.displayFilterControl.TabIndex = 1;
            this.displayFilterControl.FilterChanged += new System.EventHandler<string>(this.displayFilter1_FilterChanged);
            // 
            // PacketViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PacketViewer";
            this.Size = new System.Drawing.Size(725, 587);
            this.tabControl.ResumeLayout(false);
            this.packetPage.ResumeLayout(false);
            this.packetPage.PerformLayout();
            this.attackPage.ResumeLayout(false);
            this.attackPage.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
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
        private System.Windows.Forms.ColumnHeader length;
        private System.Windows.Forms.ColumnHeader info;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DisplayFilterControl displayFilterControl;
        private System.Windows.Forms.Label label3;
    }
}
