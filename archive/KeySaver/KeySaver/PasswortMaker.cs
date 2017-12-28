using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class PasswortMaker : UserControl
    {
        public PasswortMaker()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Generate(checkBox1.Checked, checkBox2.Checked, checkBox4.Checked, checkBox3.Checked);
            if (textBox2.Text != String.Empty)
                Clipboard.SetText(textBox2.Text);
            textBox2.Focus();
            textBox2.Select(0, textBox2.TextLength);
        }
        public string SpezialString(string zeichen, int length)
        {
            string retVal = String.Empty;
            string possibleValues = zeichen;
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                string s = possibleValues.Substring(rnd.Next(0, possibleValues.Length), 1);
                retVal = retVal + s;
            }
            return retVal;
        }

        string Generate(bool grB, bool klB, bool zahl, bool sonder)
        {
            string kleinbuchstaben = "abcdefghijklmnopqrstuvwxyz";
            string großbuchstaben = kleinbuchstaben.ToUpper();
            string sonderzeichen = "&=ß+-#{[]}!|.@<>";
            string zahlen = "0123456789";
            //string spezial = "`´~,._|<>µ²³{?<>[]}\\ \"!";
            string zeichen = String.Empty;

            if (!klB && !grB && !sonder && !zahl)
            {
                MessageBox.Show("Bitte wählen Sie mindestens einen Parameter!");
                return String.Empty;
            }
            if (klB) zeichen += kleinbuchstaben;
            if (grB) zeichen += großbuchstaben;
            if (sonder) zeichen += sonderzeichen;
            if (zahl) zeichen += zahlen;
            return SpezialString(zeichen, (int)numericUpDown1.Value);
        }
    }
}
