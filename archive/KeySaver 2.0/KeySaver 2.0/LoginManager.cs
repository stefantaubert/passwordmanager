using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KeySaver2k15.Login;
using System.IO;
using LoginForm.Key;

namespace LoginForm
{
    public static class LoginManager
    {
        public static LoginToken Login(string fileName)
        {
            var token = new LoginToken();
            token.FileName = fileName;
            token.Success = false;
            var result = false;
            var mgr = default(Key.KeyManager);

            if (File.Exists(fileName))
            {
                try
                {
                    mgr = new KeyManager(fileName);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }

            if (result)
            {
                token = ShowLogin(token, mgr);
            }
            else
            {
                result = ShowPasswordCreator(fileName);

                if (result)
                {
                    return Login(fileName);
                }

                token.Success = result;
            }

            return token;
        }

        private static LoginToken ShowLogin(LoginToken token, KeyManager mgr)
        {
            using (var loginForm = new LoginForm(mgr))
            {
                var result = loginForm.ShowDialog();

                // Deleted
                if (result == System.Windows.Forms.DialogResult.Ignore)
                {
                    token = Login(token.FileName);
                }
                else if (result == System.Windows.Forms.DialogResult.OK)
                {
                    token.Password = loginForm.EnteredPassword;
                    token.Success = true;
                    token.Key = mgr.GetKey(token.Password);
                }
                else
                {
                    token.Success = false;
                }
            }

            return token;
        }

        private static bool ShowPasswordCreator(string fileName)
        {
            using (var pwdForm = new SetPasswordForm())
            {
                if (pwdForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    new KeyManager(fileName, pwdForm.Password);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
