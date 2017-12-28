namespace PM
{
    using KeyManager;
    using System;
    using System.Windows.Forms;

    internal class CryptoTextBox : RichTextBox
    {
        private readonly string emptyText;
        private IEntry currentEntry;

        private MenuItem saveMenuItem;
        private MenuItem keyGenMenuItem;
        private MenuItem Separator1;
        private MenuItem Separator2;
        private MenuItem copyMenuItem;
        private MenuItem pasteMenuItem;

        public CryptoTextBox()
            : base()
        {
            emptyText = "Please select an item!";

            this.Multiline = true;
            this.currentEntry = default(IEntry);

            this.ContextMenu = new ContextMenu();
            this.ShortcutsEnabled = true;
            this.saveMenuItem = new MenuItem();
            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.Click += new EventHandler(saveMenuItem_Click);
            this.saveMenuItem.Visible = false;
            this.ContextMenu.MenuItems.Add(this.saveMenuItem);

            this.Separator1 = new MenuItem("-");
            this.Separator1.Visible = false;
            this.ContextMenu.MenuItems.Add(this.Separator1);

            this.keyGenMenuItem = new MenuItem();
            this.keyGenMenuItem.Text = "&Generate key";
            this.keyGenMenuItem.Click += new EventHandler(keyGenMenuItem_Click);
            this.ContextMenu.MenuItems.Add(this.keyGenMenuItem);

            this.Separator2 = new MenuItem("-");
            this.Separator2.Visible = false;
            this.ContextMenu.MenuItems.Add(this.Separator2);

            this.copyMenuItem = new MenuItem();
            this.copyMenuItem.Text = "&Copy";
            this.copyMenuItem.Click += new EventHandler(copyMenuItem_Click);
            this.copyMenuItem.Visible = false;
            this.ContextMenu.MenuItems.Add(this.copyMenuItem);

            this.pasteMenuItem = new MenuItem();
            this.pasteMenuItem.Text = "&Paste";
            this.pasteMenuItem.Click += new EventHandler(pasteMenuItem_Click);
            this.pasteMenuItem.Visible = false;
            this.ContextMenu.MenuItems.Add(this.pasteMenuItem);

            //this.Render();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            var text = Clipboard.GetText();

            if (!string.IsNullOrWhiteSpace(text))
            {
                var selStart = this.SelectionStart;
                this.Text = this.Text.Insert(this.SelectionStart, text);
                this.SelectionStart = selStart + text.Length;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.S && e.Control)
            {
                this.currentEntry.Content = this.Text.Trim();

                GuiIOHandler.SaveCurrentModel();
            }
        }
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.SelectedText))
            {
                Clipboard.SetText(this.SelectedText);
            }
        }

        private void keyGenMenuItem_Click(object sender, EventArgs e)
        {
            KeyGenForm.ShowKeyGen();
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            this.currentEntry.Content = this.Text.Trim();
            GuiIOHandler.SaveCurrentModel();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                this.saveMenuItem.Click -= new EventHandler(saveMenuItem_Click);
                this.keyGenMenuItem.Click -= new EventHandler(keyGenMenuItem_Click);
                this.pasteMenuItem.Click -= new EventHandler(pasteMenuItem_Click);
                this.copyMenuItem.Click -= new EventHandler(copyMenuItem_Click);
            }
        }

        public void RenderEmpty()
        {
            this.Render(default(IEntry));
        }

        public void Render(IEntry currentEntry)
        {
            this.SuspendLayout();

            this.currentEntry = currentEntry;

            var hasEntry = this.currentEntry != default(IEntry);

            this.ReadOnly = !hasEntry;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.saveMenuItem.Visible = hasEntry;
            this.Separator1.Visible = hasEntry;
            this.Separator2.Visible = hasEntry;
            this.copyMenuItem.Visible = hasEntry;
            this.pasteMenuItem.Visible = hasEntry;
            this.Text = hasEntry ? currentEntry.Content : this.emptyText;

            this.ResumeLayout();
        }

    }
}