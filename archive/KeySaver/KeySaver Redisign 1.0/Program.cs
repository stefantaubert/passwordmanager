using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KeySaver_Redisign_1._0
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
            Application.Run(new Form1());
            //try { Application.Run(new Form1()); }
            //catch (ObjectDisposedException) { }
            //catch
            //{
            //}
        }
    }
}
