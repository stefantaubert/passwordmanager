using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StreamCryptor.Tests
{
    [Serializable]
    class TestClass : ISerializable
    {
        public string TestProperty
        {
            get;
            set;
        }

        public TestClass()
        {
        }

        public TestClass(SerializationInfo info, StreamingContext context)
        {
            this.TestProperty = info.GetString("TestProperty");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TestProperty", this.TestProperty);
        }
    }
}
