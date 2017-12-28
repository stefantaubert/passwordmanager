using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginGui
{
    public sealed class PasswordChangedEventArgs : EventArgs
    {
        public PasswordChangedEventArgs(AuthData oldData, AuthData newData)
        {
            this.OldAuthData = oldData;
            this.NewAuthData = newData;
        }

        public AuthData OldAuthData
        {
            get;
            private set;
        }

        public AuthData NewAuthData
        {
            get;
            private set;
        }
    }
}
