using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public sealed class PasswordChangedEventArgs : EventArgs
    {
        public PasswordChangedEventArgs(string oldPassword, byte[] oldPasswortSalt, string newPassword, byte[] newPasswordSalt)
        {
            this.OldPassword = oldPassword;
            this.OldPasswortSalt = oldPasswortSalt;
            this.NewPassword = newPassword;
            this.NewPasswordSalt = newPasswordSalt;
        }

        public string NewPassword
        {
            get;
            private set;
        }

        public byte[] NewPasswordSalt
        {
            get;
            private set;
        }

        public string OldPassword
        {
            get;
            private set;
        }

        public byte[] OldPasswortSalt
        {
            get;
            private set;
        }
    }
}
