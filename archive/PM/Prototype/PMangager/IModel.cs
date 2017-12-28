using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeyManager
{
    public interface IModel : ISerializable
    {
        IEnumerable<IEntry> Entries
        {
            get;
        }

        IEnumerable<ICryptedMailAddress> MailAdresses
        {
            get;
        }

        void RemoveEntry(IEntry entry);

        void RemoveMail(ICryptedMailAddress mail);

        IEntry AddEntry(string name, string content, DateTime dateTime);

        /// <summary>
        /// Fügt am Ende der Auflistung die Mailaddresse hinzu
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        ICryptedMailAddress AddMail(string mail);
    }
}
