using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PM.Gui
{
    internal class CancelableListBox : ListBox
    {
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            var newEvent = new SelectedValueChangedEventArgs();
            base.OnSelectedIndexChanged(newEvent);

            if (newEvent.Cancel)
            {

            }
        }
    }
}
