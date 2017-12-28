using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class NeueGruppe : Form
    {
        private List<string> _gruppen;

        public NeueGruppe(List<string> _gruppen)
        {
            this._gruppen = _gruppen;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_gruppen.Contains(textBox1.Text.Trim()))
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else
            {
                MessageBox.Show("Diese Gruppe existiert bereits!");
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Abort;
        }
    }
}
