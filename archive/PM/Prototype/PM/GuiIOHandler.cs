using KeyManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PM.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PM
{
    public static class GuiIOHandler
    {
        public static string ModelPath = string.Format("{0}\\{1}", Application.StartupPath, Settings.Default.ModelFileName);

        public static IModel CurrentModel
        {
            get;
            private set;
        }

        public static bool TryLoadModel(string password)
        {
            try
            {
                CurrentModel = IOHandler.LoadModelFromFile(ModelPath, password);
                return true;
            }
            catch (WrongPasswordException)
            {
                MessageBox.Show("Wrong password!");
            }
            catch (Exception)
            {
                MessageBox.Show("Model couldn't be loaded!");
            }

            return false;
        }

        public static void SaveCurrentModel()
        {
            Assert.IsFalse(CurrentModel == default(IModel));
        }

        public static void CreateModelFile(string password)
        {
            IOHandler.CreateModelFile(ModelPath, password);
        }
    }
}
