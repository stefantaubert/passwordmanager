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
    public partial class EditContentDialog : Form
    {
        private Entry entry;

        public EditContentDialog(Core.Entry entry)
        {
            InitializeComponent();

            this.entry = entry;

            this.richTextBox1.Text = entry.Content;
            this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
            this.richTextBox1.Focus();

            this.Text = string.Format("Edit content of \"{0}\"", entry.Label);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.entry.Content != this.richTextBox1.Text)
            {
                this.entry.Content = this.richTextBox1.Text;
                ModelLoader.SaveModel();
            }

            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PasswordGeneratorDialog.ShowKeyGen();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.richTextBox1.SelectedText))
            {
                Clipboard.Clear();
                Clipboard.SetText(this.richTextBox1.SelectedText);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = Clipboard.GetText();

            this.richTextBox1.SelectedText = text;
        }
    }
}
