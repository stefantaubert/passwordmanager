using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginForm.Key;

namespace LoginForm
{
    internal partial class LoginForm : Form
    {
        private KeyManager loginMgr;
        public LoginForm(KeyManager mgr, string pwd)
            : this(mgr)
        {
            this.EnteredPassword = pwd;
            this.textBox1.Select(0, this.textBox1.Text.Length);
        }

        public LoginForm(KeyManager mgr)
        {
            InitializeComponent();

            this.loginMgr = mgr;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.loginMgr.IsRightPassword(this.EnteredPassword))
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else
            {
                MessageBox.Show("Wrong password!");
                textBox1.Focus();
                textBox1.Select(0, textBox1.Text.Length);
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void HandleChangePasswordClick(object sender, EventArgs e)
        {
            if (LoginKey.IsRightPassword(this.EnteredPassword))
                LoginManager.ShowPasswordCreator();
            else
                MessageBox.Show("Please enter your right password!");
        }

        private void HandleResetClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete all keydata?", "Sure?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                LoginKey.Delete();

                DialogResult = System.Windows.Forms.DialogResult.Ignore;
            }
        }
    }
}
