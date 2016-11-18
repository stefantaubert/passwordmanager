using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Login.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        string pass;
        uint hashIterations = 100;

        [TestInitialize]
        public void Init()
        {
            pass = "123456";
            hashIterations = 100;
        }

        [TestMethod]
        public void TestCreation()
        {
            using (var tmp = TempDir.Create())
            {
                var file = Path.Combine(tmp.Name, "key.xml");

                var created = PasswordInformation.Create(file, pass, hashIterations);
                Assert.IsTrue(created.PasswordIsCorrect(pass));

                created.Save(file);

                Assert.IsTrue(created.PasswordIsCorrect(pass));

                var loaded = PasswordInformation.Load(file);
                Assert.IsTrue(loaded.PasswordIsCorrect(pass));

                Assert.IsTrue(loaded.PasswordSalt.SequenceEqual(created.PasswordSalt));
            }
        }

        [TestMethod]
        public void TestHashRenew()
        {
            using (var tmp = TempDir.Create())
            {
                var file = Path.Combine(tmp.Name, "key.xml");

                var created = PasswordInformation.Create(file, pass, hashIterations);

                created.RenewHash(pass);
                created.Save(file);

                var loaded = PasswordInformation.Load(file);
                Assert.IsTrue(loaded.PasswordIsCorrect(pass));
                Assert.IsTrue(loaded.PasswordSalt.SequenceEqual(created.PasswordSalt));
            }
        }
    }
}
