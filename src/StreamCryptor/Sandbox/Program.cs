using StreamCryptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main()
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
