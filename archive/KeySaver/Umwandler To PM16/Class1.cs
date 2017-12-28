using KeyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umwandler_To_PM16
{
    public static class Class1
    {
        public static void Start()
        {
            Schlüsselverwaltung.Infos.Login();
            Schlüsselverwaltung.Infos.Laden();

            IOHandler.Load(@"C: \Users\Stefan\Desktop\data.xml", "admin");

            for (int i = 0; i < Schlüsselverwaltung.Infos.Keys.Count; i++)
            {
                var item = Schlüsselverwaltung.Infos.Keys[Schlüsselverwaltung.Infos.Keys.Count - 1 - i];
                item.Entschlüsseln();
                var datum = item.Key.Split('\n').First().Trim();
                var date = datum.Substring(datum.LastIndexOf('(') + 1, 10);
                Console.WriteLine(date);

                IOHandler.CurrentModel.AddEntry(item.Name, item.Key.Replace("\n", "\r\n"), Convert.ToDateTime(date));

                //m.Add(item.Name, item.Key.Replace("\n", "\r\n"), Convert.ToDateTime(date));
            }

            for (int i = 0; i < Schlüsselverwaltung.Infos.Adressen.Count; i++)
            {
                IOHandler.CurrentModel.AddMail(Schlüsselverwaltung.Infos.Adressen[i]);
                //m.AddMail(Schlüsselverwaltung.Infos.Adressen[i]);
            }

            IOHandler.SaveCurrentModel();
        }
    }
}
