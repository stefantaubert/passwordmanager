using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private List<string> _gruppen = new List<string>();
        public List<string> Gruppen { get { return _gruppen; } }

        public Form2(List<string> gruppen, string gruppe)
        {
            Iniz("", "", gruppen, gruppe);
        }
        public Form2(string akt, List<string> gruppen, string gruppe)
        {
            Iniz(akt, "", gruppen, gruppe);
        }

        public Form2(string akt, string key, List<string> gruppen, string gruppe)
        {
            Iniz(akt, key, gruppen, gruppe);
        }

        private void Iniz(string akt, string key, List<string> gruppen, string gruppe)
        {
            InitializeComponent();
            textBox1.Text = akt.Trim();
            _gruppen = gruppen;
            LoadCombo();
            if (comboBox1.Items.Count > 0)
            {
                int index = comboBox1.Items.IndexOf(gruppe);
                if (index == -1)
                    comboBox1.SelectedIndex = 0;
                else comboBox1.SelectedIndex = index;
            }
            if (key != String.Empty) MessageBox.Show(key);
        }

        private void LoadCombo()
        {
            comboBox1.Items.Clear();
            for (int i = 0; i < _gruppen.Count; i++)
                comboBox1.Items.Add(_gruppen[i]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Bitte geben Sie einen Namen für die Rubrik ein!");
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("Bitte wählen Sie eine Gruppe!");
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
                DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NeueGruppe ng = new NeueGruppe(_gruppen);
            if (ng.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _gruppen.Add(ng.textBox1.Text.Trim());
                LoadCombo();
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0 && comboBox1.SelectedIndex != -1)
            {
                _gruppen.RemoveAt(comboBox1.SelectedIndex);
                LoadCombo();
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
            }
        }
    }
}
