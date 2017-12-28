namespace KeyManager
{
    using System;
    using Common;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class CryptedMailAddress : ISerializable, ICryptedMailAddress
    {
        private Model model;

        private string mailAddress;

        public CryptedMailAddress(Model model)
        {
            this.model = model;
        }

        private CryptedMailAddress(SerializationInfo info, StreamingContext context)
        {
            this.model = info.GetValue("Model", typeof(Model)) as Model;
            this.mailAddress = info.GetString("MailAddress");
        }

        public string MailAddress
        {
            get
            {
                return this.mailAddress.DecryptAsString(this.model.Password);
            }
            set
            {
                this.mailAddress = value.Encrypt(this.model.Password);
            }
        }

        internal void ChangePassword(string newPassword)
        {
            this.mailAddress = this.MailAddress.Encrypt(newPassword);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Model", this.model);
            info.AddValue("MailAddress", this.mailAddress);
        }

        /// <summary>
        /// Gibt die aktuelle Addresse zurück.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MailAddress;
        }
    }
}