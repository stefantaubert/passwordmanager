using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        string pathToCombo = Do.PathEmails;
        string _tmpKey = String.Empty;

        public Form2(string p)
        {
            InitializeComponent();
            textBox1.Text = p;
            panel1.Hide();
            Size = new Size(Width, Height - panel1.Height - 7);
        }
        public Form2()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("keine Email");
            comboBox1.SelectedIndex = 0;
            if (!File.Exists(pathToCombo))
            {
                Do.FileReset(pathToCombo);
                return;
            }
            RSA rsa = new RSA(false);
            foreach (var item in Do.ReadLines(pathToCombo))
            {
                if (item == String.Empty) continue;
                string entschl = item;
                entschl = rsa.Entschlüsseln(item, Application.CompanyName).Trim();
                if (entschl != String.Empty && entschl.Contains("@"))
                    comboBox1.Items.Add(entschl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Bitte geben Sie einen Namen für die Rubrik ein!");
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                if (_tmpKey != String.Empty) Clipboard.SetText(_tmpKey);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void Save()
        {
            List<string> items = new List<string>();
            RSA rsa = new RSA(false);
            for (int i = 1; i < comboBox1.Items.Count; i++)
                items.Add(rsa.Verschlüsseln(comboBox1.Items[i].ToString(), Application.CompanyName));
            Do.WriteFile(Do.PathEmails, items.ToArray());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddEmail aE = new AddEmail();
            if (aE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                comboBox1.Items.Add(aE.textBox1.Text);
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
                Save();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0 && comboBox1.Text.Contains("@") && (MessageBox.Show("Wollen Sie die E-Mail " + "\"" + comboBox1.Text + "\" wirklich entfernen?", "Achtung", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes))
            {
                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                comboBox1.SelectedIndex = 0;
                Save();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _tmpKey = Clipboard.GetText().Trim();
        }
    }
}
