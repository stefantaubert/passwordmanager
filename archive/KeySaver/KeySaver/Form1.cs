using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using Passwortverwaltung;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Centrale _zenti = new Centrale(Do.PathXml);
        int _index;
        private const string beginnReg = "REG#A", endReg = "REG#B";
        bool _falsch = false;
        string _tmpAktText = "Wählen Sie eine Rubrik";
        bool _isShowAKey = false;

        public Form1()
        {
            _zenti.listviewChange += new Action<String>(_zenti_listviewChange);
            if (_zenti.Load(Do.PathKey))
            {
                InitializeComponent();
                textBox1.Width = listView1.Width;
                _zenti.LoadNodes(listView1);
                _index = -1;
                listView1.Columns[0].Text = "Rubriken (" + listView1.Items.Count + ")";
                _zenti.Save();
                //        toolStripMenuItemPaste.Enabled = false;
            }
            else Close();
        }
        private void _zenti_listviewChange(String obj)
        {
            //  toolStripMenuItemPaste.Enabled = false;
            richTextBox1.Text = "Wählen Sie eine Rubrik";
            textBox1.Text = String.Empty;
            textBox1_Leave_1(textBox1, new EventArgs());
            Refresh();
            _isShowAKey = false;
        }
        private void Save()
        {
            _zenti.Save();
            _tmpAktText = richTextBox1.Text;
            saveToolStripMenuItem.Enabled = false;
        }
        // Textänderung prüfen
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_index != -1 && !_falsch)
            {
                if (_tmpAktText != richTextBox1.Text && _isShowAKey) saveToolStripMenuItem.Enabled = true;
                else saveToolStripMenuItem.Enabled = false;
            }
        }
        #region Suchen


        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty && !textBox1.Focused)
            {
                textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Suchen";
            }
        }
        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "Suchen")
            {
                textBox1.Text = String.Empty;
                textBox1.ForeColor = Color.Black;
                textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            _zenti.Suchen(textBox1.Text.Trim(), textBox1.ForeColor, pwSearchBool.Checked, listView1);
            listView1.Columns[0].Text = "Rubriken (" + listView1.Items.Count + ")";
        }

        #endregion
        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_index != -1 && listView1.Items.Count != 0)
                _zenti.Remove(_index, listView1);
        }
        private void GetAtIndex(int listInde)
        {
            foreach (ListViewItem item in listView1.Items) if (item.ForeColor != Color.Black) item.ForeColor = Color.Black;
            listView1.Items[listInde].ForeColor = Color.Red;
            // Key anzeigen
            richTextBox1.Text = _zenti.Info.GetKey(_index);
            if (richTextBox1.Text.StartsWith("Falscher")) _falsch = true;
            else _falsch = false;
            _tmpAktText = richTextBox1.Text;
            saveToolStripMenuItem.Enabled = false;
            richTextBox1.Focus();
            richTextBox1.Select(richTextBox1.TextLength, 0);
            _isShowAKey = true;
        }

        private void treeView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            löschenToolStripMenuItem.Visible = false;
            // toolStripMenuItemPaste.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            _index = -1;
            _isShowAKey = false;
            // Nichts
            if (listView1.SelectedItems.Count == 0)
            // return;
            { richTextBox1.ReadOnly = true; return; }
            richTextBox1.ReadOnly = false;
            // Überbegriffe
            if (listView1.SelectedItems[0].Tag == null)
            {
                _tmpAktText = richTextBox1.Text = "Wählen Sie eine Rubrik";
                return;
            }
            // Item
            _index = (int)listView1.SelectedItems[0].Tag;
            löschenToolStripMenuItem.Visible = true;
            //toolStripMenuItemPaste.Enabled = true;
            // Key anzeigen
            GetAtIndex(listView1.SelectedItems[0].Index);
        }
        private void treeView1_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                if (_index != -1)
                {
                    _zenti.Remove(_index, listView1);
                    _index = -1;
                    listView1.Columns[0].Text = "Rubriken (" + listView1.Items.Count + ")";
                }
        }
        // Change
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int ind = listView1.SelectedItems[0].Index;
                _zenti.Edit(_index, listView1);
                GetAtIndex(ind);
            }
        }
        // Add
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool showPassGen;
            string s = _zenti.Add(listView1, out showPassGen);
            // Erfolgereich
            if (s != String.Empty)
            {
                _index = 0;
                GetAtIndex(_index);
                listView1.Columns[0].Text = "Rubriken (" + listView1.Items.Count + ")";
                if (showPassGen && showGeneratorToolStripMenuItem.Tag.ToString() == String.Empty) showGeneratorToolStripMenuItem_Click(showGeneratorToolStripMenuItem, new EventArgs());
            }
        }
        // Generator Ein/Aus
        private void showGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showGeneratorToolStripMenuItem.Tag.ToString() == "aus")
            {
                //  showGeneratorToolStripMenuItem.Text = "Passwortgenerator einblenden";
                splitContainer1.Height = splitContainer1.Size.Height + 10 + passwortMaker1.Height;
                showGeneratorToolStripMenuItem.Tag = String.Empty;
            }
            else
            {
                // showGeneratorToolStripMenuItem.Text = "Passwortgenerator ausblenden";
                splitContainer1.Height = splitContainer1.Size.Height - 10 - passwortMaker1.Height;
                showGeneratorToolStripMenuItem.Tag = "aus";
            }
        }
        // Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zenti.Info.Change(_index, richTextBox1.Text);
            Save();
        }
        private void löschenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (_index != -1)
            {
                _zenti.Remove(_index, listView1);
                _index = -1;
                listView1.Columns[0].Text = "Rubriken (" + listView1.Items.Count + ")";
            }
        }
        private void pwSearchBool_Click(object sender, EventArgs e)
        {
            textBox1_TextChanged_1(textBox1, new EventArgs());
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.IndexOf(richTextBox1.SelectedText), richTextBox1.SelectedText.Length);
            richTextBox1.Text += Clipboard.GetText();
            richTextBox1.Select(richTextBox1.Text.Length, 0);
        }

        private void toolStripMenuItem3Clear_Click(object sender, EventArgs e)
        {
            textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "Suchen";
            _index = 0;
            _zenti.LoadNodes(listView1);
            treeView1_SelectedIndexChanged(null, new EventArgs());
            //  toolStripMenuItemPaste.Enabled = false;
        }

        private void saveToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
            {
                saveToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.0f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                saveToolStripMenuItem.ForeColor = Color.Red;
            }
            else
            {
                saveToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                saveToolStripMenuItem.ForeColor = Color.Black;
            }
        }
        // Kopieren
        private void alleEinträgeKopiernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zenti.Kopieren();
            MessageBox.Show("Alle Informationen wurden erfolgreich in die Zwischenablage kopiert!\nSie können diesen Text jetzt in einem Texteditor einfügen und ausdrucken.");

        }

        private void eintragHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_isShowAKey) return;
            RegEx regE = new RegEx();
            if (regE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (regE.Code != String.Empty) richTextBox1.Text += "\nRegistry Eintrag:\n" + beginnReg + regE.Code + endReg;
            }
        }

        private void eintragAktivierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_isShowAKey || !richTextBox1.Text.Contains(beginnReg) || !richTextBox1.Text.Contains(endReg)) { MessageBox.Show("Es wurde kein Registryeintrag gefunden!"); return; }
            int indA = richTextBox1.Text.IndexOf(beginnReg) + beginnReg.Length;
            int indB = richTextBox1.Text.IndexOf(endReg);
            if (indB <= indA) return;
            string code = richTextBox1.Text.Substring(indA, indB - indA);
            string tmpPath = Application.StartupPath + "\\Registryeintrag hinzufügen.reg";
            Do.WriteFile(tmpPath, code);
            if (MessageBox.Show("Folgende Registryinformationen wurden geschrieben:\n\n" + code, "Fortsetzen?", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel) return;
            Process prc = new Process();
            prc.StartInfo.FileName = tmpPath;
            prc.Start();
        }

        private void exportierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            fBD.Description = "Der Ordner, in dem die 3 Dateien gespeichert werden sollen.";
            if (fBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _zenti.Save(fBD.SelectedPath);
                MessageBox.Show("Daten wurden erfolgreich exportiert!");
            }
        }

        private void importierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            fBD.Description = "Der Ordner, aus dem die 3 Dateien geladen werden sollen.";
            if (fBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //    _zenti.Save(fBD.SelectedPath);
                MessageBox.Show("Daten wurden nicht erfolgreich importiert!");
            }
        }
    }
}