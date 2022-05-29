
namespace NetSnifferApp
{
    partial class AttackLogForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.attacksNumberLabel = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.UpdateTime_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(353, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save Log";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label2
            // 
            this.attacksNumberLabel.AutoSize = true;
            this.attacksNumberLabel.Location = new System.Drawing.Point(21, 9);
            this.attacksNumberLabel.Name = "label2";
            this.attacksNumberLabel.Size = new System.Drawing.Size(99, 20);
            this.attacksNumberLabel.TabIndex = 3;
            this.attacksNumberLabel.Text = "Attacks No.: 0";
            // 
            // richTextBox
            // 
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox.Location = new System.Drawing.Point(21, 41);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(758, 349);
            this.richTextBox.TabIndex = 4;
            this.richTextBox.Text = "";
            this.richTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.RichTextBox_LinkClicked);
            // 
            // AttackLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.attacksNumberLabel);
            this.Controls.Add(this.button1);
            this.Name = "AttackLogForm";
            this.Text = "Attack Log";
            this.Load += new System.EventHandler(this.AttackLogForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label attacksNumberLabel;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}