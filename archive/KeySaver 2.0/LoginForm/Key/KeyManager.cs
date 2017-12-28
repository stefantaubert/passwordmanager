namespace LoginForm.Key
{
    using System;
    using System.IO;
    using Misc;

    public sealed class KeyManager
    {
        private KeyInfo key;
        private readonly string fileName;

        private const int KeyLength = 1024;
        private const int KeySubLength = 64;

        public KeyManager(string fileName)
        {
            this.fileName = fileName;

            if (!File.Exists(this.fileName))
            {
                throw new Exception("File not exists");
            }

            this.key = this.key.DezerializeFile(this.fileName);

            if (this.key == default(KeyInfo))
            {
                throw new Exception("Loading failed");
            }
        }

        public KeyManager(string fileName, string pwd)
        {
            this.fileName = fileName;

            this.SetPassword(pwd);
        }

        public bool CheckPassword(string pwd)
        {
            return this.key.Key.Decrypt(pwd).StartsWith(this.key.StartWith);
        }

        private bool SetPassword(string pwd)
        {
            var key = RandomString.Generate(KeyLength);
            this.key = new KeyInfo(key.Encrypt(pwd), key.Substring(0, KeySubLength));
            return this.key.SerializeStream(this.fileName);
        }

        public bool ChangePassword(string oldpwd, string pwd)
        {
            var old = this.key.Key.Decrypt(oldpwd);
            this.key = new KeyInfo(old.Encrypt(pwd), old.Substring(0, KeySubLength));
            return this.key.SerializeStream(this.fileName);
        }

        internal string GetKey(string pwd)
        {
            return this.key.Key.Decrypt(pwd);
        }
    }
}
