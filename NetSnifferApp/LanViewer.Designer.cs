
namespace NetSnifferApp
{
    partial class LanViewer
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
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.indLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // prevButton
            // 
            this.prevButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prevButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.prevButton.BackColor = System.Drawing.Color.White;
            this.prevButton.BackgroundImage = global::NetSnifferApp.Properties.Resources.left_arrow;
            this.prevButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.prevButton.Enabled = false;
            this.prevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prevButton.Location = new System.Drawing.Point(15, 292);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(76, 36);
            this.prevButton.TabIndex = 1;
            this.prevButton.UseVisualStyleBackColor = false;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nextButton.BackColor = System.Drawing.Color.White;
            this.nextButton.BackgroundImage = global::NetSnifferApp.Properties.Resources.right_arrow;
            this.nextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextButton.Location = new System.Drawing.Point(328, 300);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(76, 36);
            this.nextButton.TabIndex = 2;
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // indLabel
            // 
            this.indLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.indLabel.AutoSize = true;
            this.indLabel.Location = new System.Drawing.Point(190, 308);
            this.indLabel.Name = "indLabel";
            this.indLabel.Size = new System.Drawing.Size(43, 20);
            this.indLabel.TabIndex = 3;
            this.indLabel.Text = " 1/25";
            this.indLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Location = new System.Drawing.Point(15, 16);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(389, 267);
            this.mainPanel.TabIndex = 4;
            // 
            // LanViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.indLabel);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.prevButton);
            this.Name = "LanViewer";
            this.Size = new System.Drawing.Size(423, 349);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label indLabel;
        private System.Windows.Forms.Panel mainPanel;
    }
}
