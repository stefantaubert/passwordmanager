using PM.Properties;
using KeyManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PM
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

            //IOHandler.LoadModel(Path.Combine(Application.StartupPath, Settings.Default.ModelFileName), "admin");

            //AddItems();
            OpenMainForm();
        }

        private static void OpenMainForm()
        {
            if (File.Exists(GuiIOHandler.ModelPath))
            {
                using (var loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new MainForm());
                    }
                }
            }
            else
            {
                using (var createPasswordForm = new CreatePasswordForm())
                {
                    if (createPasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        OpenMainForm();
                    }
                }
            }

        }

        //private static void AddItems()
        //{
        //    var data = File.ReadAllLines(@"C:\Users\Stefan\Desktop\pass.txt", System.Text.Encoding.Default);

        //    for (int i = 0; i < data.Length - 9; i += 9)
        //    {
        //        var comment = data[i + 1];
        //        var label = comment.Substring(comment.IndexOf("//") + 2).TrimEnd('/');
        //        var name = data[i + 4];
        //        var pass = data[i + 5];

        //        comment = comment.Substring(comment.IndexOf(":") + 2);
        //        name = name.Substring(name.IndexOf(":") + 2);
        //        pass = pass.Substring(pass.IndexOf(":") + 2);
        //        IOHandler.CurrentModel.AddEntry(label, CreateContent(label, name, "", pass, comment), DateTime.Now);
        //    }
        //}

        //private static string CreateContent(string label, string name, string mail, string key, string comment)
        //{
        //    string result = string.Empty;

        //    result += string.Format("{0} ({1})\r\n", label, DateTime.Now.ToShortDateString());
        //    result += GetText("Name", name, true);
        //    result += GetText("Key", key, false);
        //    result += GetText("Comment", comment, true);

        //    return result.TrimEnd('\n');
        //}

        //private static string GetText(string text, string textBox, bool emptyIfEmpty)
        //{
        //    if (emptyIfEmpty && string.IsNullOrWhiteSpace(textBox))
        //    {
        //        return string.Empty;
        //    }
        //    else
        //    {
        //        return string.Format("{0}: {1}\r\n", text, textBox);
        //    }
        //}

    }
}
