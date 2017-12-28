using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public class Centrale
    {
        Infos _infos = new Infos();
        private string _pathXML;

        public Action<string> listviewChange { get; set; }

        public Infos Info { get { return _infos; } set { _infos = value; } }

        public Centrale(string pathXML) { this._pathXML = pathXML; }

        public bool Load(string key)
        {
            ErstelleOrdner();
            if (!File.Exists(_pathXML)) { _infos = new Infos(); Save(); }
            else _infos = XmlSerialisierung<Infos>.Deserialisieren(_pathXML);

            if (!_infos.Ini(key))
            {
                KeyGenerator kG = new KeyGenerator();
                if (kG.ShowDialog() != DialogResult.OK) return false;
                else _infos.Ini(key);
            }

            DialogResult fd = new Login(_infos).ShowDialog();
            if (fd != System.Windows.Forms.DialogResult.OK) return false;

            List<string> ausg = new List<string>();

            for (int i = 0; i < _infos.Namen.Count; i++)
            {
                ausg.Add(_infos.Namen[i] = new RSA(false).Entschlüsseln(_infos.Namen[i], "WindowsFormsApplication1")); //+ "\n-> " +
                //_infos.GetKey(i) + "\n\n");
            }
            Do.WriteFile(@"C:\Users\Admin\Desktop\Neues Textdokument.txt", ausg.ToString());
            return true;
        }

        public void Save()
        {
            List<string> tmpListName = new List<string>();
            RSA rsa = new RSA(false);
            for (int i = 0; i < _infos.Namen.Count; i++)
            {
                tmpListName.Add(_infos.Namen[i]);
                _infos.Namen[i] = rsa.Verschlüsseln(_infos.Namen[i], Application.CompanyName);
            }
            XmlSerialisierung<Infos>.Serialisieren(_infos, _pathXML);
            _infos.Namen = tmpListName;
        }
        public void Save(string dir)
        {
            Do.CopyDirectory(Path.GetDirectoryName(Do.PathKey), dir);
        }

        private void ErstelleOrdner()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Do.PathEmails));
            //  DirectoryInfo dd = new DirectoryInfo(Path.GetDirectoryName(Do.PathEmails));
            //  File.SetAttributes(Path.GetDirectoryName(Do.PathEmails), System.IO.FileAttributes.Hidden);
        }

        public void Remove(int index, ListView lw)
        {
            if (index != -1 && (MessageBox.Show("Wollen Sie \"" + _infos.Namen[index] + "\" wirklich entfernen?", "Hinweis", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes))
            {
                _infos.Remove(index);
                LoadNodes(lw);
                Save();
            }
        }
        public void Edit(int index, ListView lw)
        {
            if (index == -1) return;
            int tmpInd = index = (int)lw.SelectedItems[0].Tag;
            Form2 frm2 = new Form2(_infos.Namen[index]);
            if (frm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _infos.Namen[index] = frm2.textBox1.Tex;
                Save();
                LoadNodes(lw);
                if (listviewChange != null) listviewChange(String.Empty);
                lw.FocusedItem = lw.Items[index];
            }
        }
        public string Add(ListView lw, out bool passwort)
        {
            passwort = false;
            string tex = String.Empty;
            Form2 frm2 = new Form2();
            if (frm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _infos.Add(frm2.textBox1.Tex.Trim(), String.Empty);
                lw.Select();
                lw.Focus();
                tex = frm2.textBox1.Tex + " (" + DateTime.Now.ToShortDateString() + ")";
                if (frm2.checkBox1.Checked) tex += "\n---Giveaway---";
                if (frm2.textBoxKommentar.Text.Trim() != String.Empty) tex += "\nKommentar: " + frm2.textBoxKommentar.Text.Trim();
                if (frm2.comboBox1.Text.Contains("@")) tex += "\nE-Mail: " + frm2.comboBox1.Text;
                if (frm2.radioButton2.Checked) tex += "\nSchlüssel: "; else { tex += "\nPasswort: "; passwort = true; }
                _infos.Change(0, tex);
                LoadNodes(lw);
                if (listviewChange != null) listviewChange(String.Empty);
                lw.FocusedItem = lw.Items[0];
                lw.TopItem = lw.Items[0];
                Save();
            }
            return tex;
        }
        public void LoadNodes(ListView lw)
        {
            lw.Items.Clear();
            for (int i = 0; i < _infos.Namen.Count; i++)
            {
                //  ListViewItem l = new ListViewItem((i + 1).ToString() + ". " + _infos.Namen[i]);
                ListViewItem l = new ListViewItem(_infos.Namen[i]);
                l.Tag = i;
                lw.Items.Add(l);
            }
            if (listviewChange != null) listviewChange(String.Empty);
        }
        public void Kopieren()
        {
            string ausg = String.Empty;
            for (int i = 0; i < _infos.Namen.Count; i++)
            {
                string a = _infos.GetKey(i);
                string b = _infos.Namen[i];
                if (!a.Split('\n')[0].Contains(b))
                    ausg += b + ": ";
                ausg += _infos.GetKey(i) + "\n" + new String('_', 80);
            }
            if (ausg != String.Empty) Clipboard.SetText(ausg);
        }
        public void Suchen(string text, Color farbe, bool pwSearchBool, ListView lw)
        {
            if (text != String.Empty && text != "Suchen" && farbe != Color.Gray)
            {
                lw.Items.Clear();
                foreach (var index in _infos.Suche(text, pwSearchBool))
                {
                    lw.Items.Add(_infos.Namen[index]);
                    lw.Items[lw.Items.Count - 1].Tag = index;
                }
            }
            else if (text == String.Empty && farbe == Color.Black)
                LoadNodes(lw);
        }
    }
}
