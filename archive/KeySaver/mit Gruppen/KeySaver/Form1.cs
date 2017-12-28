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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Zentrale _zenti = new Zentrale(Application.StartupPath + "\\saves.xml");
        int _index;
        string _tmpAktText = "Wählen Sie eine Rubrik";
        public Form1()
        {
            _zenti.listviewChange += new Action<String>(_zenti_listviewChange);
            if (_zenti.InitializeComponent(treeView1))
            {
                InitializeComponent();
                _zenti.LoadNodes(treeView1);
                _index = -1;
            }
            else Close();
        }
        private void _zenti_listviewChange(String obj)
        {
            richTextBox1.Text = "Wählen Sie eine Rubrik";
            Refresh();
        }
        private void Save()
        {
            _zenti.Save();
            _tmpAktText = richTextBox1.Text;
            button8.Enabled = false;
        }
        // Speichern nach textänderung
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_index != -1)
            {
                if (_tmpAktText != richTextBox1.Text) button8.Enabled = true;
                else button8.Enabled = false;
            }
        }
        // Add
        private void button5_Click(object sender, EventArgs e)
        {
            string s = _zenti.Add(treeView1);
            // Erfolgereich
            if (s != String.Empty)
            {
                richTextBox1.Text = s;
                richTextBox1.Focus();
                richTextBox1.Select(richTextBox1.TextLength, 0);
            }
        }
        // Generator Ein/Aus
        private void button6_Click_1(object sender, EventArgs e)
        {
            if (button6.Tag.ToString() == "aus")
            {
                panel2.Height = panel2.Size.Height + 5 + passwortMaker1.Height;
                button6.Tag = String.Empty;
            }
            else
            {
                panel2.Height = panel2.Size.Height - 5 - passwortMaker1.Height;
                button6.Tag = "aus";
            }
        }
        // Save
        private void button8_Click(object sender, EventArgs e)
        {
            _zenti.Info.Change(_index, richTextBox1.Text);
            Save();
        }
        // Kopieren
        private void button1_Click(object sender, EventArgs e)
        {
            _zenti.Kopieren();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode == null) return;
            _zenti.Edit(_index, treeView1);
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _index = -1;
            // Nichts
            if (treeView1.SelectedNode == null) return;
            // Überbegriffe
            if (treeView1.SelectedNode.Tag == null)
            {
                _tmpAktText = richTextBox1.Text = "Wählen Sie eine Rubrik";
                return;
            }
            // Item
            _index = (int)treeView1.SelectedNode.Tag;
            // Key anzeigen
            richTextBox1.Text = _zenti.Info.GetKey(_index);
            _tmpAktText = richTextBox1.Text;
            button8.Enabled = false;
        }

        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                if (_index != -1)
                {
                    _zenti.Remove(_index, treeView1);
                    _index = -1;
                }
        }
        #region Suchen
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Suchen";
            }
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Suchen")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
                textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _zenti.Suchen(textBox1.Text.Trim(), textBox1.ForeColor, treeView1);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.Select(0, 0);
            textBox1.BringToFront();
        }

        // alt
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //_zenti.Info.Gruppe[_index] = e.ClickedItem.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_index != -1 && treeView1.Nodes.Count != 0)
                _zenti.Remove(_index, treeView1);
        }
    }
}