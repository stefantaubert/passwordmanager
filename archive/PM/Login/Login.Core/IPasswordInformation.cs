using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public interface IPasswordInformation
    {
        void Save(string path);

        void RenewHash(string password, uint hashIterations);

        void ChangePassword(string oldPassword, string newPassword, uint hashIterations);

        bool PasswordIsCorrect(string password, uint hashIterations);

        byte[] PasswordSalt
        {
            get;
        }
    }
}