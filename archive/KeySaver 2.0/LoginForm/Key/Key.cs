namespace LoginForm.Key
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class KeyInfo : ISerializable
    {
        public KeyInfo(string key, string startsWith)
        {
            this.Key = key;
            this.StartWith = startsWith;
        }

        public string StartWith
        {
            get;
            private set;
        }

        public string Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="KeyInfo" /> Klasse.
        /// </summary>
        /// <param name="info">Die Serialisierungsinformationen.</param>
        /// <param name="context">Der Streaming-Context.</param>
        private KeyInfo(SerializationInfo info, StreamingContext context)
        {
            this.StartWith = info.GetString("StartsWith");
            this.Key = info.GetString("Key");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StartsWith", this.StartWith);
            info.AddValue("Key", this.Key);
        }
    }
}