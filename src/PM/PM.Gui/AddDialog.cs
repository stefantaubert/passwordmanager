using System;
using System.Windows.Forms;

namespace PM.Gui
{
    public partial class AddDialog : Form
    {
        private readonly static string EmptyText = "no mail";

        public Core.Entry CreatedEntry
        {
            get;
            private set;
        }

        public AddDialog()
        {
            InitializeComponent();

            this.DrawMailComboBox();
            this.mailComboBox.SelectedIndex = 0;
            this.keyTextBox.Text = CryptedRandomString.GenerateIdentifier(30, true, true, true, true);
        }

        private static string CreateContent(string label, string name, string mail, string key, string url, string comment)
        {
            string result = string.Empty;

            result += string.Format("{0} ({1})\r\n", label, DateTime.Now.ToShortDateString());
            result += GetText("Name", name, true);
            result += (string.IsNullOrWhiteSpace(mail) || mail == EmptyText) ? string.Empty : string.Format("{0}: {1}\r\n", "E-Mail", mail);
            result += GetText("Password", key, false);
            result += GetText("Url", url, true);
            result += GetText("Comment", comment, true);

            return result;
        }

        private string Content
        {
            get
            {
                return CreateContent(this.labelTextBox.Text, this.nameTextBox.Text, this.mailComboBox.Text, this.keyTextBox.Text, this.urlTextBox.Text, this.commentTextBox.Text);
            }
        }

        private string Label
        {
            get
            {
                return this.labelTextBox.Text;
            }
        }

        private static string GetText(string text, string textBox, bool emptyIfEmpty)
        {
            if (emptyIfEmpty && string.IsNullOrWhiteSpace(textBox))
            {
                return string.Empty;
            }
            else
            {
                return string.Format("{0}: {1}\r\n", text, textBox);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mailComboBox.Text.Trim() != EmptyText)
            {
                ModelLoader.CurrentModel.MailAdresses.Add(this.mailComboBox.Text);
                ModelLoader.SaveModel();

                this.mailComboBox.Items.Add(this.mailComboBox.Text);
                this.mailComboBox.SelectedIndex = this.mailComboBox.Items.Count - 1;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void DrawMailComboBox()
        {
            this.mailComboBox.SuspendLayout();

            this.mailComboBox.Items.Clear();
            this.mailComboBox.Items.Add(EmptyText);

            foreach (var item in ModelLoader.CurrentModel.MailAdresses)
            {
                this.mailComboBox.Items.Add(item);
            }

            this.mailComboBox.ResumeLayout();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Label))
            {
                var newEntry = new Core.Entry(this.Label);

                newEntry.Content = this.Content;
                ModelLoader.CurrentModel.Entries.Add(newEntry);
                this.CreatedEntry = newEntry;
                ModelLoader.SaveModel();

                if (!string.IsNullOrEmpty(this.keyTextBox.Text))
                {
                    Clipboard.Clear();
                    Clipboard.SetText(this.keyTextBox.Text);
                }               

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please enter a label!");

                this.DialogResult = DialogResult.None;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mailComboBox.SelectedItem != null && this.mailComboBox.Text != EmptyText)
            {
                ModelLoader.CurrentModel.MailAdresses.Remove(this.mailComboBox.SelectedItem.ToString());
                ModelLoader.SaveModel();

                this.mailComboBox.Items.Remove(this.mailComboBox.SelectedItem);
                this.mailComboBox.SelectedIndex = 0;
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.removeToolStripMenuItem.Enabled = ModelLoader.CurrentModel.MailAdresses.Contains(this.mailComboBox.Text);
            this.addToolStripMenuItem.Enabled = !ModelLoader.CurrentModel.MailAdresses.Contains(this.mailComboBox.Text) && this.mailComboBox.Text != EmptyText && !string.IsNullOrWhiteSpace(this.mailComboBox.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (PasswordGeneratorDialog.ShowKeyGen())
            {
                this.keyTextBox.Text = PasswordGeneratorDialog.LastGeneratedKey;
            }
        }
    }
}
