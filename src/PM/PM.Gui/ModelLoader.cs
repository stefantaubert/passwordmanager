using LoginGui;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PM.Core;
using PM.Gui.Properties;
using StreamCryptor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LoginGui.PasswordInfoLoader;

namespace PM.Gui
{
    public static class ModelLoader
    {
        private static AuthData authenticationResult;

        public static Model CurrentModel
        {
            get;
            private set;
        }

        public static void ReplaceCurrentModelWithImportedModel(Model importedModel)
        {
            Assert.IsNotNull(importedModel);

            CurrentModel = importedModel;
        }

        private static string ModelPath
        {
            get
            {
                return Settings.Default.AbsolutePaths ? Settings.Default.ModelPath : Path.Combine(Application.StartupPath, Settings.Default.ModelPath);
            }
        }

        public static void LoadModel(AuthData authData)
        {
            Assert.IsTrue(authData.Success);

            authenticationResult = authData;

            if (File.Exists(ModelPath))
            {
                var model = SecureSerialization.DeserializeObjectFromFile(ModelPath, authenticationResult.EnteredPasswordAsBytes, authenticationResult.PasswordSalt, Settings.Default.AESIterations, Settings.Default.AESKeyIterations);

                CurrentModel = model as Model;

                //PasswordInfoLoader.Rehash(Settings.Default.KeyPath, authenticationResult.EnteredPasswordAsString);
            }

            if (CurrentModel == default(Model))
            {
                CurrentModel = new Model();

                SaveModel();
            }
        }

        public static void ChangeAuthData(AuthData oldData, AuthData newData)
        {
            Assert.IsTrue(oldData.Success);
            Assert.IsTrue(newData.Success);

            if (File.Exists(ModelPath))
            {
                var model = SecureSerialization.DeserializeObjectFromFile(ModelPath, oldData.EnteredPasswordAsBytes, oldData.PasswordSalt, Settings.Default.AESIterations, Settings.Default.AESKeyIterations);

                Assert.IsNotNull(model);

                SecureSerialization.SerializeObjectToFile(ModelPath, model, newData.EnteredPasswordAsBytes, newData.PasswordSalt, Settings.Default.AESIterations, Settings.Default.AESKeyIterations);
            }
        }

        public static void SaveModel()
        {
            Assert.IsNotNull(CurrentModel);
            Assert.IsNotNull(authenticationResult);
            Assert.IsTrue(authenticationResult.Success);

            SecureSerialization.SerializeObjectToFile(ModelPath, CurrentModel, authenticationResult.EnteredPasswordAsBytes, authenticationResult.PasswordSalt, Settings.Default.AESIterations, Settings.Default.AESKeyIterations);
        }
    }
}