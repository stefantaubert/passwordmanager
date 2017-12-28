using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1;
using System.IO;

namespace Passwortverwaltung
{
    public partial class RegEx : Form
    {
        List<string> _ausg = new List<string>();
        public string Code { get { return Do.ConvertToText(_ausg.ToArray()).Trim(); } }

        public RegEx()
        {
            InitializeComponent();
        }

        private void Loade(string path)
        {
            _ausg.Clear();
            string[] lin = Do.ReadLines(path);
            for (int i = 0; i < lin.Length; i++)
                if (lin[i].Trim() != String.Empty) _ausg.Add(lin[i].Trim());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tex = textBox1.Text.Trim();
            if (File.Exists(tex))
            { Loade(tex); DialogResult = System.Windows.Forms.DialogResult.OK; }
            else { MessageBox.Show("Bitte geben Sie einen gültigen Pfad an!"); DialogResult = System.Windows.Forms.DialogResult.None; return; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Abort;
        }
    }
}
