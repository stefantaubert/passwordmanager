using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PM.Core
{
    [Serializable]
    public class Model : ISerializable
    {
        public Model()
        {
            this.Entries = new List<Entry>();
            this.MailAdresses = new List<string>();
        }

        protected Model(SerializationInfo info, StreamingContext context)
        {
            this.Entries = info.GetValue("Keys", typeof(List<Entry>)) as List<Entry>;
            this.MailAdresses = info.GetValue("Mails", typeof(List<string>)) as List<string>;
        }

        [XmlElement("Entries")]
        public List<Entry> Entries
        {
            get;
            private set;
        }

        [XmlElement("MailAddresses")]
        public List<string> MailAdresses
        {
            get;
            private set;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Keys", this.Entries);
            info.AddValue("Mails", this.MailAdresses);
        }
    }
}