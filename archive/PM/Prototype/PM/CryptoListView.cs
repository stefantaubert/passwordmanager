using KeyManager;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PM
{
    internal class CryptoListView : ListView
    {
        private MenuItem addItem;

        private MenuItem deleteItem;

        public CryptoListView()
            : base()
        {
            this.Columns.Add("");
            this.Columns[0].Width = -2;
            this.HeaderStyle = ColumnHeaderStyle.None;

            this.View = System.Windows.Forms.View.Details;
            this.FullRowSelect = true;
            this.MultiSelect = false;
            this.LabelEdit = true;
            this.LabelWrap = false;

            this.ContextMenu = new ContextMenu();
            this.ContextMenu.Popup += new EventHandler(ContextMenu_Popup);

            this.addItem = new MenuItem("&Add");
            this.addItem.Click += new EventHandler(addItem_Click);
            this.ContextMenu.MenuItems.Add(this.addItem);

            this.deleteItem = new MenuItem("&Delete");
            this.deleteItem.Click += new EventHandler(deleteItem_Click);
            this.ContextMenu.MenuItems.Add(this.deleteItem);
        }

        public event EventHandler<SelectionChangedEventArgs> SelectedEntryChanged;

        public void RenderFilteredItems(string filter)
        {
            this.SuspendLayout();

            this.Items.Clear();

            var items = GuiIOHandler.CurrentModel.Entries.Where(s => s.Name.ToUpper().Contains(filter.ToUpper())).OrderBy(s => s.Created).Reverse();

            foreach (var item in items)
            {
                this.Items.Add(new CryptoListViewItem(item));
            }

            this.ResumeLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.addItem.Click -= new EventHandler(addItem_Click);
                this.deleteItem.Click -= new EventHandler(deleteItem_Click);
                this.ContextMenu.Popup -= new EventHandler(ContextMenu_Popup);
            }

            base.Dispose(disposing);
        }

        protected override void OnAfterLabelEdit(LabelEditEventArgs e)
        {
            base.OnAfterLabelEdit(e);

            var item = this.Items[e.Item] as CryptoListViewItem;

            item.Entry.Name = e.Label;

            GuiIOHandler.SaveCurrentModel();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            this.SuspendLayout();
            this.ResetColor();

            if (this.SelectedItems.Count == 1)
            {
                var item = this.SelectedItems[0] as CryptoListViewItem;

                this.SelectedItems[0].ForeColor = Color.Red;
                this.RaiseSelectionChangedEvent(new SelectionChangedEventArgs(item.Entry));
            }
            else
            {
                this.RaiseSelectionChangedEvent(new SelectionChangedEventArgs());
            }

            this.ResumeLayout();
        }

        private void addItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new CreateEntryForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var item = GuiIOHandler.CurrentModel.AddEntry(dlg.Label, dlg.KeyValue, DateTime.Now);

                    GuiIOHandler.SaveCurrentModel();

                    this.Items.Insert(0, new CryptoListViewItem(item));
                    this.Items[0].Selected = true;
                }
            }
        }

        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            this.deleteItem.Visible = this.SelectedItems.Count == 1;
        }

        private void deleteItem_Click(object sender, EventArgs e)
        {
            var item = this.SelectedItems[0] as CryptoListViewItem;

            if (MessageBox.Show(string.Format("Are you sure to delete \"{0}\"?", item.Entry.Name), "Sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                item.Remove();

            }
        }

        private void RaiseSelectionChangedEvent(SelectionChangedEventArgs eventArgs)
        {
            var handler = this.SelectedEntryChanged;

            if (handler != default(EventHandler<SelectionChangedEventArgs>))
            {
                handler(this, eventArgs);
            }
        }

        private void ResetColor()
        {
            var defaultColor = Color.Black;

            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].ForeColor != defaultColor)
                {
                    this.Items[i].ForeColor = defaultColor;
                    break;
                }
            }
        }

        private sealed class CryptoListViewItem : ListViewItem
        {
            public CryptoListViewItem(IEntry item)
            {
                this.Entry = item;

                this.Text = item.Name;

            }

            public IEntry Entry
            {
                get;
                private set;
            }

            public override void Remove()
            {
                base.Remove();

                GuiIOHandler.CurrentModel.RemoveEntry(this.Entry);

                GuiIOHandler.SaveCurrentModel();
            }
        }
    }
}