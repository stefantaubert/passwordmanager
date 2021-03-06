﻿using System;
using System.Windows.Forms;

namespace PM.Gui
{
    public partial class CreateEntryForm : Form
    {
        private readonly static string EmptyText = "no mail";

        public CreateEntryForm()
        {
            InitializeComponent();

            this.DrawComboBox();
            this.mailComboBox.SelectedIndex = 0;
            this.keyTextBox.Text = CryptedRandomString.GenerateIdentifier(30, true, true, true, true);
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

        private string Content
        {
            get
            {
                return CreateContent(this.labelTextBox.Text, this.nameTextBox.Text, this.mailComboBox.Text, this.keyTextBox.Text, this.commentTextBox.Text);
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

        private void DrawComboBox()
        {
            this.mailComboBox.Items.Clear();

            this.mailComboBox.Items.Add(EmptyText);

            foreach (var item in ModelLoader.CurrentModel.MailAdresses)
            {
                this.mailComboBox.Items.Add(item);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Label))
            {
                var newEntry = new Core.Entry(this.Label);

                newEntry.Content = this.Content;

                ModelLoader.CurrentModel.Entries.Add(newEntry);
                ModelLoader.SaveModel();

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
                ModelLoader.CurrentModel.MailAdresses.Remove(this.mailComboBox.SelectedItem.ToString());

                ModelLoader.SaveModel();

                this.mailComboBox.Items.Remove(this.mailComboBox.SelectedItem);

                this.mailComboBox.SelectedIndex = 0;
            }
        }
    }
}
