using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KeyManager
{
    [Serializable]
    internal sealed class PasswordInformation : ISerializable
    {
        public PasswordInformation(Model model, string password)
        {
            Assert.IsTrue(password.Length > 0);

            this.HashPassword(password);
        }

        private PasswordInformation(SerializationInfo info, StreamingContext context)
        {
            this.Salt = info.GetValue("Salt", typeof(byte[])) as byte[];
            this.HashedBytes = info.GetValue("HashedBytes", typeof(byte[])) as byte[];
        }

        public byte[] Salt
        {
            get;
            private set;
        }

        public byte[] HashedBytes
        {
            get;
            private set;
        }

        public bool PasswordIsCorrect(string password)
        {
            Assert.IsTrue(password.Length > 0);

            var passwordBytes = Convert.FromBase64String(password);
            var hashedBytes = PasswordHasher.GenerateSaltedHash(passwordBytes, this.Salt);

            return hashedBytes.SequenceEqual(this.HashedBytes);
        }

        public void HashPassword(string password)
        {
            var passwordBytes = Convert.FromBase64String(password);

            this.Salt = PasswordHasher.GetSalt(passwordBytes.Length);
            this.HashedBytes = PasswordHasher.GenerateSaltedHash(passwordBytes, this.Salt);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Salt", this.Salt);
            info.AddValue("HashedBytes", this.HashedBytes);
        }
    }
}