using PM.Gui.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PM.Gui
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var path = Settings.Default.AbsolutePaths ? Settings.Default.KeyPath : Path.Combine(Application.StartupPath, Settings.Default.KeyPath);

            LoginGui.PasswordInfoLoader.PasswordChanged += PasswordInfoLoader_PasswordChanged;

            var authenticationResult = LoginGui.PasswordInfoLoader.Authenticate(path);

            if (authenticationResult.Success)
            {
                ModelLoader.LoadModel(authenticationResult);

                //////XML Test
                ////Core.XmlSerialization.SerializeModel(ModelLoader.CurrentModel, @"B:\modelXml.xml");
                ////var res = Core.XmlSerialization.DeserializeModel(@"B:\modelXml.xml");

                Application.Run(new MainForm());
            }
        }

        private static void PasswordInfoLoader_PasswordChanged(object sender, LoginGui.PasswordChangedEventArgs e)
        {
            ModelLoader.ChangeAuthData(e.OldAuthData, e.NewAuthData);
        }
    }
}
