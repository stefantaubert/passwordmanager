using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeyManager
{
    [Serializable]
    internal sealed class Model : ISerializable, IModel
    {
        private List<IEntry> Keys;

        private List<ICryptedMailAddress> Mails;

        public Model(string password)
        {
            Assert.IsTrue(password.Length > 0);
            this.AESHash = PasswordHasher.GetSalt(password.Length);
            this.Keys = new List<IEntry>();
            this.Mails = new List<ICryptedMailAddress>();
            this.PasswordInfo = new PasswordInformation(this, password);
        }

        private Model(SerializationInfo info, StreamingContext context)
        {
            this.AESHash = info.GetValue("AESHash", typeof(byte[])) as byte[];
            this.Keys = info.GetValue("Keys", typeof(List<IEntry>)) as List<IEntry>;
            this.Mails = info.GetValue("Mails", typeof(List<ICryptedMailAddress>)) as List<ICryptedMailAddress>;
            this.PasswordInfo = info.GetValue("PasswordInfo", typeof(PasswordInformation)) as PasswordInformation;
        }

        public IEnumerable<IEntry> Entries
        {
            get
            {
                return this.Keys;
            }
        }

        public IEnumerable<ICryptedMailAddress> MailAdresses
        {
            get
            {
                return this.Mails;
            }
        }

        internal byte[] AESHash
        {
            get;
            set;
        }

        internal string Password
        {
            get;
            set;
        }

        internal PasswordInformation PasswordInfo
        {
            get;
            private set;
        }

        public IEntry AddEntry(string name, string content, DateTime dateTime)
        {
            var entry = new Entry(this);

            entry.Created = dateTime;
            entry.Name = name;
            entry.Content = content;

            this.Keys.Insert(0, entry);

            return entry;
        }

        public ICryptedMailAddress AddMail(string mail)
        {
            var result = new CryptedMailAddress(this);

            result.MailAddress = mail;

            this.Mails.Add(result);

            return result;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("AESHash", this.AESHash);
            info.AddValue("Keys", this.Keys);
            info.AddValue("Mails", this.Mails);
            info.AddValue("PasswordInfo", this.PasswordInfo);
        }

        public void RemoveEntry(IEntry entry)
        {
            this.Keys.Remove(entry);
        }

        public void RemoveMail(ICryptedMailAddress mail)
        {
            this.Mails.Remove(mail);
        }
    }
}