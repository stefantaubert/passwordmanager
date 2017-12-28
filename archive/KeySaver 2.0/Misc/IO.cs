using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security;
using System.IO;
using System.Reflection;

namespace Misc
{
    public static class IO
    {
        /// <summary>
        /// Lädt das ISerializable-Objekt aus der angegebenen Datei.
        /// </summary>
        /// <typeparam name="T">Der Typ des zu deserialisierenden Obejektes.</typeparam>
        /// <param name="fileName">Der Dateipfad, welcher das Model enthält.</param>
        /// <returns>Gibt das ISerializable-Objekt zurück, oder wirft eine Exception.</returns>
        public static T DezerializeFile<T>(this T item, string fileName)
            where T : class, ISerializable
        {
            var stream = new FileStream(fileName, FileMode.Open);

            var result = item.DeserializeStream<T>(stream);

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
        private static T DeserializeStream<T>(this T item, Stream stream)
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
        public static bool SerializeStream<T>(this T item, string fileName)
           where T : class, ISerializable
        {
            var result = true;

            try
            {
                var stream = new FileStream(fileName, FileMode.OpenOrCreate);

                result = item.SerializeStream(stream);

                stream.Close();
            }
            catch (IOException)
            {
                result = false;
            }
            catch (SecurityException)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// Speichert das angegebene ISerializable-Objekt in den Stream.
        /// </summary>
        /// <typeparam name="T">Der Typ zu serialisierenden Modells..</typeparam>
        /// <param name="model">Das zu serialisierende Modell.</param>
        /// <param name="stream">Der Stream, in dem geschrieben wird.</param>
        /// <returns>True, wenn das Speiuchern erfolgreich war.</returns>
        private static bool SerializeStream<T>(this T model, Stream stream)
            where T : class, ISerializable
        {
            var result = true;
            var formatter = new BinaryFormatter();

            try
            {
                stream.SetLength(0);

                stream.Position = 0;

                formatter.Serialize(stream, model);
            }
            catch (SerializationException)
            {
                result = false;
            }
            catch (SecurityException)
            {
                result = false;
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }
    }
}
