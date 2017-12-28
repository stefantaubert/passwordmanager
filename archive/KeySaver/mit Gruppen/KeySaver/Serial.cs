using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

/// <summary>
/// Generische Implementierung einer XML-Serialisation
/// </summary>
/// <typeparam name="T">
/// generischer Typ
/// </typeparam>
public static class XmlSerialisierung<T> where T : class
{
    /// <summary>
    /// Serialisiert das Objekt
    /// Achtung: Serializiert nur pulic Properties (mit get und >set<)
    /// </summary>
    /// <param name="obj">
    /// zu serialisierendes Objekt
    /// </param>
    /// <param name="file">
    /// XML-Datei in die serialisiert wird
    /// </param>
    public static void Serialisieren(T obj, string file)
    {
        XmlSerializer xmlSer = new XmlSerializer(typeof(T));

        using (FileStream fs = new FileStream(file, FileMode.Create))
        {
            xmlSer.Serialize(fs, obj);
        }
    }
    /// <summary>
    /// Deserialisiert das Objekt
    /// </summary>
    /// <param name="file">
    /// XML-Datei
    /// </param>
    /// <returns>
    /// deserialisiertes Objekt
    /// </returns>
    public static T Deserialisieren(string file)
    {
        //if (!File.Exists(file)) return null;
        XmlSerializer xmlSer = new XmlSerializer(typeof(T));

        using (FileStream fs = new FileStream(file, FileMode.Open))
        {
            return xmlSer.Deserialize(fs) as T;
        }
    }
}
