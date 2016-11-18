using Login;
using LoginGui.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginGui
{
    public static class PasswordInfoLoader
    {
        public static event EventHandler<PasswordChangedEventArgs> PasswordChanged;

        public static void Rehash(string passwordFilePath, string password)
        {
            Assert.IsTrue(File.Exists(passwordFilePath));
            Assert.IsTrue(password.Length > 0);

            var pwInfo = PasswordInformation.Load(passwordFilePath);

            pwInfo.RenewHash(password);
            pwInfo.Save(passwordFilePath);
        }

        internal static void RaisePasswordChangedEvent(AuthData oldData, AuthData newData)
        {
            var handler = PasswordChanged;

            if (handler != default(EventHandler<PasswordChangedEventArgs>))
            {
                handler(null, new PasswordChangedEventArgs(oldData, newData));
            }
        }

        public static AuthData Authenticate(string passwordFilePath, string password)
        {
            var pwInfo = PasswordInformation.Load(passwordFilePath);
            var result = new AuthData();

            result.Success = false;
            result.PasswordSalt = pwInfo.PasswordSalt;

            if (pwInfo.PasswordIsCorrect(password))
            {
                result.EnteredPasswordAsString = password;
                result.PasswordSalt = pwInfo.PasswordSalt;
                result.Success = true;
            }

            return result;
        }

        public static AuthData Authenticate(string passwordFilePath)
        {
            if (File.Exists(passwordFilePath))
            {
                return ShowLoginForm(passwordFilePath);
            }
            else
            {
                return ShowCreatePasswordForm(passwordFilePath);
            }
        }

        private static AuthData ShowCreatePasswordForm(string passwordFilePath)
        {
            using (var createPassForm = new CreatePasswordForm(passwordFilePath))
            {
                createPassForm.ShowDialog();

                return createPassForm.AuthenticationResult;
            }
        }

        private static AuthData ShowLoginForm(string passwordFilePath)
        {
            using (var login = new LoginForm(passwordFilePath))
            {
                login.ShowDialog();

                return login.AuthenticationResult;
            }
        }
    }
}