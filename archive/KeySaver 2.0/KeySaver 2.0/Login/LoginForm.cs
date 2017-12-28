namespace KeySaver
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal partial class LoginForm : Form
    {
        private readonly int KeyLength;

        private readonly int KeySubLength;

        private string fileName;

        private KeyInfo key;

        public LoginForm(string fileName)
        {
            this.KeyLength = 1024;
            this.KeySubLength = 64;
            this.fileName = fileName;

            this.InitializeComponent();
        }

        public string EnteredPassword
        {
            get
            {
                return this.textBox1.Text;
            }

            private set
            {
                this.textBox1.Text = value;
            }
        }

        public bool LoadKey()
        {
            if (File.Exists(this.fileName))
            {
                try
                {
                    this.key = this.key.DezerializeFile(this.fileName);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Error in reading keyfile!");
                }
            }
            else
            {
                using (var dlg = new SetPasswordForm())
                {
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var key = RandomString.Generate(KeyLength);
                        var tmp = new KeyInfo(key.Encrypt(dlg.Password), key.Substring(0, KeySubLength));

                        if (tmp.SerializeStream(this.fileName))
                        {
                            this.key = tmp;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Error in creating keyfile!");
                        }
                    }
                }
            }

            return false;
        }

        public bool CheckPassword(string pwd)
        {
            return this.key.Key.Decrypt(pwd).StartsWith(this.key.StartWith);
        }

        private void Login()
        {
            if (this.CheckPassword(this.EnteredPassword))
            {
                Model.Key = this.key.Key.Decrypt(this.EnteredPassword);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please enter your right password!");
                textBox1.Focus();
                textBox1.Select(0, textBox1.Text.Length);
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void ChangePassword()
        {
            if (this.CheckPassword(this.EnteredPassword))
            {
                using (var dlg = new SetPasswordForm())
                {
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var old = this.key.Key.Decrypt(this.EnteredPassword);
                        var tmp = new KeyInfo(old.Encrypt(dlg.Password), old.Substring(0, KeySubLength));

                        if (tmp.SerializeStream(this.fileName))
                        {
                            this.key = tmp;
                        }
                        else
                        {
                            MessageBox.Show("Error in changing keyfile!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter your right password!");
            }
        }

        private void Reset()
        {
            if (MessageBox.Show("Are you sure to delete all keydata?", "Sure?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    File.Delete(this.fileName);
                    this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
                }
                catch
                {
                    MessageBox.Show("Error in deleting keydata!");
                }
            }
        }

        private void HandleResetClick(object sender, EventArgs e)
        {
            this.Reset();
        }

        private void HandleLoginClick(object sender, EventArgs e)
        {
            this.Login();
        }

        private void HandleChangePasswordClick(object sender, EventArgs e)
        {
            this.ChangePassword();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}