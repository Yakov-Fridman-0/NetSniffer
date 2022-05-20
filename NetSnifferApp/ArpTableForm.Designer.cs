
namespace ArpTableWinFormsApp.ArpTable
{
    partial class ArpTableForm
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
            this.arpTableControl = new ArpTableControl();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.arpTableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arpTableControl.Location = new System.Drawing.Point(0, 0);
            this.arpTableControl.Name = "arpTableControl";
            this.arpTableControl.Size = new System.Drawing.Size(800, 450);
            this.arpTableControl.TabIndex = 0;
            // 
            // ArpTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.arpTableControl);
            this.Name = "ArpTableForm";
            this.Text = "ArpTableForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ArpTableControl arpTableControl;
    }
}