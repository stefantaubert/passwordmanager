using System.Windows.Forms;

namespace KeySaver
{
    internal sealed class CryptoListViewItem : ListViewItem
    {
        public CryptoListViewItem(Entry item)
        {
            this.Entry = item;
            this.Text = item.ToString();
        }

        public Entry Entry
        {
            get;
            private set;
        }

        public override void Remove()
        {
            base.Remove();

            Model.CurrentModel.Remove(this.Entry);
        }
    }
}