
namespace NetSnifferApp
{
    partial class GeneralStatisticsForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ip4BytesLbl = new System.Windows.Forms.Label();
            this.ip4NumLbl = new System.Windows.Forms.Label();
            this.eNumLbl = new System.Windows.Forms.Label();
            this.tNumLbl = new System.Windows.Forms.Label();
            this.ip6NumLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ip6BytesLbl = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tcpBytesLbl = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tcpNumLbl = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.udpBytesLbl = new System.Windows.Forms.Label();
            this.udpNumLbl = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tBytesLbl = new System.Windows.Forms.Label();
            this.eBytesLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.zeroButton = new System.Windows.Forms.Button();
            this.elapsedTimeTitleLabel = new System.Windows.Forms.Label();
            this.elapsedTimeTimer = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.freezeButtom = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total No. of packets: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 29);
            this.label3.TabIndex = 1;
            this.label3.Text = "No. of Ethernet packets:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "No. of IPv4 packets: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 29);
            this.label6.TabIndex = 3;
            this.label6.Text = "Bytes Tx. over IPv4:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ip4BytesLbl
            // 
            this.ip4BytesLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ip4BytesLbl.AutoSize = true;
            this.ip4BytesLbl.Location = new System.Drawing.Point(204, 145);
            this.ip4BytesLbl.Name = "ip4BytesLbl";
            this.ip4BytesLbl.Size = new System.Drawing.Size(23, 29);
            this.ip4BytesLbl.TabIndex = 7;
            this.ip4BytesLbl.Text = "0 B";
            this.ip4BytesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ip4NumLbl
            // 
            this.ip4NumLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ip4NumLbl.AutoSize = true;
            this.ip4NumLbl.Location = new System.Drawing.Point(209, 116);
            this.ip4NumLbl.Name = "ip4NumLbl";
            this.ip4NumLbl.Size = new System.Drawing.Size(13, 29);
            this.ip4NumLbl.TabIndex = 6;
            this.ip4NumLbl.Text = "0";
            this.ip4NumLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // eNumLbl
            // 
            this.eNumLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.eNumLbl.AutoSize = true;
            this.eNumLbl.Location = new System.Drawing.Point(209, 58);
            this.eNumLbl.Name = "eNumLbl";
            this.eNumLbl.Size = new System.Drawing.Size(13, 29);
            this.eNumLbl.TabIndex = 5;
            this.eNumLbl.Text = "0";
            this.eNumLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tNumLbl
            // 
            this.tNumLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tNumLbl.AutoSize = true;
            this.tNumLbl.Location = new System.Drawing.Point(209, 0);
            this.tNumLbl.Name = "tNumLbl";
            this.tNumLbl.Size = new System.Drawing.Size(13, 29);
            this.tNumLbl.TabIndex = 4;
            this.tNumLbl.Text = "0";
            this.tNumLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ip6NumLbl
            // 
            this.ip6NumLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ip6NumLbl.AutoSize = true;
            this.ip6NumLbl.Location = new System.Drawing.Point(209, 174);
            this.ip6NumLbl.Name = "ip6NumLbl";
            this.ip6NumLbl.Size = new System.Drawing.Size(13, 29);
            this.ip6NumLbl.TabIndex = 9;
            this.ip6NumLbl.Text = "0";
            this.ip6NumLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 29);
            this.label7.TabIndex = 8;
            this.label7.Text = "No. of IPv6 packets:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ip6BytesLbl
            // 
            this.ip6BytesLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ip6BytesLbl.AutoSize = true;
            this.ip6BytesLbl.Location = new System.Drawing.Point(204, 203);
            this.ip6BytesLbl.Name = "ip6BytesLbl";
            this.ip6BytesLbl.Size = new System.Drawing.Size(23, 29);
            this.ip6BytesLbl.TabIndex = 11;
            this.ip6BytesLbl.Text = "0 B";
            this.ip6BytesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 29);
            this.label8.TabIndex = 10;
            this.label8.Text = "Bytes Tx. over IPv6:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tcpBytesLbl
            // 
            this.tcpBytesLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tcpBytesLbl.AutoSize = true;
            this.tcpBytesLbl.Location = new System.Drawing.Point(204, 319);
            this.tcpBytesLbl.Name = "tcpBytesLbl";
            this.tcpBytesLbl.Size = new System.Drawing.Size(23, 37);
            this.tcpBytesLbl.TabIndex = 19;
            this.tcpBytesLbl.Text = "0 B";
            this.tcpBytesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(3, 319);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 37);
            this.label12.TabIndex = 18;
            this.label12.Text = "Bytes Tx. over TCP:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tcpNumLbl
            // 
            this.tcpNumLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tcpNumLbl.AutoSize = true;
            this.tcpNumLbl.Location = new System.Drawing.Point(209, 290);
            this.tcpNumLbl.Name = "tcpNumLbl";
            this.tcpNumLbl.Size = new System.Drawing.Size(13, 29);
            this.tcpNumLbl.TabIndex = 17;
            this.tcpNumLbl.Text = "0";
            this.tcpNumLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 290);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 29);
            this.label11.TabIndex = 16;
            this.label11.Text = "TCP packets No.:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // udpBytesLbl
            // 
            this.udpBytesLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.udpBytesLbl.AutoSize = true;
            this.udpBytesLbl.Location = new System.Drawing.Point(204, 261);
            this.udpBytesLbl.Name = "udpBytesLbl";
            this.udpBytesLbl.Size = new System.Drawing.Size(23, 29);
            this.udpBytesLbl.TabIndex = 15;
            this.udpBytesLbl.Text = "0 B";
            this.udpBytesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // udpNumLbl
            // 
            this.udpNumLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.udpNumLbl.AutoSize = true;
            this.udpNumLbl.Location = new System.Drawing.Point(209, 232);
            this.udpNumLbl.Name = "udpNumLbl";
            this.udpNumLbl.Size = new System.Drawing.Size(13, 29);
            this.udpNumLbl.TabIndex = 14;
            this.udpNumLbl.Text = "0";
            this.udpNumLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 261);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 29);
            this.label10.TabIndex = 13;
            this.label10.Text = "Bytes Tx. over UDP:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(3, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 29);
            this.label9.TabIndex = 12;
            this.label9.Text = "UDP packets No.: ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 29);
            this.label2.TabIndex = 20;
            this.label2.Text = "Total Tx. bytes: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tBytesLbl
            // 
            this.tBytesLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tBytesLbl.AutoSize = true;
            this.tBytesLbl.Location = new System.Drawing.Point(204, 29);
            this.tBytesLbl.Name = "tBytesLbl";
            this.tBytesLbl.Size = new System.Drawing.Size(23, 29);
            this.tBytesLbl.TabIndex = 21;
            this.tBytesLbl.Text = "0 B";
            this.tBytesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // eBytesLbl
            // 
            this.eBytesLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.eBytesLbl.AutoSize = true;
            this.eBytesLbl.Location = new System.Drawing.Point(204, 87);
            this.eBytesLbl.Name = "eBytesLbl";
            this.eBytesLbl.Size = new System.Drawing.Size(23, 29);
            this.eBytesLbl.TabIndex = 23;
            this.eBytesLbl.Text = "0 B";
            this.eBytesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 29);
            this.label4.TabIndex = 22;
            this.label4.Text = "Bytes Tx. over Ethernet:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.09338F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.90661F));
            this.tableLayoutPanel1.Controls.Add(this.tNumLbl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tcpBytesLbl, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.eBytesLbl, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tcpNumLbl, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.tBytesLbl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.udpBytesLbl, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.udpNumLbl, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.ip6BytesLbl, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.ip6NumLbl, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.ip4BytesLbl, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.ip4NumLbl, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.eNumLbl, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(53, 61);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333335F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(257, 356);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // zeroButton
            // 
            this.zeroButton.Location = new System.Drawing.Point(226, 435);
            this.zeroButton.Name = "zeroButton";
            this.zeroButton.Size = new System.Drawing.Size(84, 23);
            this.zeroButton.TabIndex = 26;
            this.zeroButton.Text = "Zero";
            this.zeroButton.UseVisualStyleBackColor = true;
            this.zeroButton.Click += new System.EventHandler(this.zeroButton_Click);
            // 
            // elapsedTimeTitleLabel
            // 
            this.elapsedTimeTitleLabel.AutoSize = true;
            this.elapsedTimeTitleLabel.Location = new System.Drawing.Point(53, 27);
            this.elapsedTimeTitleLabel.Name = "elapsedTimeTitleLabel";
            this.elapsedTimeTitleLabel.Size = new System.Drawing.Size(77, 15);
            this.elapsedTimeTitleLabel.TabIndex = 27;
            this.elapsedTimeTitleLabel.Text = "Elapsed time:";
            // 
            // elapsedTimeTimer
            // 
            this.elapsedTimeTimer.Tick += new System.EventHandler(this.elapsedTimeTimer_Tick);
            // 
            // elapsedTimeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(141, 27);
            this.timeLabel.Name = "elapsedTimeLabel";
            this.timeLabel.Size = new System.Drawing.Size(21, 15);
            this.timeLabel.TabIndex = 28;
            this.timeLabel.Text = "0 s";
            // 
            // freezeButtom
            // 
            this.freezeButtom.Location = new System.Drawing.Point(53, 435);
            this.freezeButtom.Name = "freezeButtom";
            this.freezeButtom.Size = new System.Drawing.Size(75, 23);
            this.freezeButtom.TabIndex = 29;
            this.freezeButtom.Text = "Zreeze";
            this.freezeButtom.UseVisualStyleBackColor = true;
            this.freezeButtom.Click += new System.EventHandler(this.freezeButtom_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(159, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(44, 15);
            this.titleLabel.TabIndex = 30;
            this.titleLabel.Text = "label13";
            // 
            // GeneralStatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 499);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.freezeButtom);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.elapsedTimeTitleLabel);
            this.Controls.Add(this.zeroButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GeneralStatisticsForm";
            this.Text = "General Statistics";
            this.Load += new System.EventHandler(this.GeneralStatisticsForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ip4BytesLbl;
        private System.Windows.Forms.Label ip4NumLbl;
        private System.Windows.Forms.Label eNumLbl;
        private System.Windows.Forms.Label tNumLbl;
        private System.Windows.Forms.Label ip6NumLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label ip6BytesLbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label tcpBytesLbl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label tcpNumLbl;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label udpBytesLbl;
        private System.Windows.Forms.Label udpNumLbl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tBytesLbl;
        private System.Windows.Forms.Label eBytesLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Button zeroButton;
        private System.Windows.Forms.Label elapsedTimeTitleLabel;
        private System.Windows.Forms.Timer elapsedTimeTimer;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button freezeButtom;
        private System.Windows.Forms.Label titleLabel;
    }
}