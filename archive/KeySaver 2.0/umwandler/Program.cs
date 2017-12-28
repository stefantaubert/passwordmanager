using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1;
using KeySaver;
using System.Windows.Forms;

namespace umwandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new Model();
            Schlüsselverwaltung.Infos.Login();
            Schlüsselverwaltung.Infos.Laden();

            var login = new LoginForm(Application.StartupPath + "\\key");
            if (login.LoadKey())
            {
                var result = login.ShowDialog();
                if (result == DialogResult.OK)
                {
                    for (int i = 0; i < Schlüsselverwaltung.Infos.Keys.Count; i++)
                    {
                        var item = Schlüsselverwaltung.Infos.Keys[Schlüsselverwaltung.Infos.Keys.Count - 1 - i];
                        item.Entschlüsseln();
                        var datum = item.Key.Split('\n').First().Trim();
                        var date = datum.Substring(datum.LastIndexOf('(') + 1, 10);
                        Console.WriteLine(date);
                        m.Add(item.Name, item.Key.Replace("\n", "\r\n"), Convert.ToDateTime(date));
                    }

                    for (int i = 0; i < Schlüsselverwaltung.Infos.Adressen.Count; i++)
                    {
                        m.AddMail(Schlüsselverwaltung.Infos.Adressen[i]);
                    }

                    m.SerializeStream(Application.StartupPath + "\\data");
                }
            }

            return;
            Centrale _zenti = new WindowsFormsApplication1.Centrale(WindowsFormsApplication1.Do.PathXml);

            if (_zenti.Load(WindowsFormsApplication1.Do.PathKey))
            {
                Schlüsselverwaltung.Infos.Login();
                Schlüsselverwaltung.Infos.Laden();
                Schlüsselverwaltung.Infos.Keys.Clear();
                Schlüsselverwaltung.Infos.Adressen.Clear();
                for (int i = 0; i < _zenti.Info.Keys.Count; i++)
                {
                    Schlüsselverwaltung.Infos.Neu(_zenti.Info.Namen[_zenti.Info.Keys.Count - 1 - i], _zenti.Info.GetKey(_zenti.Info.Keys.Count - 1 - i));
                }
                RSA rsa = new RSA(false);
                foreach (var item in WindowsFormsApplication1.Do.ReadLines(WindowsFormsApplication1.Do.PathEmails))
                {
                    if (item == String.Empty) continue;
                    string entschl = item;
                    entschl = rsa.Entschlüsseln(item, "WindowsFormsApplication1").Trim();
                    if (entschl != String.Empty && entschl.Contains("@"))
                        Schlüsselverwaltung.Infos.Adressen.Add(entschl);
                }
                Schlüsselverwaltung.Infos.Speichern(false);
                Schlüsselverwaltung.Infos.Speichern(true);

            }
        }
    }
}
