namespace Misc
{
    using System;

    public static class RandomString
    {
        private static Random random;

        static RandomString()
        {
            RandomString.random = new Random();
        }

        public static string Generate(int length)
        {
            return Generate(length, true, true, true, true);
        }

        public static string Generate(int length, bool numbers, bool lower, bool upper, bool special)
        {
            string
                lowerChars = "abcdefghijklmnopqrstuvwxyz",
                higherChars = lowerChars.ToUpper(),
                specialChars = "&=ß+-#{[]}!?._",
                zahlen = "0123456789", result = string.Empty,
                chars = string.Empty;

            if (lower)
                chars += lowerChars;

            if (upper)
                chars += higherChars;

            if (special)
                chars += specialChars;

            if (numbers)
                chars += zahlen;

            if (!string.IsNullOrWhiteSpace(chars))
                for (int i = 0; i < length; i++)
                    result += chars.Substring(random.Next(0, chars.Length), 1);

            return result;
        }
    }
}