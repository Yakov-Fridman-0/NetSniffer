
namespace NetSnifferApp
{
    partial class SignInForm
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
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.signInButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.createAccountLinkLabel = new System.Windows.Forms.LinkLabel();
            this.signInPanel = new System.Windows.Forms.Panel();
            this.signInResultLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.createAccountUsernameTextBox = new System.Windows.Forms.TextBox();
            this.validatePasswordTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.createAccountPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.createAccountPanel = new System.Windows.Forms.Panel();
            this.signInLinkLabel = new System.Windows.Forms.LinkLabel();
            this.createAccountResultLabel = new System.Windows.Forms.Label();
            this.createAccountButton = new System.Windows.Forms.Button();
            this.signInLink = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.signInPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.createAccountPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTextBox.Location = new System.Drawing.Point(103, 11);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(231, 27);
            this.usernameTextBox.TabIndex = 2;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(103, 64);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(231, 27);
            this.passwordTextBox.TabIndex = 3;
            // 
            // signInButton
            // 
            this.signInButton.Location = new System.Drawing.Point(230, 211);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(94, 29);
            this.signInButton.TabIndex = 4;
            this.signInButton.Text = "Sign In";
            this.signInButton.UseVisualStyleBackColor = true;
            this.signInButton.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.passwordTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.usernameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(109, 55);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(337, 105);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // createAccountLinkLabel
            // 
            this.createAccountLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createAccountLinkLabel.AutoSize = true;
            this.createAccountLinkLabel.Location = new System.Drawing.Point(19, 282);
            this.createAccountLinkLabel.Name = "createAccountLinkLabel";
            this.createAccountLinkLabel.Size = new System.Drawing.Size(144, 20);
            this.createAccountLinkLabel.TabIndex = 6;
            this.createAccountLinkLabel.TabStop = true;
            this.createAccountLinkLabel.Text = "Create New Account";
            this.createAccountLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CreateNewAccountLinkLabel_LinkClicked);
            // 
            // signInPanel
            // 
            this.signInPanel.Controls.Add(this.signInButton);
            this.signInPanel.Controls.Add(this.createAccountLinkLabel);
            this.signInPanel.Controls.Add(this.tableLayoutPanel1);
            this.signInPanel.Controls.Add(this.signInResultLabel);
            this.signInPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signInPanel.Location = new System.Drawing.Point(0, 0);
            this.signInPanel.Name = "signInPanel";
            this.signInPanel.Size = new System.Drawing.Size(554, 316);
            this.signInPanel.TabIndex = 7;
            // 
            // signInResultLabel
            // 
            this.signInResultLabel.AutoSize = true;
            this.signInResultLabel.Location = new System.Drawing.Point(109, 176);
            this.signInResultLabel.Name = "signInResultLabel";
            this.signInResultLabel.Size = new System.Drawing.Size(45, 20);
            this.signInResultLabel.TabIndex = 7;
            this.signInResultLabel.Text = "result";
            this.signInResultLabel.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.createAccountUsernameTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.validatePasswordTextBox, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.createAccountPasswordTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(109, 55);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(337, 150);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // createAccountUsernameTextBox
            // 
            this.createAccountUsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.createAccountUsernameTextBox.Location = new System.Drawing.Point(103, 11);
            this.createAccountUsernameTextBox.Name = "createAccountUsernameTextBox";
            this.createAccountUsernameTextBox.Size = new System.Drawing.Size(231, 27);
            this.createAccountUsernameTextBox.TabIndex = 2;
            // 
            // validatePasswordTextBox
            // 
            this.validatePasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.validatePasswordTextBox.Location = new System.Drawing.Point(103, 111);
            this.validatePasswordTextBox.Name = "validatePasswordTextBox";
            this.validatePasswordTextBox.PasswordChar = '*';
            this.validatePasswordTextBox.Size = new System.Drawing.Size(231, 27);
            this.validatePasswordTextBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 40);
            this.label5.TabIndex = 4;
            this.label5.Text = "Validate Password:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username:";
            // 
            // createAccountPasswordTextBox
            // 
            this.createAccountPasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.createAccountPasswordTextBox.Location = new System.Drawing.Point(103, 61);
            this.createAccountPasswordTextBox.Name = "createAccountPasswordTextBox";
            this.createAccountPasswordTextBox.PasswordChar = '*';
            this.createAccountPasswordTextBox.Size = new System.Drawing.Size(231, 27);
            this.createAccountPasswordTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password:";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.createAccountPanel);
            this.mainPanel.Controls.Add(this.signInPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(554, 316);
            this.mainPanel.TabIndex = 9;
            // 
            // createAccountPanel
            // 
            this.createAccountPanel.Controls.Add(this.signInLinkLabel);
            this.createAccountPanel.Controls.Add(this.createAccountResultLabel);
            this.createAccountPanel.Controls.Add(this.createAccountButton);
            this.createAccountPanel.Controls.Add(this.signInLink);
            this.createAccountPanel.Controls.Add(this.tableLayoutPanel2);
            this.createAccountPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createAccountPanel.Location = new System.Drawing.Point(0, 0);
            this.createAccountPanel.Name = "createAccountPanel";
            this.createAccountPanel.Size = new System.Drawing.Size(554, 316);
            this.createAccountPanel.TabIndex = 10;
            this.createAccountPanel.Visible = false;
            // 
            // signInLinkLabel
            // 
            this.signInLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.signInLinkLabel.AutoSize = true;
            this.signInLinkLabel.Location = new System.Drawing.Point(19, 281);
            this.signInLinkLabel.Name = "signInLinkLabel";
            this.signInLinkLabel.Size = new System.Drawing.Size(54, 20);
            this.signInLinkLabel.TabIndex = 10;
            this.signInLinkLabel.TabStop = true;
            this.signInLinkLabel.Text = "Sign In";
            this.signInLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SignInLinkLabel_LinkClicked);
            // 
            // createAccountResultLabel
            // 
            this.createAccountResultLabel.AutoSize = true;
            this.createAccountResultLabel.Location = new System.Drawing.Point(109, 224);
            this.createAccountResultLabel.Name = "createAccountResultLabel";
            this.createAccountResultLabel.Size = new System.Drawing.Size(50, 20);
            this.createAccountResultLabel.TabIndex = 9;
            this.createAccountResultLabel.Text = "label6";
            this.createAccountResultLabel.Visible = false;
            // 
            // createAccountButton
            // 
            this.createAccountButton.AutoSize = true;
            this.createAccountButton.Location = new System.Drawing.Point(203, 259);
            this.createAccountButton.Name = "createAccountButton";
            this.createAccountButton.Size = new System.Drawing.Size(149, 30);
            this.createAccountButton.TabIndex = 8;
            this.createAccountButton.Text = "Create Account";
            this.createAccountButton.UseVisualStyleBackColor = true;
            this.createAccountButton.Click += new System.EventHandler(this.CreateAccountButton_Click);
            // 
            // signInLink
            // 
            this.signInLink.AutoSize = true;
            this.signInLink.Location = new System.Drawing.Point(15, 416);
            this.signInLink.Name = "signInLink";
            this.signInLink.Size = new System.Drawing.Size(76, 20);
            this.signInLink.TabIndex = 7;
            this.signInLink.TabStop = true;
            this.signInLink.Text = "linkLabel2";
            // 
            // SignInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 316);
            this.Controls.Add(this.mainPanel);
            this.Name = "SignInForm";
            this.Text = "Sign In";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.signInPanel.ResumeLayout(false);
            this.signInPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.createAccountPanel.ResumeLayout(false);
            this.createAccountPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button signInButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.LinkLabel createAccountLinkLabel;
        private System.Windows.Forms.Panel signInPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox createAccountPasswordTextBox;
        private System.Windows.Forms.TextBox createAccountUsernameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel createAccountPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TextBox validatePasswordTextBox;
        private System.Windows.Forms.Button createAccountButton;
        private System.Windows.Forms.LinkLabel signInLink;
        private System.Windows.Forms.Label createAccountResultLabel;
        private System.Windows.Forms.LinkLabel signInLinkLabel;
        private System.Windows.Forms.Label signInResultLabel;
    }
}