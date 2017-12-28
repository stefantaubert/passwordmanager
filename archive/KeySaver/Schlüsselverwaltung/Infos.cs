using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Login;

namespace Schlüsselverwaltung
{
    public static class Infos
    {
        private static string
            pKeys = Application.StartupPath + "\\keys.xml",
            pAdresses = Application.StartupPath + "\\adresses.xml",
            pRegistryeintrag = Application.StartupPath + "\\Registryeintrag.reg",
            RegBeginn = "REG#A",
            RegEnde = "REG#B";

        public static List<Eintrag> Keys { get; set; }
        public static List<string> Adressen { get; set; }

        public static int AktInd { get; set; }
        public static string AktKey
        {
            get
            {
                return GetEintrag(false);
            }
            set
            {
                ChangeEintrag(value, false);
            }
        }
        public static string AktName
        {
            get
            {
                return GetEintrag(true);
            }
            set
            {
                ChangeEintrag(value, true);
            }
        }

        public static bool AktKeyContainsReg { get { return GetRegistryEintrag() != ""; } }

        static void ChangeEintrag(string value, bool name)
        {
            Keys[AktInd].Entschlüsseln();
            if (name) Keys[AktInd].Name = value;
            else Keys[AktInd].Key = value;
            Keys[AktInd].Verschlüsseln();
        }
        static string GetEintrag(bool name)
        {
            string ausg;
            Keys[AktInd].Entschlüsseln();
            if (name) ausg = Keys[AktInd].Name;
            else ausg = Keys[AktInd].Key;
            Keys[AktInd].Verschlüsseln();
            return ausg;
        }

        public static string Filter { get; set; }
        public static bool EinträgeEinschließen { get; set; }
        public static List<Eintrag> GefilterteEinträge
        {
            get
            {
                List<Eintrag> ausg = new List<Eintrag>();
                for (int i = 0; i < Keys.Count; i++)
                {
                    AktInd = i;
                    if (Filter.Trim() == "" || AktName.ToLower().Contains(Filter.Trim().ToLower()) || (EinträgeEinschließen && AktKey.Contains(Filter)))
                        ausg.Add(Keys[i]);
                }
                return ausg;
            }
        }

        public static bool Login()
        {
            return Verwaltung.Anmelden();
        }
        public static void Laden()
        {
            AktInd = -1;
            Keys = Deserialisieren<List<Eintrag>>(pKeys);
            Adressen = Deserialisieren<List<string>>(pAdresses);
            if (Keys == null) Keys = new List<Eintrag>();
            if (Adressen == null) Adressen = new List<string>();
            Filter = "";
            EinträgeEinschließen = false;
            for (int i = 0; i < Adressen.Count; i++)
                Adressen[i] = Verwaltung.Entschlüsseln(Adressen[i]);
            IndexesSetzen();
        }
        public static void Speichern(bool adressen)
        {
            if (adressen)
            {
                List<string> tmp = new List<string>(Adressen);
                for (int i = 0; i < Adressen.Count; i++)
                    Adressen[i] = Verwaltung.Verschlüsseln(Adressen[i]);
                Serialisieren<List<string>>(Adressen, pAdresses);
                Adressen = new List<string>(tmp);
            }
            else Serialisieren<List<Eintrag>>(Keys, pKeys);
        }

        public static void RegistryEintragHinzufügen(string path)
        {
            AktKey = AktKey + "\n\nRegistry Eintrag:\n" + RegBeginn + File.ReadAllText(path) + RegEnde;
        }
        private static string GetRegistryEintrag()
        {
            int a = AktKey.IndexOf(RegBeginn), b = AktKey.IndexOf(RegEnde);
            return (a != -1 && b != -1 && a < b) ? AktKey.Substring(a + RegBeginn.Length, b - a - RegBeginn.Length) : "";
        }
        public static void RegistryEintragAktivieren()
        {
            if (!AktKeyContainsReg) return;
            File.WriteAllText(pRegistryeintrag, GetRegistryEintrag());
            System.Diagnostics.Process.Start(pRegistryeintrag);
        }

        public static void Neu(string name, string key)
        {
            Eintrag e = new Eintrag() { Name = name, Key = key };
            e.Verschlüsseln();
            Keys.Insert(0, e);
            IndexesSetzen();
            //   AktInd = Keys.Count - 1;
        }
        public static void Löschen()
        {
            Keys.RemoveAt(AktInd);
            IndexesSetzen();
        }
        public static void Ändern(string eintrag, bool name)
        {
            if (name) AktName = eintrag;
            else AktKey = eintrag;
        }
        private static void IndexesSetzen()
        {
            for (int i = 0; i < Keys.Count; i++)
                Keys[i].TmpInd = i;
        }

        private static void Serialisieren<T>(T obj, string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Create))
                new XmlSerializer(typeof(T)).Serialize(fs, obj);
        }
        private static T Deserialisieren<T>(string file)
        {
            //  return File.Exists(file) ? ((T)(new XmlSerializer(typeof(T))).Deserialize(new FileStream(file, FileMode.Open))) : default(T);
            if (!File.Exists(file)) return default(T);
            else using (FileStream fs = new FileStream(file, FileMode.Open))
                    return (T)(new XmlSerializer(typeof(T))).Deserialize(fs);
        }
    }
}