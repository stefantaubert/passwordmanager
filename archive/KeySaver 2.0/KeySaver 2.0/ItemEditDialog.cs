namespace KeySaver
{
    using System;
    using System.Windows.Forms;

    internal partial class ItemEditDialog : Form
    {
        private readonly string EmptyText;

        public ItemEditDialog()
        {
            InitializeComponent();
            this.EmptyText = "no mail";
            this.textBoxName.SetCueBanner("Name", false);
            this.textBoxLabel.SetCueBanner("Label", false);
            this.textBoxKey.SetCueBanner("Key", false);
            this.textBoxComment.SetCueBanner("Comment", false);

            this.DrawComboBox();
            this.comboBox1.SelectedIndex = 0;
        }

        public string KeyValue
        {
            get
            {
                string result = string.Empty;

                result += string.Format("{0} ({1})\r\n", this.textBoxLabel.Text, DateTime.Now.ToShortDateString());
                result += this.GetText("Name", this.textBoxName, true);
                result += (string.IsNullOrWhiteSpace(comboBox1.Text) || comboBox1.Text == this.EmptyText) ? string.Empty : string.Format("{0}: {1}\r\n", "E-Mail", comboBox1.Text);
                result += this.GetText("Key", this.textBoxKey, false);
                result += this.GetText("Comment", this.textBoxComment, true);

                return result.TrimEnd('\n');
            }
        }

        public string Label
        {
            get
            {
                return this.textBoxLabel.Text;
            }
        }

        private void DrawComboBox()
        {
            this.comboBox1.Items.Clear();

            this.comboBox1.Items.Add(this.EmptyText);

            foreach (var item in Model.CurrentModel.Mails)
            {
                this.comboBox1.Items.Add(item);
            }
        }

        private string GetText(string text, TextBox textBox, bool emptyIfEmpty)
        {
            if (emptyIfEmpty && string.IsNullOrWhiteSpace(textBox.Text))
            {
                return string.Empty;
            }
            else
            {
                return string.Format("{0}: {1}\r\n", text, textBox.Text);
            }
        }

        private void HandleCancelClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void HandleOKClick(object sender, EventArgs e)
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

        private void HandleAddEmailClick(object sender, EventArgs e)
        {
            Model.CurrentModel.AddMail(this.comboBox1.Text.Trim());
            this.DrawComboBox();
            this.comboBox1.SelectedIndex = this.comboBox1.Items.Count - 1;
        }

        private void AddMail()
        {
          
            //using (var dlg = new NameEditDialog())
            //{
            //    if (dlg.ShowDialog() == DialogResult.OK)
            //    {
            //        if (dlg.NewName.Contains("@"))
            //        {
            //            this.model.AddMail(dlg.NewName.Trim());
            //            this.DrawComboBox();
            //            this.comboBox1.SelectedIndex = this.comboBox1.Items.Count - 1;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please insert a valid mail adress!");
            //            this.AddMail();
            //        }
            //    }
            //}
        }

        private void HandleRemoveMailClick(object sender, EventArgs e)
        {
            //if (this.comboBox1.Text.Contains("@"))
            //{
            Model.CurrentModel.RemoveMail(this.comboBox1.Text);
            this.DrawComboBox();
            //}
        }
    }
}