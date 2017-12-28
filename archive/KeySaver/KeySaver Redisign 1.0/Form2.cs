using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Schlüsselverwaltung;

namespace KeySaver_Redisign_1._0
{
    public partial class Form2 : Form
    {
        public string Ausg
        {
            get
            {
                string tex = "";
                tex = textBox1.Tex + " (" + DateTime.Now.ToShortDateString() + ")";
                if (checkBox1.Checked) tex += "\n---Giveaway---";
                if (textBox2.Tex != String.Empty) tex += "\nName: " + textBox2.Tex;
                if (comboBox1.comboBox1.Text.Contains("@")) tex += "\nE-Mail: " + comboBox1.comboBox1.Text;
                tex += "\nSchlüssel: " + textBox4.Tex;
                if (textBox3.Tex != String.Empty) tex += "\nKommentar: " + textBox3.Tex;
                return tex;
            }
        }
        public string Bezeichnung { get { return textBox1.Tex; } }

        public Form2(string p)
        {
            InitializeComponent();
            textBox1.Tex = p;
            panel1.Hide();
            Size = new Size(Width, Height - panel1.Height - 7);
        }
        public Form2()
        {
            InitializeComponent();
            comboBox1.comboBox1.Items.Clear();
            comboBox1.comboBox1.Items.Add("keine Email");
            foreach (var item in Infos.Adressen)
            {
                comboBox1.comboBox1.Items.Add(item);
            }
            comboBox1.comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = textBox1.Tex == String.Empty ? System.Windows.Forms.DialogResult.None : System.Windows.Forms.DialogResult.OK;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Infos.Adressen.Clear();
            List<string> items = new List<string>();
            for (int i = 1; i < comboBox1.comboBox1.Items.Count; i++)
                Infos.Adressen.Add(comboBox1.comboBox1.Items[i].ToString());
            Infos.Speichern(true);
        }
    }
}
