using System;
using System.Windows.Forms;

namespace KeySaver
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
            if ((textBox1.Text != textBox2.Text) || textBox1.Text == string.Empty || textBox2.Text == string.Empty)
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