using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PM.Core
{
    [Serializable]
    public class Entry : ISerializable
    {
        public Entry()
        {
        }

        public Entry(string name) :
            this(name, DateTime.Now)
        {
        }

        public Entry(string name, DateTime created)
        {
            this.Label = name;
            this.Created = created;
            this.Content = string.Empty;
            this.Requests = 0;
        }

        protected Entry(SerializationInfo info, StreamingContext context)
        {
            this.Label = info.GetString("Label");
            this.Content = info.GetString("Content");
            this.Requests = info.GetInt32("Requests");
            this.Created = info.GetDateTime("Created");
        }

        [XmlElement("Content")]
        public string Content
        {
            get;
            set;
        }

        [XmlElement("Created")]
        public DateTime Created
        {
            get;
            set;
        }

        [XmlElement("Label")]
        public string Label
        {
            get;
            set;
        }

        [XmlElement("Requests")]
        public int Requests
        {
            get;
            set;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Label", this.Label);
            info.AddValue("Content", this.Content);
            info.AddValue("Requests", this.Requests);
            info.AddValue("Created", this.Created);
        }
    }
}