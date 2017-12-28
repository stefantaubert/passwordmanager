using KeyManager;
using System;

namespace PM
{
    internal sealed class SelectionChangedEventArgs : EventArgs
    {
        public SelectionChangedEventArgs(IEntry entry)
            : base()
        {
            this.Entry = entry;
        }

        public SelectionChangedEventArgs()
           : base()
        {
        }

        public bool HasEntry
        {
            get
            {
                return this.Entry != default(IEntry);
            }
        }

        public IEntry Entry
        {
            get;
            private set;
        }
    }
}
