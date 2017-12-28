using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Schlüsselverwaltung;

namespace KeySaver_Redisign_1._0
{
    public partial class Form1 : Form
    {
        // private string _tmpAktText;
        public Form1()
        {
            if (!Infos.Login())
            {
                Close();
                return;
            }
            InitializeComponent();
            userControl11.TexChanged += new Action<string>(userControl11_TexChanged);
            Infos.Laden();
            LadeListe();

            contextMenuStrip1.Items[1].Visible = treeView1.SelectedNode != null;
        }

        void userControl11_TexChanged(string obj)
        {
            //  if (obj.Trim() == "") return;
            Infos.Filter = obj;
            Infos.EinträgeEinschließen = einträgeEinschließenToolStripMenuItem.Checked;
            LadeListe();
            ZeigeEintrag(-1);
        }

        void LadeListe()
        {
            List<Eintrag> einträgeTMP = new List<Eintrag>(Infos.GefilterteEinträge);
            Text = "Schlüsselverwaltung (" + einträgeTMP.Count + (einträgeTMP.Count == 1 ? " Eintrag)" : " Einträge)");
            treeView1.Nodes.Clear();
            for (int i = 0; i < einträgeTMP.Count; i++)
            {
                Infos.AktInd = einträgeTMP[i].TmpInd;
                treeView1.Nodes.Add(new TreeNode(Infos.AktName) { Tag = Infos.AktInd });
            }
        }
        void ZeigeEintrag(int index)
        {
            foreach (TreeNode item in treeView1.Nodes)
                item.ForeColor = Color.Black;

            if (contextMenuStrip1.Items[1].Visible =
                contextMenuStrip2.Items[1].Visible =
                contextMenuStrip2.Items[2].Visible =
                (index >= 0 && index < Infos.Keys.Count))
            {
                treeView1.SelectedNode = treeView1.Nodes[index];
                Infos.AktInd = (int)treeView1.Nodes[index].Tag;
                richTextBox1.ReadOnly = false;
                treeView1.Nodes[index].ForeColor = Color.Red;
                richTextBox1.Text = Infos.AktKey;
            }
            else
            {
                richTextBox1.Text = "Bitte Auswahl treffen!";
                richTextBox1.ReadOnly = true;
            }
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            if (f2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Infos.Neu(f2.Bezeichnung, f2.Ausg);
                Infos.Filter = "";
                LadeListe();
                ZeigeEintrag(0);
            }
        }

        private void keygenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new KeyGenForm().ShowDialog();
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if ((int)treeView1.SelectedNode.Tag != (int)e.Node.Tag) return; //label editiert ohne es auszuwählen
            if (e.Label == null) return; //wenn man umbennen will aber nichts ändert und einen anderen Node auswählt
            Infos.AktInd = (int)e.Node.Tag;
            Infos.Ändern(e.Label, true);
            Infos.Speichern(false);
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Infos.AktInd = (int)treeView1.SelectedNode.Tag;
            Infos.Löschen();
            LadeListe();
            ZeigeEintrag(-1);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ZeigeEintrag(e.Node.Index);
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Infos.Ändern(richTextBox1.Text.Trim(), false);
            Infos.Speichern(false);
        }

        private void registryeintragHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (registryeintragHinzufügenToolStripMenuItem.Text.Contains("hinzufügen"))
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Infos.RegistryEintragHinzufügen(openFileDialog1.FileName);
                    ZeigeEintrag(treeView1.SelectedNode.Index);
                    speichernToolStripMenuItem_Click(null, null);
                }
            }
            else Infos.RegistryEintragAktivieren();
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (Infos.AktInd != -1) contextMenuStrip2.Items[1].Text = Infos.AktKeyContainsReg ? "Registryeintrag ausführen" : "Registryeintrag hinzufügen";
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys)131155) // Strg + S
                speichernToolStripMenuItem_Click(null, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            contextMenuStrip3.Show(Cursor.Position);
        }

        private void einträgeEinschließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userControl11_TexChanged(userControl11.Tex);
        }
    }
}