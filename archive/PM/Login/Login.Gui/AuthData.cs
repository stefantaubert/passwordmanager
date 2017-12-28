using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginGui
{
    public struct AuthData
    {
        public string EnteredPasswordAsString
        {
            get;
            internal set;
        }

        public byte[] PasswordSalt
        {
            get;
            internal set;
        }

        public bool Success
        {
            get;
            internal set;
        }

        public byte[] EnteredPasswordAsBytes
        {
            get
            {
                return Encoding.UTF8.GetBytes(EnteredPasswordAsString);
            }
        }
    }
}
