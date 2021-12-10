
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
            this.lstvPackets = new System.Windows.Forms.ListView();
            this.Index = new System.Windows.Forms.ColumnHeader();
            this.Protocol = new System.Windows.Forms.ColumnHeader();
            this.Time = new System.Windows.Forms.ColumnHeader();
            this.Source = new System.Windows.Forms.ColumnHeader();
            this.Destination = new System.Windows.Forms.ColumnHeader();
            this.PayloadLength = new System.Windows.Forms.ColumnHeader();
            this.Description = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lstvPackets
            // 
            this.lstvPackets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.Time,
            this.Protocol,
            this.Source,
            this.Destination,
            this.PayloadLength,
            this.Description});
            this.lstvPackets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvPackets.FullRowSelect = true;
            this.lstvPackets.GridLines = true;
            this.lstvPackets.HideSelection = false;
            this.lstvPackets.Location = new System.Drawing.Point(0, 0);
            this.lstvPackets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstvPackets.Name = "lstvPackets";
            this.lstvPackets.ShowGroups = false;
            this.lstvPackets.Size = new System.Drawing.Size(611, 200);
            this.lstvPackets.TabIndex = 0;
            this.lstvPackets.UseCompatibleStateImageBehavior = false;
            this.lstvPackets.View = System.Windows.Forms.View.Details;
            // 
            // Index
            // 
            this.Index.Text = "Index";
            this.Index.Width = 80;
            // 
            // Protocol
            // 
            this.Protocol.Text = "Protocol";
            this.Protocol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Protocol.Width = 80;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 80;
            // 
            // Source
            // 
            this.Source.Text = "Source";
            this.Source.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Source.Width = 100;
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
            this.PayloadLength.Width = 80;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 150;
            // 
            // PacketViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstvPackets);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PacketViewer";
            this.Size = new System.Drawing.Size(611, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstvPackets;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader Protocol;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Source;
        private System.Windows.Forms.ColumnHeader Destination;
        private System.Windows.Forms.ColumnHeader PayloadLength;
        private System.Windows.Forms.ColumnHeader Description;
    }
}
