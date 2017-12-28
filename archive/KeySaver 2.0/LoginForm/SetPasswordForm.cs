using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeySaver2k15.Login
{
    internal partial class SetPasswordForm : Form
    {
        public SetPasswordForm()
        {
            InitializeComponent();
        }

        public string Password
        {
            get
            {
                return this.textBox1.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != textBox2.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please control your entries.");
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                MessageBox.Show("New password successfully created.");
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
