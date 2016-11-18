using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PM.Core;

namespace PM.Gui
{
    public partial class RenameDialog : Form
    {
        private Entry entry;

        public RenameDialog(Core.Entry entry)
        {
            InitializeComponent();

            this.Text = string.Format("Rename \"{0}\"", entry.Label);

            this.entry = entry;
            this.textBox1.Text = entry.Label;
            this.textBox1.SelectAll();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                this.entry.Label = this.textBox1.Text;
                ModelLoader.SaveModel();

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please enter a name!");
                this.DialogResult = DialogResult.None;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
