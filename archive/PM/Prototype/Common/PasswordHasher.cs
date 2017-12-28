using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
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

        //internal static byte[] GenerateSaltedHash(string plainText)
        //{
        //    var passwordBytes = Convert.FromBase64String(plainText);

        //    var hash = GetSalt(passwordBytes.Length);

        //    return GenerateSaltedHash(passwordBytes, hash);
        //}

        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            Array.Copy(plainText, 0, plainTextWithSaltBytes, 0, plainText.Length);
            Array.Copy(salt, 0, plainTextWithSaltBytes, plainText.Length, salt.Length);

            //for (int i = 0; i < plainText.Length; i++)
            //{
            //    plainTextWithSaltBytes[i] = plainText[i];
            //}

            //for (int i = 0; i < salt.Length; i++)
            //{
            //    plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            //}

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        //internal static bool CompareByteArrays(byte[] array1, byte[] array2)
        //{
        //    return array1.SequenceEqual(array2);

        //    //if (array1.Length != array2.Length)
        //    //{
        //    //    return false;
        //    //}

        //    //for (int i = 0; i < array1.Length; i++)
        //    //{
        //    //    if (array1[i] != array2[i])
        //    //    {
        //    //        return false;
        //    //    }
        //    //}

        //    //return true;
        //}
    }
}
