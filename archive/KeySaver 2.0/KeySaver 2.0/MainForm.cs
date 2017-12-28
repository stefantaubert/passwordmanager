using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeySaver
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Model.Load();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Model.Save();
        }
    }
}
