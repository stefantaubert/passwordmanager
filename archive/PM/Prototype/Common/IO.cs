namespace Common
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security;

    public static class IO
    {
        /// <summary>
        /// Lädt das ISerializable-Objekt aus der angegebenen Datei.
        /// </summary>
        /// <typeparam name="T">Der Typ des zu deserialisierenden Obejektes.</typeparam>
        /// <param name="fileName">Der Dateipfad, welcher das Model enthält.</param>
        /// <returns>Gibt das ISerializable-Objekt zurück, oder wirft eine Exception.</returns>
        public static T DezerializeFile<T>(string fileName)
            where T : class, ISerializable
        {
            var stream = new FileStream(fileName, FileMode.Open);

            var result = DeserializeStream<T>(stream);

            stream.Close();

            return result;
        }

        /// <summary>
        /// Lädt das ISerializable-Objekt aus dem angegebenen Stream.
        /// </summary>
        /// <typeparam name="T">Der Typ des zu deserialisierenden Objektes.</typeparam>
        /// <param name="stream">Der Stream, welcher das ISerializable-Objekt enthält.</param>
        /// <param name="includeTemporaries">True, wenn temporäre Felder geladen werden sollen.</param>
        /// <returns>Gibt das ISerializable-Objekt zurück, oder wirft eine Exception.</returns>
        private static T DeserializeStream<T>(Stream stream)
            where T : class, ISerializable
        {
            var formatter = new BinaryFormatter();

            try
            {
                stream.Position = 0;

                var result = (T)formatter.Deserialize(stream);

                return result;
            }
            catch (SecurityException ex)
            {
            }
            catch (SerializationException ex)
            {
            }
            catch (TargetInvocationException ex)
            {
            }

            return default(T);
        }

        /// <summary>
        /// Speichert das angegebene ISerializable-Objekt in die Datei.
        /// </summary>
        /// <typeparam name="T">Der Typ des ISerializable-Objektes.</typeparam>
        /// <param name="item">Das ISerializable-Objekt.</param>
        /// <param name="fileName">Der Pfad zu Datei, in die gespeichert wird.</param>
        /// <returns>True, wenn das Speiuchern erfolgreich war.</returns>
        public static void SerializeFile<T>(this T item, string fileName)
           where T : class, ISerializable
        {
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }

            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                item.SerializeStream(stream);

                stream.Close();
            }
        }

        /// <summary>
        /// Speichert das angegebene ISerializable-Objekt in den Stream.
        /// </summary>
        /// <typeparam name="T">Der Typ zu serialisierenden Modells..</typeparam>
        /// <param name="model">Das zu serialisierende Modell.</param>
        /// <param name="stream">Der Stream, in dem geschrieben wird.</param>
        /// <returns>True, wenn das Speiuchern erfolgreich war.</returns>
        private static void SerializeStream<T>(this T model, Stream stream)
            where T : class, ISerializable
        {
            var formatter = new BinaryFormatter();

            stream.SetLength(0);

            stream.Position = 0;

            formatter.Serialize(stream, model);
        }
    }
}