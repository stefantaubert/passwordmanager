using KeyManager;
using System;
using Common;
using System.Drawing;
using System.Windows.Forms;

namespace PM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.textBox1.SetCueBanner("Search...", false);

            this.cryptoListView2.RenderFilteredItems(textBox1.Text);

            this.cryptoTextBox1.RenderEmpty();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.cryptoListView2.RenderFilteredItems(textBox1.Text);

            this.cryptoTextBox1.RenderEmpty();
        }

        private void cryptoListView1_SelectedEntryChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cryptoTextBox1.Render(e.Entry);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

    }
}