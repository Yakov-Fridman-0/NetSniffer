
namespace NetSnifferApp
{
    partial class StatisticsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ip4BytesLbl = new System.Windows.Forms.Label();
            this.ip4NumLbl = new System.Windows.Forms.Label();
            this.eNumLbl = new System.Windows.Forms.Label();
            this.tNumLbl = new System.Windows.Forms.Label();
            this.ip6NumLbl = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ip6BytesLbl = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tcpBytesLbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tcpNumLbl = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.udpBytesLbl = new System.Windows.Forms.Label();
            this.udpNumLbl = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Number of packets: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of Etherbet packets:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Number of IPv4 packets: ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Data transmitted over IPv4:";
            // 
            // ip4BytesLbl
            // 
            this.ip4BytesLbl.AutoSize = true;
            this.ip4BytesLbl.Location = new System.Drawing.Point(245, 177);
            this.ip4BytesLbl.Name = "ip4BytesLbl";
            this.ip4BytesLbl.Size = new System.Drawing.Size(50, 20);
            this.ip4BytesLbl.TabIndex = 7;
            this.ip4BytesLbl.Text = "label5";
            // 
            // ip4NumLbl
            // 
            this.ip4NumLbl.AutoSize = true;
            this.ip4NumLbl.Location = new System.Drawing.Point(245, 129);
            this.ip4NumLbl.Name = "ip4NumLbl";
            this.ip4NumLbl.Size = new System.Drawing.Size(50, 20);
            this.ip4NumLbl.TabIndex = 6;
            this.ip4NumLbl.Text = "label6";
            // 
            // eNumLbl
            // 
            this.eNumLbl.AutoSize = true;
            this.eNumLbl.Location = new System.Drawing.Point(245, 88);
            this.eNumLbl.Name = "eNumLbl";
            this.eNumLbl.Size = new System.Drawing.Size(50, 20);
            this.eNumLbl.TabIndex = 5;
            this.eNumLbl.Text = "label7";
            // 
            // tNumLbl
            // 
            this.tNumLbl.AutoSize = true;
            this.tNumLbl.Location = new System.Drawing.Point(245, 46);
            this.tNumLbl.Name = "tNumLbl";
            this.tNumLbl.Size = new System.Drawing.Size(50, 20);
            this.tNumLbl.TabIndex = 4;
            this.tNumLbl.Text = "label8";
            // 
            // ip6NumLbl
            // 
            this.ip6NumLbl.AutoSize = true;
            this.ip6NumLbl.Location = new System.Drawing.Point(245, 218);
            this.ip6NumLbl.Name = "ip6NumLbl";
            this.ip6NumLbl.Size = new System.Drawing.Size(50, 20);
            this.ip6NumLbl.TabIndex = 9;
            this.ip6NumLbl.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 218);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "Number of IPv6 packets:";
            // 
            // ip6BytesLbl
            // 
            this.ip6BytesLbl.AutoSize = true;
            this.ip6BytesLbl.Location = new System.Drawing.Point(245, 255);
            this.ip6BytesLbl.Name = "ip6BytesLbl";
            this.ip6BytesLbl.Size = new System.Drawing.Size(58, 20);
            this.ip6BytesLbl.TabIndex = 11;
            this.ip6BytesLbl.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 255);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(188, 20);
            this.label12.TabIndex = 10;
            this.label12.Text = "Data transmitted over IPv6:";
            // 
            // tcpBytesLbl
            // 
            this.tcpBytesLbl.AutoSize = true;
            this.tcpBytesLbl.Location = new System.Drawing.Point(245, 430);
            this.tcpBytesLbl.Name = "tcpBytesLbl";
            this.tcpBytesLbl.Size = new System.Drawing.Size(58, 20);
            this.tcpBytesLbl.TabIndex = 19;
            this.tcpBytesLbl.Text = "label13";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 430);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(185, 20);
            this.label14.TabIndex = 18;
            this.label14.Text = "Data transmitted over TCP:";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // tcpNumLbl
            // 
            this.tcpNumLbl.AutoSize = true;
            this.tcpNumLbl.Location = new System.Drawing.Point(245, 393);
            this.tcpNumLbl.Name = "tcpNumLbl";
            this.tcpNumLbl.Size = new System.Drawing.Size(58, 20);
            this.tcpNumLbl.TabIndex = 17;
            this.tcpNumLbl.Text = "label15";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(21, 393);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(166, 20);
            this.label16.TabIndex = 16;
            this.label16.Text = "Number of TCP packets:";
            // 
            // udpBytesLbl
            // 
            this.udpBytesLbl.AutoSize = true;
            this.udpBytesLbl.Location = new System.Drawing.Point(245, 352);
            this.udpBytesLbl.Name = "udpBytesLbl";
            this.udpBytesLbl.Size = new System.Drawing.Size(58, 20);
            this.udpBytesLbl.TabIndex = 15;
            this.udpBytesLbl.Text = "label17";
            // 
            // udpNumLbl
            // 
            this.udpNumLbl.AutoSize = true;
            this.udpNumLbl.Location = new System.Drawing.Point(245, 304);
            this.udpNumLbl.Name = "udpNumLbl";
            this.udpNumLbl.Size = new System.Drawing.Size(58, 20);
            this.udpNumLbl.TabIndex = 14;
            this.udpNumLbl.Text = "label18";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(21, 352);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(190, 20);
            this.label19.TabIndex = 13;
            this.label19.Text = "Data transmitted over UDP:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(21, 304);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(175, 20);
            this.label20.TabIndex = 12;
            this.label20.Text = "Number of UDP packets: ";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 530);
            this.Controls.Add(this.tcpBytesLbl);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tcpNumLbl);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.udpBytesLbl);
            this.Controls.Add(this.udpNumLbl);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.ip6BytesLbl);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ip6NumLbl);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ip4BytesLbl);
            this.Controls.Add(this.ip4NumLbl);
            this.Controls.Add(this.eNumLbl);
            this.Controls.Add(this.tNumLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "StatisticsForm";
            this.Text = "Statistics";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ip4BytesLbl;
        private System.Windows.Forms.Label ip4NumLbl;
        private System.Windows.Forms.Label eNumLbl;
        private System.Windows.Forms.Label tNumLbl;
        private System.Windows.Forms.Label ip6NumLbl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label ip6BytesLbl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label tcpBytesLbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label tcpNumLbl;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label udpBytesLbl;
        private System.Windows.Forms.Label udpNumLbl;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
    }
}