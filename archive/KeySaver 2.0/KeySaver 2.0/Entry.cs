using System;
using System.Runtime.Serialization;

namespace KeySaver
{
    [Serializable]
    internal sealed class Entry : ISerializable
    {
        public Entry()
        {
            this.Created = DateTime.Now;
        }

        private Entry(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("Name").Decrypt(Model.Key);
            this.Content = info.GetString("Content").Decrypt(Model.Key);
            this.Requests = Convert.ToInt32(info.GetString("Requests").Decrypt(Model.Key));
            this.Created = new DateTime(Convert.ToInt64(info.GetString("Created").Decrypt(Model.Key)));
        }

        public string Content
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Requests
        {
            get;
            set;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name.Encrypt(Model.Key));
            info.AddValue("Content", this.Content.Encrypt(Model.Key));
            info.AddValue("Requests", this.Requests.ToString().Encrypt(Model.Key));
            info.AddValue("Created", this.Created.Ticks.ToString().Encrypt(Model.Key));
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}