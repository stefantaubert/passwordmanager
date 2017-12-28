namespace KeySaver
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    internal static class CryptoString
    {
        /// <summary>
        /// Verschlüsseln
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(this string text, string key)
        {
            var result = string.Empty;
            var manager = new RijndaelManaged();
            var provider = new MD5CryptoServiceProvider();
            var keyBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            var data = Encoding.UTF8.GetBytes(text);
            var stream = new MemoryStream();
            var cs = default(CryptoStream);

            provider.Clear();

            manager.Key = keyBytes;
            manager.GenerateIV();

            stream.Write(manager.IV, 0, manager.IV.Length);

            cs = new CryptoStream(stream, manager.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();

            result = Convert.ToBase64String(stream.ToArray());

            cs.Close();
            manager.Clear();

            return result;
        }

        /// <summary>
        /// Entschlüsseln.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(this string text, string key)
        {
            var result = string.Empty;
            var manager = new RijndaelManaged();
            var length = 16;
            var provider = new MD5CryptoServiceProvider();
            var keyBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            var stream = default(MemoryStream);
            var iv = new byte[16];
            var cs = default(CryptoStream);
            var data = default(byte[]);

            provider.Clear();

            try
            {
                stream = new MemoryStream(Convert.FromBase64String(text));
                stream.Read(iv, 0, length);
                manager.IV = iv;
                manager.Key = keyBytes;
                cs = new CryptoStream(stream, manager.CreateDecryptor(), CryptoStreamMode.Read);
                data = new byte[stream.Length - length];
                result = Encoding.UTF8.GetString(data, 0, cs.Read(data, 0, data.Length));
                cs.Close();
            }
            catch
            {
            }
            finally
            {
                manager.Clear();
            }

            return result;
        }
    }
}