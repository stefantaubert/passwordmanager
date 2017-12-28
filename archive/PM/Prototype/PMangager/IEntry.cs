using System;
using System.Runtime.Serialization;

namespace KeyManager
{
    public interface IEntry : ISerializable
    {
        string Content
        {
            get;
            set;
        }

        DateTime Created
        {
            get;
        }

        string Name
        {
            get;
            set;
        }

        int Requests
        {
            get;
            set;
        }
    }
}
