using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSnifferApp
{
    public partial class SignInForm : Form
    {
        public AccountManager AccountManager { get; init; }

        public bool IsSignedIn { get; private set; } = false;

        public string Username { get; private set; } = string.Empty;

        public SignInForm()
        {
            InitializeComponent();
        }

        private void CreateNewAccountLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            signInResultLabel.Text = string.Empty;

            signInPanel.Visible = false;
            createAccountPanel.Visible = true;

            Text = "Create Account";
        }

        private void SignInLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            createAccountResultLabel.Text = string.Empty;

            createAccountPanel.Visible = false;
            signInPanel.Visible = true;

            Text = "Sign In";
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(password))
                return;

            if (AccountManager.UsernameExists(username))
            {
                if (AccountManager.IsPasswordCorrect(username, password))
                {
                    signInResultLabel.ForeColor = Color.Green;
                    signInResultLabel.Text = "Signed In.";
                    signInResultLabel.Visible = true;

                    IsSignedIn = true;
                    Username = username;

                    signInButton.Enabled = false;
                    createAccountLinkLabel.Enabled = false;

                }
                else
                {
                    signInResultLabel.ForeColor = Color.Red;
                    signInResultLabel.Text = "Wrong username or password.";
                    signInResultLabel.Visible = true;

                    IsSignedIn = false;
                }
            }
            else
            {
                signInResultLabel.ForeColor = Color.Red;
                signInResultLabel.Text = "Wrong username or password.";
                signInResultLabel.Visible = true;

                IsSignedIn = false;
            }
        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            var username = createAccountUsernameTextBox.Text;
            var password = createAccountPasswordTextBox.Text;
            var validatedPassword = validatePasswordTextBox.Text;
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || 
                string.IsNullOrWhiteSpace(validatedPassword))
                return;

            if (password != validatedPassword)
            {
                createAccountResultLabel.ForeColor = Color.Red;
                createAccountResultLabel.Text = "Passwords do not match.";
                createAccountResultLabel.Visible = true;

                return;
            }

            if (AccountManager.UsernameExists(username))
            {
                createAccountResultLabel.ForeColor = Color.Red;
                createAccountResultLabel.Text = "Username taken.";
                createAccountResultLabel.Visible = true;

                IsSignedIn = false;
            }
            else
            {
                AccountManager.RegisterUser(username, password);

                createAccountResultLabel.ForeColor = Color.Green;
                createAccountResultLabel.Text = "Created account successfully.";
                createAccountResultLabel.Visible = true;


                IsSignedIn = true;
                Username = username;

                createAccountButton.Enabled = false;
                signInLinkLabel.Enabled = false;
            }
        }
    }
}
