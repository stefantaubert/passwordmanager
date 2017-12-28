using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KeySaver_Redisign_1._0
{
    public partial class ComboBoxExtended : UserControl
    {
        int _tmpI = 0;
        public ComboBoxExtended()
        {
            InitializeComponent();
            ComboBoxExtended_SizeChanged(null, null);
        }

        private void ComboBoxExtended_SizeChanged(object sender, EventArgs e)
        {
            Height = comboBox1.Height;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            if ((string)comboBox1.Tag == "edit")
            {
                comboBox1.Items[_tmpI] = comboBox1.Text.Trim();
                comboBox1.SelectedIndex = _tmpI;
            }
            else
            {
                comboBox1.Items.Add(comboBox1.Text.Trim());
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Tag = "";
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox1.Text = "";
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.DropDownStyle == ComboBoxStyle.DropDown)
            {
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.SelectedIndex = _tmpI;
            }
        }

        private void ändernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox1.Tag = "edit";
            SendKeys.Send("{RIGHT}");
            _tmpI = comboBox1.SelectedIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            contextMenuStrip1.Items[1].Visible = contextMenuStrip1.Items[2].Visible = comboBox1.SelectedIndex != -1;
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
        }

    }
}
