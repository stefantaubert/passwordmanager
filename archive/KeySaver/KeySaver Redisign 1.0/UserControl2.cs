using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KeySaver_Redisign_1._0
{
    public partial class UserControl2 : UserControl
    {
        Random _rnd = new Random();
        public UserControl2()
        {
            InitializeComponent();
            button1_Click(button1, new EventArgs());
        }
        public string SpezialString(string zeichen, int length)
        {
            string retVal = String.Empty;
            string possibleValues = zeichen;
            for (int i = 0; i < length; i++)
            {
                string s = possibleValues.Substring(_rnd.Next(0, possibleValues.Length), 1);
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
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Generate(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked);
            if (textBox1.Text != String.Empty)
                Clipboard.SetText(textBox1.Text);
           // textBox1.Focus();
           // textBox1.Select(0, textBox1.TextLength);
        }
    }
}
