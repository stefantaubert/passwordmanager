namespace Common
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class CryptoString
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="bytesToBeEncrypted"></param>
        ///// <param name="passwordBytes"></param>
        ///// <returns></returns>
        ///// <remarks>http://www.codeproject.com/Articles/769741/Csharp-AES-bits-Encryption-Library-with-Salt</remarks>
        //public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        //{
        //    byte[] encryptedBytes = null;

        //    // Set your salt here, change it to meet your flavor:
        //    // The salt bytes must be at least 8 bytes.
        //    byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (RijndaelManaged AES = new RijndaelManaged())
        //        {
        //            AES.KeySize = 256;
        //            AES.BlockSize = 128;

        //            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        //            AES.Key = key.GetBytes(AES.KeySize / 8);
        //            AES.IV = key.GetBytes(AES.BlockSize / 8);

        //            AES.Mode = CipherMode.CBC;

        //            using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
        //                cs.Close();
        //            }

        //            encryptedBytes = ms.ToArray();
        //        }
        //    }

        //    return encryptedBytes;
        //}
        //public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        //{
        //    byte[] decryptedBytes = null;

        //    // Set your salt here, change it to meet your flavor:
        //    // The salt bytes must be at least 8 bytes.
        //    byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (RijndaelManaged AES = new RijndaelManaged())
        //        {
        //            AES.KeySize = 256;
        //            AES.BlockSize = 128;

        //            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        //            AES.Key = key.GetBytes(AES.KeySize / 8);
        //            AES.IV = key.GetBytes(AES.BlockSize / 8);

        //            AES.Mode = CipherMode.CBC;

        //            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
        //                cs.Close();
        //            }

        //            decryptedBytes = ms.ToArray();
        //        }
        //    }

        //    return decryptedBytes;
        //}

        public static byte[] AES_Encrypt(byte[] bytesToBeTransformed, byte[] passwordBytes, byte[] saltBytes)
        {
            return AES_Core(bytesToBeTransformed, passwordBytes, saltBytes, true);
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeTransformed, byte[] passwordBytes, byte[] saltBytes)
        {
            return AES_Core(bytesToBeTransformed, passwordBytes, saltBytes, false);
        }

        private static byte[] AES_Core(byte[] bytesToBeTransformed, byte[] passwordBytes, byte[] saltBytes, bool encrypt)
        {
            byte[] result = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    ICryptoTransform transform = encrypt ? AES.CreateEncryptor() : AES.CreateDecryptor();

                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeTransformed, 0, bytesToBeTransformed.Length);
                        cs.Close();
                    }

                    result = ms.ToArray();
                }
            }

            return result;
        }

        public static string Encrypt(this DateTime dateTime, string key)
        {
            return Encrypt(dateTime.Ticks.ToString(), key);
        }

        public static string Encrypt(this int number, string key)
        {
            return Encrypt(number.ToString(), key);
        }

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

        public static int DecryptAsInt(this string text, string key)
        {
            return Convert.ToInt32(DecryptAsString(text, key));
        }

        public static DateTime DecryptAsDateTime(this string text, string key)
        {
            return new DateTime(Convert.ToInt64(DecryptAsString(text, key)));
        }

        /// <summary>
        /// Entschlüsseln.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptAsString(this string text, string key)
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