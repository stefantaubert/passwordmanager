using Microsoft.VisualStudio.TestTools.UnitTesting;
using PM.Gui.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PM.Gui
{
    public partial class MainForm : Form
    {
        private readonly string keyStr;

        public MainForm()
        {
            InitializeComponent();

            this.keyStr = "Password: ";
            this.sideMenuListBox.DisplayMember = "Label";
            this.versionLabel.Text = String.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            this.MakeGoldenRatio();

            this.RenderItems();
            this.RenderForm();
        }

        public bool EntrySelected
        {
            get
            {
                return this.SelectedEntry != default(Core.Entry);
            }
        }

        private Core.Entry SelectedEntry
        {
            get
            {
                if (this.sideMenuListBox.SelectedItems.Count == 1)
                {
                    return this.sideMenuListBox.SelectedItem as Core.Entry;
                }
                else
                {
                    return default(Core.Entry);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutDlg = new AboutDialog())
            {
                aboutDlg.ShowDialog();
            }
        }

        private void AddEntry()
        {
            using (var createDlg = new AddDialog())
            {
                if (createDlg.ShowDialog() == DialogResult.OK)
                {
                    Assert.IsNotNull(createDlg.CreatedEntry);

                    this.filterTextBox.Text = string.Empty;

                    this.RenderItems();

                    this.sideMenuListBox.Focus();
                    this.sideMenuListBox.SelectedItem = createDlg.CreatedEntry;
                    this.ShowEditContentDialog();
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddEntry();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            deleteToolStripMenuItem.Enabled = this.EntrySelected;
            editToolStripMenuItem.Enabled = this.EntrySelected;
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            this.CopyKey();
        }

        private void CopyKey()
        {
            Assert.IsTrue(this.EntrySelected);

            var keyStart = this.richTextBox1.Text.IndexOf(keyStr);
            if (keyStart >= 0)
            {
                var secondIndex = this.richTextBox1.Text.IndexOf("\n", keyStart);

                if (secondIndex < 0)
                {
                    secondIndex = this.richTextBox1.TextLength;
                }

                Assert.IsTrue(secondIndex >= 0);

                var text = this.richTextBox1.Text.Substring(keyStart + keyStr.Length, secondIndex - keyStart - keyStr.Length);

                if (!string.IsNullOrWhiteSpace(text))
                {
                    Clipboard.Clear();
                    Clipboard.SetText(text);
                }
            }
        }

        private void datenAlsCSVExportierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExportToXML();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveSelectedEntry();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowRenameDialog();
        }

        private void ExportToXML()
        {
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Core.XmlSerialization.SerializeModel(ModelLoader.CurrentModel, saveFileDialog1.FileName);

                MessageBox.Show("Export was successful!");
            }
        }

        private void ImportFromXml()
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var importedModel = Core.XmlSerialization.DeserializeModel(this.openFileDialog1.FileName);

                ModelLoader.ReplaceCurrentModelWithImportedModel(importedModel);
                ModelLoader.SaveModel();

                this.RenderItems();
                this.RenderForm();
                this.sideMenuListBox.SelectedItem = null;

                MessageBox.Show("Import was successful!");
            }
        }

        private void importFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ImportFromXml();
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (EntrySelected)
            {
                if (e.KeyData == Keys.Delete)
                {
                    this.RemoveSelectedEntry();
                }
                else if (e.KeyData == Keys.F2)
                {
                    this.ShowRenameDialog();
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowEditContentDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.EntrySelected)
            //{
            //    this.SelectedEntry.Requests++;
            //}

            this.RenderForm();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.filterTextBox.Focus();
        }

        private void MakeGoldenRatio()
        {
            if (this.splitContainer1.Width > 0)
            {
                this.splitContainer1.SplitterDistance = (int)Math.Round(this.splitContainer1.Width / 100 * 38.2, 0);
            }
        }

        private void menuStrip1_SizeChanged(object sender, EventArgs e)
        {
            this.MakeGoldenRatio();
        }

        private void passwordGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordGeneratorDialog.ShowKeyGen();
        }

        private void RemoveSelectedEntry()
        {
            Assert.IsTrue(this.EntrySelected);

            if (MessageBox.Show(string.Format("Are you sure to delete \"{0}\"?", this.SelectedEntry.Label), "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ModelLoader.CurrentModel.Entries.Remove(this.SelectedEntry);
                ModelLoader.SaveModel();

                this.RenderItems();
                this.sideMenuListBox.SelectedItem = null;
                this.RenderForm();
            }
        }

        private void RenderForm()
        {
            this.SuspendLayout();
            this.copyBtn.Enabled = this.EntrySelected && this.SelectedEntry.Content.Contains(this.keyStr);
            this.editContentBtn.Enabled = this.EntrySelected;
            this.richTextBox1.Text = this.EntrySelected ? this.SelectedEntry.Content : "Please select an entry.";
            this.ResumeLayout();
        }

        private void RenderItems()
        {
            this.sideMenuListBox.SuspendLayout();

            this.sideMenuListBox.Items.Clear();
            var filter = this.filterTextBox.Text.ToUpperInvariant();

            foreach (var item in ModelLoader.CurrentModel.Entries.Where(s => s.Label.ToUpperInvariant().Contains(filter)).OrderBy(s => s.Created).Reverse())
            {
                this.sideMenuListBox.Items.Add(item);
            }

            this.sideMenuListBox.ResumeLayout();

            var entriesCount = ModelLoader.CurrentModel.Entries.Count;
            var filteredCount = this.sideMenuListBox.Items.Count;

            this.SuspendLayout();

            this.entriesLabel.Text = string.Format("Entries: {0}", ModelLoader.CurrentModel.Entries.Count);

            if (filteredCount < entriesCount)
            {
                this.entriesLabel.Text += string.Format(" ({0} filtered)", this.sideMenuListBox.Items.Count);
            }

            this.ResumeLayout();
        }

        private void searchTxtBox_TextChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Image = string.IsNullOrEmpty(this.filterTextBox.Text) ? Resources.search : Resources.delete;

            this.RenderItems();
            this.RenderForm();
        }

        private void ShowEditContentDialog()
        { 
            using (var dlg = new EditContentDialog(this.SelectedEntry))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.RenderForm();
                }
            }
        }

        private void ShowRenameDialog()
        {
            Assert.IsNotNull(this.SelectedEntry);

            using (var dlg = new RenameDialog(this.SelectedEntry))
            {
                dlg.ShowDialog();

                var selectedItem = this.SelectedEntry;
                this.RenderItems();
                this.sideMenuListBox.SelectedItem = selectedItem;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.ShowEditContentDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.AddEntry();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.filterTextBox.Focus();
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Enter)
            //{
            //    this.RenderItems();
            //    this.RenderForm();
            //}
      }

        private void filterTextBox_Enter(object sender, EventArgs e)
        {
            //this.pictureBox1.Image = Resources.delete;
        }

        private void filterTextBox_Leave(object sender, EventArgs e)
        {
            //this.pictureBox1.Image = Resources.search;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.filterTextBox.Text))
            {
                this.filterTextBox.Text = string.Empty;
            }

            this.filterTextBox.Focus();
        }
    }
}
