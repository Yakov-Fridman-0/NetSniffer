
namespace NetSnifferApp
{
    partial class TopologyForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("LAN");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("WAN");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lanTreeView = new System.Windows.Forms.TreeView();
            this.wanTreeView = new System.Windows.Forms.TreeView();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.lanTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wanTreeView);
            this.splitContainer1.Size = new System.Drawing.Size(335, 450);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // lanTreeView
            // 
            this.lanTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lanTreeView.Location = new System.Drawing.Point(0, 0);
            this.lanTreeView.Name = "lanTreeView";
            treeNode1.Name = "LanNode";
            treeNode1.Text = "LAN";
            this.lanTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.lanTreeView.Size = new System.Drawing.Size(335, 210);
            this.lanTreeView.TabIndex = 0;
            // 
            // wanTreeView
            // 
            this.wanTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wanTreeView.Location = new System.Drawing.Point(0, 0);
            this.wanTreeView.Name = "wanTreeView";
            treeNode2.Name = "WanNode";
            treeNode2.Text = "WAN";
            this.wanTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.wanTreeView.Size = new System.Drawing.Size(335, 236);
            this.wanTreeView.TabIndex = 0;
            this.wanTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.wanTreeView_MouseClick);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // TopologyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TopologyForm";
            this.Text = "TopologyForm";
            this.Load += new System.EventHandler(this.TopologyForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TreeView lanTreeView;
        private System.Windows.Forms.TreeView wanTreeView;
    }
}