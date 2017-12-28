using System;
using System.Windows.Forms;

namespace PM.Gui
{
    internal partial class PasswordGeneratorDialog : Form
    {
        private static PasswordGeneratorDialog form;

        public static string LastGeneratedKey
        {
            get
            {
                return form.keyTextBox.Text;
            }
        }

        public PasswordGeneratorDialog()
        {
            InitializeComponent();

            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }

            this.Generate();
        }

        static PasswordGeneratorDialog()
        {
            form = new PasswordGeneratorDialog();
        }

        public static bool ShowKeyGen()
        {
            return form.ShowDialog() == DialogResult.OK;
        }

        private void Generate()
        {
            this.keyTextBox.Text = CryptedRandomString.GenerateIdentifier(
                (int)numericUpDown1.Value,
                checkedListBox1.GetItemChecked(0),
                checkedListBox1.GetItemChecked(1),
                checkedListBox1.GetItemChecked(2),
                checkedListBox1.GetItemChecked(3));
        }

        private void HandleGenerateClick(object sender, EventArgs e)
        {
        }

        private void HandleToClipboardClick(object sender, EventArgs e)
        {
            Clipboard.SetText(this.keyTextBox.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Generate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.keyTextBox.Text))
            {
                Clipboard.SetText(this.keyTextBox.Text);
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}