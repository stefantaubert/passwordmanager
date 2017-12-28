using System;
using Common;
using System.Windows.Forms;

namespace PM
{
    internal partial class KeyGenForm : Form
    {
        private static KeyGenForm form;

        public KeyGenForm()
        {
            InitializeComponent();

            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }

            this.Generate();
        }

        static KeyGenForm()
        {
            form = new KeyGenForm();
        }

        public static void ShowKeyGen()
        {
            form.ShowDialog();
        }

        private void Generate()
        {
            this.textBox1.Text = CryptedRandomString.GenerateIdentifier(
                (int)numericUpDown1.Value,
                checkedListBox1.GetItemChecked(0),
                checkedListBox1.GetItemChecked(1),
                checkedListBox1.GetItemChecked(2),
                checkedListBox1.GetItemChecked(3));
        }

        private void HandleGenerateClick(object sender, EventArgs e)
        {
            this.Generate();
        }

        private void HandleToClipboardClick(object sender, EventArgs e)
        {
            Clipboard.SetText(this.textBox1.Text);
        }
    }
}