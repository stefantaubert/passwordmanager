using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public static class PasswordHasher
    {
        public static byte[] GetSalt(int length)
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[length];

                rng.GetBytes(salt);

                return salt;
            }
        }

        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt, uint hashIterations)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            Array.Copy(plainText, 0, plainTextWithSaltBytes, 0, plainText.Length);
            Array.Copy(salt, 0, plainTextWithSaltBytes, plainText.Length, salt.Length);

            var hash = plainTextWithSaltBytes;

            for (int i = 0; i < hashIterations; i++)
            {
                hash = algorithm.ComputeHash(hash);
            }

            return hash;
        }
    }
}
