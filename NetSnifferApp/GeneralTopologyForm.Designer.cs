
namespace NetSnifferApp
{
    partial class GeneralTopologyForm
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.titleLabel = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lanViewer = new NetSnifferApp.LanViewer();
            this.wanViewer = new NetSnifferApp.WanViewer();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(9, 6);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 25);
            label1.TabIndex = 0;
            label1.Text = "LAN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(3, 6);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(58, 25);
            label2.TabIndex = 0;
            label2.Text = "WAN";
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(603, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(176, 20);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Topology from dump file";
            // 
            // splitContainer1
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(1, 62);
            this.splitContainer.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lanViewer);
            this.splitContainer.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.wanViewer);
            this.splitContainer.Panel2.Controls.Add(label2);
            this.splitContainer.Size = new System.Drawing.Size(1795, 956);
            this.splitContainer.SplitterDistance = 478;
            this.splitContainer.TabIndex = 1;
            // 
            // lanViewer
            // 
            this.lanViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lanViewer.IsLive = true;
            this.lanViewer.Location = new System.Drawing.Point(3, 34);
            this.lanViewer.Name = "lanViewer";
            this.lanViewer.Size = new System.Drawing.Size(468, 915);
            this.lanViewer.TabIndex = 1;
            // 
            // wanViewer
            // 
            this.wanViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wanViewer.IsLive = true;
            this.wanViewer.Location = new System.Drawing.Point(4, 35);
            this.wanViewer.Name = "wanViewer";
            this.wanViewer.Size = new System.Drawing.Size(1302, 914);
            this.wanViewer.TabIndex = 1;
            // 
            // GeneralTopologyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 1030);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GeneralTopologyForm";
            this.Text = "GeneralTopologyForm";
            this.Load += new System.EventHandler(this.GeneralTopologyForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private LanViewer lanViewer;
        private WanViewer wanViewer;
    }
}