using System;

namespace KeySaver
{
    internal sealed class SelectionChangedEventArgs : EventArgs
    {
        public SelectionChangedEventArgs(Entry entry)
            : base()
        {
            this.Entry = entry;
        }

        public Entry Entry
        {
            get;
            private set;
        }

        public bool HasEntry
        {
            get
            {
                return this.Entry != default(Entry);
            }
        }
    }
}