using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace Umwandler_Alte_Keys_in_Neue
{
    class Class1
    {
        public void Do()
        {
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