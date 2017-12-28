using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CryptedRandomString
    {
        private static readonly char[] AvailableSpecialCharacters = {
            '-', '_', '?','&','=','+','#','{','[',']','}','!','?'
        };

        private static readonly char[] AvailableNumberCharacters = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        private static readonly char[] AvailableUpperCharacters = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private static readonly char[] AvailableLowerCharacters = {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static string GenerateIdentifier(int length, bool number, bool lower, bool upper, bool special)
        {
            var identifier = new char[length];
            var randomData = new byte[length];
            var availableChars = new List<char>();

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomData);
            }

            if (upper)
            {
                availableChars.AddRange(AvailableUpperCharacters);
            }

            if (lower)
            {
                availableChars.AddRange(AvailableLowerCharacters);
            }

            if (number)
            {
                availableChars.AddRange(AvailableNumberCharacters);
            }

            if (special)
            {
                availableChars.AddRange(AvailableSpecialCharacters);
            }

            for (int i = 0; i < identifier.Length; i++)
            {
                var pos = randomData[i] % availableChars.Count;

                identifier[i] = availableChars[pos];
            }

            return new string(identifier);
        }
    }
}