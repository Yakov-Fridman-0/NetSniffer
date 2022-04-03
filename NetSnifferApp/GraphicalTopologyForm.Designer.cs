
namespace NetSnifferApp
{
    partial class GraphicalTopologyForm
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
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.topologyViewer = new NetSnifferApp.TopologyViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.subnetMaskLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // topologyViewer
            // 
            this.topologyViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topologyViewer.Location = new System.Drawing.Point(12, 38);
            this.topologyViewer.Name = "topologyViewer";
            this.topologyViewer.Size = new System.Drawing.Size(776, 400);
            this.topologyViewer.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Subnet Mask:";
            // 
            // subnetMaskLabel
            // 
            this.subnetMaskLabel.AutoSize = true;
            this.subnetMaskLabel.Location = new System.Drawing.Point(114, 9);
            this.subnetMaskLabel.Name = "subnetMaskLabel";
            this.subnetMaskLabel.Size = new System.Drawing.Size(50, 20);
            this.subnetMaskLabel.TabIndex = 3;
            this.subnetMaskLabel.Text = "0.0.0.0";
            // 
            // GraphicalTopologyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.subnetMaskLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.topologyViewer);
            this.Name = "GraphicalTopologyForm";
            this.Text = "Topology";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer updateTimer;
        private TopologyViewer topologyViewer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label subnetMaskLabel;
    }
}