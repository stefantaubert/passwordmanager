using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        Infos _info;
        public Login(Infos info)
        {
            InitializeComponent();
            _info = info;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_info.Anmelden(textBox1.Text.Trim()))
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else
            {
                MessageBox.Show("Falsches Passwort!");
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
