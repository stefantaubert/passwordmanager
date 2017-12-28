using KeyManager;
using System;
using System.Windows.Forms;

namespace PM
{
    public partial class CreateEntryForm : Form
    {
        private readonly static string EmptyText = "no mail";

        public CreateEntryForm()
        {
            InitializeComponent();

            this.DrawComboBox();
            this.mailComboBox.SelectedIndex = 0;
        }

        private static string CreateContent(string label, string name, string mail, string key, string comment)
        {
            string result = string.Empty;

            result += string.Format("{0} ({1})\r\n", label, DateTime.Now.ToShortDateString());
            result += GetText("Name", name, true);
            result += (string.IsNullOrWhiteSpace(mail) || mail == EmptyText) ? string.Empty : string.Format("{0}: {1}\r\n", "E-Mail", mail);
            result += GetText("Key", key, false);
            result += GetText("Comment", comment, true);

            return result.TrimEnd('\n');
        }

        public string KeyValue
        {
            get
            {
                return CreateContent(this.labelTextBox.Text, this.nameTextBox.Text, this.mailComboBox.Text, this.keyTextBox.Text, this.commentTextBox.Text);
            }
        }

        public string Label
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
                var newMail = GuiIOHandler.CurrentModel.AddMail(this.mailComboBox.Text.Trim());

                GuiIOHandler.SaveCurrentModel();

                this.mailComboBox.Items.Add(newMail);
                this.mailComboBox.SelectedIndex = this.mailComboBox.Items.Count - 1;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void DrawComboBox()
        {
            this.mailComboBox.Items.Clear();

            this.mailComboBox.Items.Add(EmptyText);

            foreach (var item in GuiIOHandler.CurrentModel.MailAdresses)
            {
                this.mailComboBox.Items.Add(item);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Label))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid input!");

                this.DialogResult = DialogResult.None;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mailComboBox.SelectedItem != null && this.mailComboBox.Text != EmptyText)
            {
                GuiIOHandler.CurrentModel.RemoveMail(this.mailComboBox.SelectedItem as ICryptedMailAddress);

                GuiIOHandler.SaveCurrentModel();

                this.mailComboBox.Items.Remove(this.mailComboBox.SelectedItem);

                this.mailComboBox.SelectedIndex = 0;
            }
        }
    }
}
