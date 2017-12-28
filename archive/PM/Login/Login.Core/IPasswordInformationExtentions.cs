using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public static class IPasswordInformationExtentions
    {
        public static void Create(this IPasswordInformation passwordInfo, string path, string password, uint hashIterations)
        {
            var result = new PasswordInformation(password, hashIterations);

            result.Save(path);
        }

        public static IPasswordInformation Load(this IPasswordInformation passwordInfo, string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var result = default(PasswordInformation);
                var formatter = new BinaryFormatter();

                stream.Position = 0;
                result = formatter.Deserialize(stream) as PasswordInformation;
                stream.Close();

                return result;
            }
        }
    }
}
