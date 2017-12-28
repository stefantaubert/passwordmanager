using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PM
{
    public partial class CreatePasswordForm : Form
    {
        public CreatePasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox1.Text) && this.textBox1.Text == this.textBox2.Text)
            {
                GuiIOHandler.CreateModelFile(this.textBox1.Text);
                MessageBox.Show("Password successfully created!");
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please enter and confirm a valid password!");
                DialogResult = DialogResult.None;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
