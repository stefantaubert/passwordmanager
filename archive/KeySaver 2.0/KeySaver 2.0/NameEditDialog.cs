using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeySaver
{
    public partial class NameEditDialog : Form
    {
        Model model;
        public NameEditDialog()
        {
            InitializeComponent();
            this.textBoxLabel.SetCueBanner("E-Mail", false);
            this.Text = "Edit E-Mail";
        }

        public NameEditDialog(Model model, string itemLabel)
        {
            InitializeComponent();
            this.model = model;
            this.textBoxLabel.SetCueBanner("Label", false);
            this.textBoxLabel.Text = itemLabel;
            this.Text = "Edit Label";
        }

        public string NewName
        {
            get
            {
                return this.textBoxLabel.Text;
            }
        }

        private void HandleOKClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBoxLabel.Text))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid input!");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void HandleCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
