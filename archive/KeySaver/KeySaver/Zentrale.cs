using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Zentrale
    {
        Infos _infos;
        public Infos Info { get { return _infos; } set { _infos = value; } }
        string _path = Do.;
        public event Action<String> listviewChange;

        private List<string> _groups = new List<string>(); //{ "stefco94@gmx.de", "virus.spam1234@web.de", "steff@128bit.hu", "Sonstiges", "Giveaway" };
        public List<string> Gruppen { get { return _groups; } set { _groups = value; } }

        public Zentrale(string pfad)
        {
            _path = pfad;
        }
        public bool InitializeComponent(ListView list)
        {
            if (File.Exists(_path))
            {
                _infos = XmlSerialisierung<Infos>.Deserialisieren(_path);

                for (int i = 0; i < _infos.Namen.Count; i++)
                {
                    try
                    {
                        _infos.Namen[i] = new RSA(false).Entschlüsseln(_infos.Namen[i], Application.CompanyName);
                        //_infos.Namen[i] = new RSA(false).Entschlüsseln(_infos.Namen[i], Application.CompanyName);
                        //    if (_infos.Gruppe.Count > i)
                        _infos.Gruppe[i] = new RSA(false).Entschlüsseln(_infos.Gruppe[i], Application.CompanyName);
                        //   _infos.Gruppe[i] = new RSA(false).Entschlüsseln(_infos.Gruppe[i], Application.CompanyName);
                    }
                    catch { }
                }
            }
            else
            {
                _infos = new Infos();
            }
            bool d = _infos.Ini(Do.);
            if (!d)
            {
                MessageBox.Show("Bitte führen Sie erst den KeyGen.exe aus!");
                return false;
            }

            DialogResult fd = new Login(_infos).ShowDialog();
            if (LoadGroups() && (fd == System.Windows.Forms.DialogResult.OK)) return true;
            else if (fd == DialogResult.Cancel) return false;
            else
            {
                MessageBox.Show("Bitte geben Sie Gruppen an! (gruppen.txt)");
                return false;
            }
        }
        public void Save()
        {
            List<string> tmpListName = new List<string>();
            List<string> tmpListGruppe = new List<string>();
            RSA rsa = new RSA(false);
            for (int i = 0; i < _infos.Namen.Count; i++)
            {
                tmpListName.Add(_infos.Namen[i]);
                _infos.Namen[i] = rsa.Verschlüsseln(_infos.Namen[i], Application.CompanyName);
                if (_infos.Gruppe.Count > i)
                {
                    tmpListGruppe.Add(_infos.Gruppe[i]);
                    _infos.Gruppe[i] = rsa.Verschlüsseln(_infos.Gruppe[i], Application.CompanyName);
                }
            }
            XmlSerialisierung<Infos>.Serialisieren(_infos, _path);
            _infos.Namen = tmpListName;
            _infos.Gruppe = tmpListGruppe;
        }
        public void Suchen(string text, Color farbe, ListView lw)
        {
            if (text != "" && text != "Suchen" && farbe != Color.Gray)
            {
                lw.Nodes.Clear();
                foreach (var item in _infos.Suche(text))
                {
                    lw.Nodes.Add(_infos.Namen[item]);
                    lw.Nodes[lw.Nodes.Count - 1].Tag = item;
                }
            }
            else if (text == "" && farbe == Color.Black)
                LoadNodes(lw);
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
            int tmpInd = index = (int)lw.SelectedNode.Tag;
            Form2 frm2 = new Form2(_infos.Namen[index], _groups, _infos.Gruppe[index]);
            if (frm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _infos.Namen[index] = frm2.textBox1.Text;
                _infos.Gruppe[index] = frm2.comboBox1.Text;
                Save();
                LoadNodes(lw);
                SelectNode(frm2.comboBox1.Text, frm2.textBox1.Text, lw);
                //lw.SelectedNode = lw.Nodes[tmpInd];
            }
        }
        private void SelectNode(string textparent, string textchild, ListView _list)
        {
            TreeNode t = new TreeNode();
            TreeNode child = new TreeNode();
            for (int i = 0; i < _list.Nodes.Count; i++)
                if (_list.Nodes[i].Text == textparent)
                { t = _list.Nodes[i]; }
            for (int i = 0; i < t.Nodes.Count; i++)
                if (t.Nodes[i].Text == textchild)
                { child = t.Nodes[i]; }
            _list.SelectedNode = child;
        }
        public string Add(ListView _list)
        {
            string tex = String.Empty;
            Form2 frm2 = new Form2(_groups, "none");
            if (frm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _infos.Add(frm2.textBox1.Text.Trim(), String.Empty, frm2.comboBox1.Text);
                _list.Select();
                _list.Focus();
                tex = frm2.textBox1.Text + " (" + DateTime.Now.ToShortDateString() + ")";
                if (frm2.comboBox1.Text.Contains("@"))
                    tex += "\nEmail: " + frm2.comboBox1.Text;
                tex += "\nPasswort/Key: ";

                _infos.Change(_infos.Keys.Count - 1, tex);
                LoadNodes(_list);
                SelectNode(frm2.comboBox1.Text, frm2.textBox1.Text, _list);
                //TreeNode t = new TreeNode();
                //for (int i = 0; i < _list.Nodes.Count; i++)
                //    if (_list.Nodes[i].Text == frm2.comboBox1.Text)
                //    { t = _list.Nodes[i]; break; }
                //_list.SelectedNode = t.Nodes[t.Nodes.Count - 1];
                Save();
            }
            return tex;
        }
        public static string[] ReadLines(string pfad)
        {
            StreamReader sR = new StreamReader(pfad);
            string text = sR.ReadToEnd();
            sR.Dispose();
            sR.Close();
            string[] lines = text.Split('\n');
            return lines;
        }
        public bool LoadGroups()
        {
            _groups.Clear();
            if (!File.Exists(Application.StartupPath + "\\gruppen.txt")) return false;
            string[] lines = ReadLines(Application.StartupPath + "\\gruppen.txt");
            foreach (var item in lines)
                if (item.Trim() != String.Empty)
                {
                    _groups.Add(item);
                }
            return _groups.Count > 0;
        }
        public void LoadNodes(ListView tw)
        {
            tw.Nodes.Clear();
            for (int i = 0; i < _infos.Namen.Count; i++)
            {
                TreeNode tn;
                int index = -1;
                if (_infos.Gruppe.Count > i)
                {
                    foreach (TreeNode item in tw.Nodes)
                        if (item.Text == _infos.Gruppe[i])
                        { index = item.Index; break; }
                    if (index != -1) tn = tw.Nodes[index];
                    else tn = new TreeNode(_infos.Gruppe[i]);
                }
                else
                {
                    Form2 frm2 = new Form2(_infos.Namen[i], _infos.GetKey(i), _groups, "none");
                    frm2.ShowDialog();
                    tn = new TreeNode(frm2.comboBox1.Text);
                    if (_infos.Gruppe.Count >= i)
                        _infos.Gruppe.Add(frm2.comboBox1.Text);
                    else _infos.Gruppe[i] = frm2.comboBox1.Text;
                    //  Save();
                }
                TreeNode rubrik = new TreeNode(_infos.Namen[i]);
                rubrik.Tag = i;
                tn.Nodes.Add(rubrik);
                if (index == -1) tw.Nodes.Add(tn);
            }
            if (listviewChange != null) listviewChange("");
        }
        public void Kopieren()
        {
            string ausg = "";
            for (int i = 0; i < _infos.Namen.Count; i++)
            {
                string a = _infos.GetKey(i);
                string b = _infos.Namen[i];
                if (!a.Split('\n')[0].Contains(b))
                    ausg += b + ": ";
                ausg += "[" + _infos.Gruppe[i] + "]\n";
                ausg += _infos.GetKey(i) + "\n" + new String('_', 80);
            }
            Clipboard.SetText(ausg);
        }
    }
}