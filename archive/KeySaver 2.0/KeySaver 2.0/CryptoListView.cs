using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KeySaver
{
    internal class CryptoListView : ListView
    {
        private MenuItem addItem;
        private MenuItem deleteItem;

        public CryptoListView()
            : base()
        {
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

            Model.ModelLoaded += new EventHandler(Model_ModelLoaded);
        }

        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            this.deleteItem.Visible = this.SelectedItems.Count == 1;
        }

        private void deleteItem_Click(object sender, EventArgs e)
        {
            var item = this.SelectedItems[0] as CryptoListViewItem;

            if (MessageBox.Show(string.Format("Are you sure to delete \"{0}\"?", item.Entry.ToString()), "Sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Items.Remove(item);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.addItem.Click -= new EventHandler(addItem_Click);
                this.deleteItem.Click -= new EventHandler(deleteItem_Click);
                this.ContextMenu.Popup -= new EventHandler(ContextMenu_Popup);
                Model.ModelLoaded -= new EventHandler(Model_ModelLoaded);

                if (Model.CurrentModel != default(Model))
                {
                    Model.CurrentModel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(CurrentModel_PropertyChanged);
                }
            }

            base.Dispose(disposing);
        }

        private void Model_ModelLoaded(object sender, EventArgs e)
        {
            Model.CurrentModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(CurrentModel_PropertyChanged);

            this.RenderFilteredItems();
        }

        private void CurrentModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentFilter")
            {
                this.RenderFilteredItems();
            }
        }

        private void RenderFilteredItems()
        {
            this.SuspendLayout();

            this.Items.Clear();

            var items = Model.CurrentModel.Keys.Where(s => s.Name.ToUpper().Contains(Model.CurrentModel.CurrentFilter)).OrderBy(s => s.Created).Reverse();

            foreach (var item in items)
            {
                this.Items.Add(new CryptoListViewItem(item));
            }

            Model.CurrentModel.RaiseSelectionChangedEvent();

            this.ResumeLayout();
        }

        private void addItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new ItemEditDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var item = Model.CurrentModel.Add(dlg.Label, dlg.KeyValue);

                    this.Items.Insert(0, new CryptoListViewItem(item));
                    this.Items[0].Selected = true;
                }
            }
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
                Model.CurrentModel.RaiseSelectionChangedEvent(item.Entry);
            }
            else
            {
                Model.CurrentModel.RaiseSelectionChangedEvent();
            }

            this.ResumeLayout();
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

        protected override void OnAfterLabelEdit(LabelEditEventArgs e)
        {
            base.OnAfterLabelEdit(e);

            var item = this.SelectedItems[0] as CryptoListViewItem;

            item.Entry.Name = e.Label;
        }
    }
}