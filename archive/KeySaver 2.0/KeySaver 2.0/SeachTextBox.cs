using System;
using System.Windows.Forms;

namespace KeySaver
{
    internal sealed class SeachTextBox : TextBox
    {
        public SeachTextBox()
            : base()
        {
            this.SetCueBanner("Search", false);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Model.CurrentModel != default(Model))
            {
                Model.CurrentModel.CurrentFilter = this.Text.Trim();
            }
        }
    }
}