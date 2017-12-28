using System;
using System.Collections.Generic;
using System.Text;

namespace Schlüsselverwaltung
{
    public class Eintrag
    {
        public string Name { get; set; }
        public string Key { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int TmpInd { get; set; }

        public void Verschlüsseln()
        {
            if (Name == null || Key == null)
            {
            }
            Name = Login.Verwaltung.Verschlüsseln(Name);
            Key = Login.Verwaltung.Verschlüsseln(Key);
        }
        public void Entschlüsseln()
        {
            if (Name == null || Key == null)
            {
            }
            Name = Login.Verwaltung.Entschlüsseln(Name);
            Key = Login.Verwaltung.Entschlüsseln(Key);
        }
    }
}
