using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KeyManager
{
    public interface ICryptedMailAddress : ISerializable
    {
        string MailAddress
        {
            get;
        }
    }
}
