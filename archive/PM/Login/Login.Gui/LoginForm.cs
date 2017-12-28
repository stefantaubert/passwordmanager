using Login;
using LoginGui.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginGui
{
    internal partial class LoginForm : Form
    {
        private readonly string passwordFilePath;
        private AuthData authenticationResult;
        private PasswordInformation pwInfo;

        public LoginForm(string passwordFilePath)
        {
            InitializeComponent();

            this.passwordFilePath = passwordFilePath;

            this.pwInfo = PasswordInformation.Load(this.passwordFilePath);
            this.authenticationResult = new AuthData();
        }

        public AuthData AuthenticationResult
        {
            get
            {
                return this.authenticationResult;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new ChangePasswordForm(this.pwInfo, this.passwordFilePath))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.SelectAll();
                }
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                this.authenticationResult.Success = false;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (this.pwInfo.PasswordIsCorrect(this.textBox1.Text))
            {
                this.authenticationResult.EnteredPasswordAsString = this.textBox1.Text;
                this.authenticationResult.PasswordSalt = pwInfo.PasswordSalt;
                this.authenticationResult.Success = true;

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("You entered a wrong password!");
                this.textBox1.SelectAll();
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
