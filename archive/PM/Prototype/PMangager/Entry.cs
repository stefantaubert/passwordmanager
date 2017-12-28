using System;
using Common;
using System.Runtime.Serialization;

namespace KeyManager
{
    [Serializable]
    internal sealed class Entry : ISerializable, IEntry
    {
        private string content;
        private string requests;
        private string created;
        private string name;

        private Model model;

        public Entry(Model model)
        {
            this.model = model;
        }

        private Entry(SerializationInfo info, StreamingContext context)
        {
            this.model = info.GetValue("Model", typeof(Model)) as Model;
            this.name = info.GetString("Name");
            this.content = info.GetString("Content");
            this.requests = info.GetString("Requests");
            this.created = info.GetString("Created");
        }

        public string Content
        {
            get
            {
                return this.content.DecryptAsString(this.model.Password);
            }
            set
            {
                this.content = value.Encrypt(this.model.Password);
            }
        }

        internal void ChangePassword(string newPassword)
        {
            this.created = this.Created.Encrypt(newPassword);
            this.name = this.Name.Encrypt(newPassword);
            this.requests = this.Requests.Encrypt(newPassword);
            this.content = this.Content.Encrypt(newPassword);
        }

        public DateTime Created
        {
            get
            {
                return this.created.DecryptAsDateTime(this.model.Password);
            }
            set
            {
                this.created = value.Encrypt(this.model.Password);
            }
        }

        public string Name
        {
            get
            {
                return this.name.DecryptAsString(this.model.Password);
            }
            set
            {
                this.name = value.Encrypt(this.model.Password);
            }
        }

        public int Requests
        {
            get
            {
                return this.requests.DecryptAsInt(this.model.Password);
            }
            set
            {
                this.content = value.Encrypt(this.model.Password);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Model", this.model);
            info.AddValue("Name", this.name);
            info.AddValue("Content", this.content);
            info.AddValue("Requests", this.requests);
            info.AddValue("Created", this.created);
        }
    }
}