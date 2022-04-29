
namespace NetSnifferApp
{
    partial class TcpStreamsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.streamsListView = new System.Windows.Forms.ListView();
            this.port = new System.Windows.Forms.ColumnHeader();
            this.arrowColumn = new System.Windows.Forms.ColumnHeader();
            this.remoteIPAddressColumn = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Location = new System.Drawing.Point(298, 23);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(50, 20);
            this.ipAddressLabel.TabIndex = 1;
            this.ipAddressLabel.Text = "label2";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // streamsListView
            // 
            this.streamsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.streamsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.port,
            this.arrowColumn,
            this.remoteIPAddressColumn,
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.streamsListView.FullRowSelect = true;
            this.streamsListView.GridLines = true;
            this.streamsListView.HideSelection = false;
            this.streamsListView.Location = new System.Drawing.Point(12, 59);
            this.streamsListView.Name = "streamsListView";
            this.streamsListView.Size = new System.Drawing.Size(585, 301);
            this.streamsListView.TabIndex = 2;
            this.streamsListView.UseCompatibleStateImageBehavior = false;
            this.streamsListView.View = System.Windows.Forms.View.Details;
            // 
            // port
            // 
            this.port.Text = "Port";
            // 
            // arrowColumn
            // 
            this.arrowColumn.Text = "";
            this.arrowColumn.Width = 31;
            // 
            // remoteIPAddressColumn
            // 
            this.remoteIPAddressColumn.Text = "Remote IP Address";
            this.remoteIPAddressColumn.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Remote Port";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Data Sent";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Data Received";
            this.columnHeader2.Width = 100;
            // 
            // TcpStreamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 386);
            this.Controls.Add(this.streamsListView);
            this.Controls.Add(this.ipAddressLabel);
            this.Controls.Add(this.label1);
            this.Name = "TcpStreamsForm";
            this.Text = "Tcp Streams";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ListView streamsListView;
        private System.Windows.Forms.ColumnHeader port;
        private System.Windows.Forms.ColumnHeader arrowColumn;
        private System.Windows.Forms.ColumnHeader remoteIPAddressColumn;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}