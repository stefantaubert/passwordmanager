using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misc;

namespace LoginForm
{
    public struct LoginToken
    {
        public string Password { get; set; }
        public string Key { get; set; }
        public bool Success { get; set; }
        public string FileName { get; set; }

        public string Encrypt(string str)
        {
            if (this.Success)
            {
                return str.Encrypt(this.Key);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
