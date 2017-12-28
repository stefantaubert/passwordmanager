using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    [Serializable]
    public sealed class PasswordInformation : ISerializable
    {
        public event EventHandler<PasswordChangedEventArgs> PasswordChanged;

        private const int minPasswordLength = 0;

        private byte[] hashedBytes;

        private byte[] hashingSalt;

        private uint hashIterations;

        private PasswordInformation(string password, uint hashIterations)
        {
            Assert.IsTrue(password.Length >= minPasswordLength);

            this.HashPassword(password, hashIterations);
        }

        private PasswordInformation(SerializationInfo info, StreamingContext context)
        {
            this.PasswordSalt = info.GetValue("PasswordSalt", typeof(byte[])) as byte[];
            this.hashingSalt = info.GetValue("HashingSalt", typeof(byte[])) as byte[];
            this.hashedBytes = info.GetValue("HashedBytes", typeof(byte[])) as byte[];
            this.hashIterations = info.GetUInt32("HashIterations");
        }

        /// <summary>
        /// Enthält einen Salt um das Passwort in anderen Anwendung erneut zu schützen.
        /// </summary>
        public byte[] PasswordSalt
        {
            get;
            private set;
        }

        public static PasswordInformation Create(string path, string password, uint hashIterations)
        {
            var result = new PasswordInformation(password, hashIterations);

            result.Save(path);

            return result;
        }

        public static PasswordInformation Load(string path)
        {
            Assert.IsTrue(File.Exists(path));

            using (var stream = new FileStream(path, FileMode.Open))
            {
                var result = default(PasswordInformation);
                var formatter = new BinaryFormatter();

                stream.Position = 0;
                result = formatter.Deserialize(stream) as PasswordInformation;
                stream.Close();

                return result;
            }
        }

        public void ChangePassword(string oldPassword, string newPassword, uint newHashIterations)
        {
            Assert.IsTrue(PasswordIsCorrect(oldPassword));
            Assert.IsTrue(newPassword.Length >= minPasswordLength);

            var oldPasswortSalt = this.PasswordSalt;

            this.HashPassword(newPassword, newHashIterations);

            this.RaisePasswordChangedEvent(oldPassword, oldPasswortSalt, newPassword, this.PasswordSalt);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PasswordSalt", this.PasswordSalt);
            info.AddValue("HashingSalt", this.hashingSalt);
            info.AddValue("HashedBytes", this.hashedBytes);
            info.AddValue("HashIterations", this.hashIterations);
        }

        public bool PasswordIsCorrect(string password)
        {
            if (password.Length < minPasswordLength)
            {
                throw new Exception("The password is to short!");
            }

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = PasswordHasher.GenerateSaltedHash(passwordBytes, this.hashingSalt, this.hashIterations);

            return hashedBytes.SequenceEqual(this.hashedBytes);
        }

        /// <summary>
        /// TODO: hier wird auch der PasswordHash erneuert, somit muss dies auch auf das Model angewandt werden.
        /// </summary>
        /// <param name="password"></param>
        public void RenewHash(string password)
        {
            Assert.IsTrue(PasswordIsCorrect(password));

            this.HashPassword(password, this.hashIterations);
        }

        public void Save(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();

                stream.SetLength(0);
                stream.Position = 0;

                formatter.Serialize(stream, this);

                stream.Close();
            }
        }

        private void HashPassword(string password, uint hashIterations)
        {
            Assert.IsTrue(password.Length >= minPasswordLength);

            var passwordBytes = Encoding.UTF8.GetBytes(password);

            this.hashIterations = hashIterations;
            this.PasswordSalt = PasswordHasher.GetSalt(passwordBytes.Length);
            this.hashingSalt = PasswordHasher.GetSalt(passwordBytes.Length);
            this.hashedBytes = PasswordHasher.GenerateSaltedHash(passwordBytes, this.hashingSalt, hashIterations);
        }

        private void RaisePasswordChangedEvent(string oldPassword, byte[] oldPasswortSalt, string newPassword, byte[] passwordSalt)
        {
            var handler = PasswordChanged;

            if (handler != default(EventHandler<PasswordChangedEventArgs>))
            {
                handler(this, new PasswordChangedEventArgs(oldPassword, oldPasswortSalt, newPassword, passwordSalt));
            }
        }

        private void RaisePasswordChangedEvent(string newPassword)
        {

        }
    }
}