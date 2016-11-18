using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using static StreamCryptor.AES;

namespace StreamCryptor
{
    public static class SecureSerialization
    {
        public static object DeserializeObjectFromFile(string fileName, byte[] password, byte[] salt, uint decryptionRounds, int keyDerivations)
        {
            var decryptedBytes = File.ReadAllBytes(fileName);
            var encryptedBytes = Decrypt(decryptedBytes, password, salt, decryptionRounds, keyDerivations);

            return DeserializeBytesToObject(encryptedBytes);
        }

        public static void SerializeObjectToFile(string fileName, object obj, byte[] password, byte[] salt, uint encryptionRounds, int keyDerivations)
        {
            var encryptedBytes = SerializeBinaryToBytes(obj);
            var decryptedBytes = Encrypt(encryptedBytes, password, salt, encryptionRounds, keyDerivations);

            File.WriteAllBytes(fileName, decryptedBytes);
        }

        private static byte[] ConvertToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return bytes;
        }

        private static object DeserializeBytesToObject(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter();

                memoryStream.Seek(0, SeekOrigin.Begin);

                return formatter.Deserialize(memoryStream);
            }
        }

        private static byte[] SerializeBinaryToBytes(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(memoryStream, obj);

                return ConvertToBytes(memoryStream);
            }
        }
    }
}