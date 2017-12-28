using System.IO;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KeyManager
{
    public static class IOHandler
    {
        /// <summary>
        /// Ändert das Passwort, ist aber nur möglich wenn das Modell bereits geladen wurde.
        /// Das Modell wird anschließend gespeichert und ist nicht mehr mit dem alten Passwort öffenbar.
        /// </summary>
        /// <param name="newPassword">Das neue Passwort.</param>
        public static void ChangeModelPassword(IModel model, string oldPassword, string newPassword)
        {
            var mainModel = model as Model;

            if (newPassword.Length == 0 || oldPassword.Length == 0)
            {
                throw new ArgumentException("Empty passwords are not allowed!", "password");
            }

            if (!mainModel.PasswordInfo.PasswordIsCorrect(oldPassword))
            {
                throw new WrongPasswordException();
            }

            foreach (Entry item in mainModel.Entries)
            {
                item.ChangePassword(newPassword);
            }

            mainModel.AESHash = PasswordHasher.GetSalt(newPassword.Length);
            mainModel.Password = newPassword;

            mainModel.PasswordInfo.HashPassword(newPassword);
        }

        public static IModel CreateModelFile(string path, string password)
        {
            if (password.Length == 0)
            {
                throw new ArgumentException("Empty passwords are not allowed!", "password");
            }

            var model = new Model(password);

            model.SerializeFile(path);

            return model;
        }

        /// <summary>
        /// Lädt das Modell aus der Datei, vorrausgesetzt ist ein gültiges Passwort.
        /// Andernfalls wird eine WrongPasswordException geworfen.
        /// </summary>
        /// <param name="path">Der Pfad zum Modell.</param>
        /// <param name="password">Das Passwort.</param>
        /// <returns></returns>
        public static IModel LoadModelFromFile(string path, string password)
        {
            var model = default(Model);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            
            if (!model.PasswordInfo.PasswordIsCorrect(password))
            {
                throw new WrongPasswordException();
            }

            model.Password = password;

            return model;
        }

        /// <summary>
        /// Speichert das aktuell geladene Modell.
        /// </summary>
        public static void SaveModelToFile(string path, IModel model)
        {
            if (model == default(IModel))
            {
                throw new ArgumentNullException("model");
            }

            model.SerializeFile(path);
        }
    }
}
