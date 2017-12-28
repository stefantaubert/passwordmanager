namespace KeySaver
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class CryptedMailAdress : ISerializable
    {
        public CryptedMailAdress(string mail)
        {
            this.Mail = mail;
        }

        private CryptedMailAdress(SerializationInfo info, StreamingContext context)
        {
            this.Mail = info.GetString("Mail").Decrypt(Model.Key);
        }

        public string Mail
        {
            get;
            private set;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Mail", this.Mail.Encrypt(Model.Key));
        }
    }
}