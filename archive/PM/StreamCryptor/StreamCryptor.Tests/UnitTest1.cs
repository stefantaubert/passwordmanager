using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamCryptor;
using System.Text;

namespace StreamCryptor.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Crypt();
            Decrypt();
        }

        private static void Crypt()
        {
            var test = new TestClass();

            test.TestProperty = "testtes";

            string pass = "tes";
            string salt = "testeste";

            SecureSerialization.SerializeObjectToFile("B:\\passwd.xml", test, Encoding.UTF8.GetBytes(pass), Encoding.UTF8.GetBytes(salt), 100, 1000);
        }

        private static void Decrypt()
        {
            string pass = "tes";
            string salt = "testeste";

            var result = SecureSerialization.DeserializeObjectFromFile("B:\\passwd.xml", Encoding.UTF8.GetBytes(pass), Encoding.UTF8.GetBytes(salt), 100, 1000);
        }
    }
}
