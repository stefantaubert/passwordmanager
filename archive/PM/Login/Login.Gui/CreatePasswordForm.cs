using Login;
using LoginGui.Properties;
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
    internal partial class CreatePasswordForm : Form
    {
        private AuthData authenticationResult;

        private readonly string passwordFilePath;

        public CreatePasswordForm(string passwordFilePath)
        {
            InitializeComponent();

            this.passwordFilePath = passwordFilePath;
            this.authenticationResult = new AuthData();
        }

        public AuthData AuthenticationResult
        {
            get
            {
                return this.authenticationResult;
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (this.textBoxPass.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.");
                DialogResult = DialogResult.None;
            }
             else   if (!string.IsNullOrEmpty(this.textBoxPass.Text) && this.textBoxPass.Text == this.textBoxPassConfirmed.Text)
            {
                var createdPw = PasswordInformation.Create(this.passwordFilePath, this.textBoxPass.Text, Convert.ToUInt32(this.numericUpDown1.Value));

                MessageBox.Show("Password successfully created!");

                this.authenticationResult.EnteredPasswordAsString = this.textBoxPass.Text;
                this.authenticationResult.PasswordSalt = createdPw.PasswordSalt;
                this.authenticationResult.Success = true;

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please enter and confirm a valid password!");
                DialogResult = DialogResult.None;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.authenticationResult.Success = false;
            DialogResult = DialogResult.Cancel;
        }
    }
}