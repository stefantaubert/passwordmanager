using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Gui
{
    internal sealed class SelectedValueChangedEventArgs : EventArgs
    {
        public SelectedValueChangedEventArgs()
            : base()
        {
        }

        public bool Cancel
        {
            get;
            set;
        }
    }
}
