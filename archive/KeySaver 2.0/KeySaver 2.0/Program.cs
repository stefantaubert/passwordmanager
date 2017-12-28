using System;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace KeySaver
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ////var stream = typeof(KeyInfo).Assembly.GetManifestResourceStream("KeySaver16.key");

            ////BinaryFormatter b = new BinaryFormatter();
            //////var obj = b.Deserialize(stream);

            ////var key = new KeyInfo("tret", "fffd");
            ////b.Serialize(stream, key);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var restart = false;

            do
            {
                restart = false;
                var login = new LoginForm(Application.StartupPath + "\\key");
                if (login.LoadKey())
                {
                    var result = login.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Application.Run(new MainForm());
                    }
                    else if (result == DialogResult.Ignore)
                    {
                        restart = true;
                    }
                }
            }
            while (restart);
        }
    }
}