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
    internal partial class ChangePasswordForm : Form
    {
        private string passwordFilePath;
        private PasswordInformation pwInfo;

        public ChangePasswordForm(PasswordInformation currentPwInfo, string passwordFilePath)
        {
            InitializeComponent();

            this.passwordFilePath = passwordFilePath;
            this.pwInfo = currentPwInfo;
            this.pwInfo.PasswordChanged += PwInfo_PasswordChanged;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                this.pwInfo.PasswordChanged -= this.PwInfo_PasswordChanged;

                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void PwInfo_PasswordChanged(object sender, Login.PasswordChangedEventArgs e)
        {
            var oldAuth = new AuthData();

            oldAuth.EnteredPasswordAsString = e.OldPassword;
            oldAuth.PasswordSalt = e.OldPasswortSalt;
            oldAuth.Success = true;

            var newAuth = new AuthData();

            newAuth.EnteredPasswordAsString = e.NewPassword;
            newAuth.PasswordSalt = e.NewPasswordSalt;
            newAuth.Success = true;

            PasswordInfoLoader.RaisePasswordChangedEvent(oldAuth, newAuth);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!pwInfo.PasswordIsCorrect(this.textBoxOld.Text))
            {
                MessageBox.Show("The current password is wrong!");
                DialogResult = DialogResult.None;
            }
            else if (this.textBoxNew.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.");
                DialogResult = DialogResult.None;
            }
            else if (string.IsNullOrEmpty(this.textBoxNew.Text) || this.textBoxNew.Text != this.textBoxNewConfirmed.Text)
            {
                MessageBox.Show("Please enter and confirm a new valid password!");
                DialogResult = DialogResult.None;
            }
          
            else if (this.textBoxNew.Text == this.textBoxOld.Text)
            {
                MessageBox.Show("Please enter a different password.");
                DialogResult = DialogResult.None;
            }
            else
            {
                pwInfo.ChangePassword(this.textBoxOld.Text, this.textBoxNew.Text, Convert.ToUInt32(this.numericUpDown1.Value));
                pwInfo.Save(this.passwordFilePath);

                MessageBox.Show("Password successfully changed!");

                DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}