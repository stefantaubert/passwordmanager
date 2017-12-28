namespace KeySaver
{
    using System;
    using System.Windows.Forms;

    internal class CryptoTextBox : RichTextBox
    {
        private Entry currentEntry;

        private MenuItem saveMenuItem;
        private MenuItem keyGenMenuItem;
        private MenuItem Separator1;
        private MenuItem Separator2;
        private MenuItem copyMenuItem;
        private MenuItem pasteMenuItem;

        public CryptoTextBox()
            : base()
        {
            this.Multiline = true;
            this.currentEntry = default(Entry);

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

            Model.ModelLoaded += new EventHandler(Model_ModelLoaded);

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
                Model.Save();
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
            Model.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Model.ModelLoaded -= new EventHandler(Model_ModelLoaded);
                this.saveMenuItem.Click -= new EventHandler(saveMenuItem_Click);
                this.keyGenMenuItem.Click -= new EventHandler(keyGenMenuItem_Click);
                this.pasteMenuItem.Click -= new EventHandler(pasteMenuItem_Click);
                this.copyMenuItem.Click -= new EventHandler(copyMenuItem_Click);

                if (Model.CurrentModel != default(Model))
                {
                    Model.CurrentModel.SelectionChanged -= new EventHandler<SelectionChangedEventArgs>(CurrentModel_SelectionChanged);
                }
            }

            base.Dispose(disposing);
        }

        private void Model_ModelLoaded(object sender, EventArgs e)
        {
            Model.CurrentModel.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(CurrentModel_SelectionChanged);
        }

        private void Render()
        {
            this.SuspendLayout();
            var hasEntry = this.currentEntry != default(Entry);

            this.ReadOnly = !hasEntry;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.saveMenuItem.Visible = hasEntry;
            this.Separator1.Visible = hasEntry;
            this.Separator2.Visible = hasEntry;
            this.copyMenuItem.Visible = hasEntry;
            this.pasteMenuItem.Visible = hasEntry;
            this.Text = hasEntry ? currentEntry.Content : "Please choose an item!";

            this.ResumeLayout();
        }

        private void CurrentModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.currentEntry = e.Entry;
            this.Render();
        }
    }
}