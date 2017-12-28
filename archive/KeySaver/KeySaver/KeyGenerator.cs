using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class KeyGenerator : Form
    {
        public KeyGenerator()
        {
            InitializeComponent();
        }
        string Generate(bool grB, bool klB, bool zahl, bool sonder, int lenght)
        {
            string kleinbuchstaben = "abcdefghijklmnopqrstuvwxyz";
            string großbuchstaben = kleinbuchstaben.ToUpper();
            string sonderzeichen = "&=ß+-#{[]}!|.";
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
            return SpezialString(zeichen, lenght);
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
        public static void WriteFile(string pfad, string[] text)
        {
            StreamWriter sR = new StreamWriter(pfad);
            for (int i = 0; i < text.Length; i++)
            {
                string zeile = text[i].Trim(' ');
                if (zeile != String.Empty)
                    sR.WriteLine((text[i]).Trim());
            }
            sR.Dispose();
            sR.Close();
        }
        public string Verschlüsseln(string text, string schlüssel)
        {
            string ausg = String.Empty;
            RijndaelManaged manager = new RijndaelManaged();
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            Byte[] schluessel = provider.ComputeHash(Encoding.UTF8.GetBytes(schlüssel));
            provider.Clear();
            manager.Key = schluessel;
            manager.GenerateIV();
            Byte[] iv = manager.IV;
            MemoryStream stream = new MemoryStream();
            stream.Write(iv, 0, iv.Length);
            CryptoStream cs = new CryptoStream(stream,
                manager.CreateEncryptor(), CryptoStreamMode.Write);
            Byte[] data = Encoding.UTF8.GetBytes(text);
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();
            Byte[] encdata = stream.ToArray();
            ausg = Convert.ToBase64String(encdata);
            cs.Close();
            manager.Clear();
            return ausg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text) MessageBox.Show("Bitte prüfen Sie Ihre Angaben!");
            else if (MessageBox.Show("Falls Sie bereits einen Schlüssel mit Speicherinhalten hatten, werden diese nicht mehr abrufbar sein!\n\nWollen Sie Fortfahren?", "Hinweis", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                string _startsWith = Generate(true, true, true, true, 1000);
                string _key = Verschlüsseln(_startsWith, textBox1.Text);
                WriteFile(Do.PathKey, new string[] { _key, _startsWith.Substring(0, 250) });
                DialogResult = System.Windows.Forms.DialogResult.OK;
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Abort;
        }
    }
}
