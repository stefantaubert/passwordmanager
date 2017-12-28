namespace StreamCryptor
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    internal static class AES
    {
        public static byte[] Encrypt(byte[] bytesToBeTransformed, byte[] passwordBytes, byte[] saltBytes, uint rounds, int keyDerivations)
        {
            return Core_Rounds(bytesToBeTransformed, passwordBytes, saltBytes, true, rounds, keyDerivations);
        }

        public static byte[] Decrypt(byte[] bytesToBeTransformed, byte[] passwordBytes, byte[] saltBytes, uint rounds, int keyDerivations)
        {
            return Core_Rounds(bytesToBeTransformed, passwordBytes, saltBytes, false, rounds, keyDerivations);
        }

        private static byte[] Core_Rounds(byte[] bytesToBeTransformed, byte[] passwordBytes, byte[] saltBytes, bool encrypt, uint rounds, int keyDerivations)
        {
            var result = bytesToBeTransformed;

            for (int i = 0; i < rounds; i++)
            {
                result = Core(result, passwordBytes, saltBytes, keyDerivations, encrypt);
            }

            return result;
        }

        private static byte[] Core(byte[] bytesToBeTransformed, byte[] passwordBytes, byte[] saltBytes, int iterations, bool encrypt)
        {
            Assert.IsTrue(saltBytes.Length >= 8);

            using (var ms = new MemoryStream())
            {
                byte[] result = null;

                using (var aes = new RijndaelManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, iterations);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);

                    aes.Mode = CipherMode.CBC;

                    ICryptoTransform transform = encrypt ? aes.CreateEncryptor() : aes.CreateDecryptor();

                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeTransformed, 0, bytesToBeTransformed.Length);

                        cs.Close();
                    }

                    result = ms.ToArray();
                }

                ms.Close();

                return result;
            }
        }
    }
}