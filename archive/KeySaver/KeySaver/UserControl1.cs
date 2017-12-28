using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class UserControl1 : UserControl
    {
        string _a = "abc";
        public string a
        {
            get { return _a; }
            set
            {
                _a = value.Trim();
                //textBox1.Text = _a;
                //textBox1_Leave(textBox1, null);
            }
        }
        public string Tex { get { return textBox1.Text.Trim() == a ? "" : textBox1.Text.Trim(); } set { if (value.Trim() != a) { SetNormal(); textBox1.Text = value.Trim(); } } }
        public event Action<string> TexChanged;

        public UserControl1()
        {
            InitializeComponent();
        }

        #region Suchen
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty && !textBox1.Focused) SetKursiv();
            else SetNormal();
        }
        void SetKursiv()
        {
            textBox1.Font = new System.Drawing.Font(textBox1.Font, System.Drawing.FontStyle.Italic);
            //textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = a;
        }
        void SetNormal()
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Font = new System.Drawing.Font(textBox1.Font, System.Drawing.FontStyle.Regular);
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == a) textBox1.Text = String.Empty;
            SetNormal();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (TexChanged != null) TexChanged(Tex);
        }
        #endregion

        private void UserControl1_SizeChanged(object sender, EventArgs e)
        {
            //   Size = new System.Drawing.Size(Size.Width, textBox1.Height);
            Height = textBox1.Height;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            textBox1_Leave(null, null);
        }
    }
}
