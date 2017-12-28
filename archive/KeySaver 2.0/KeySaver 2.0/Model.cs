using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace KeySaver
{
    [Serializable]
    internal sealed class Model : ISerializable, INotifyPropertyChanged
    {
        private static string filePath;

        private string currentFilter;

        static Model()
        {
            filePath = Application.StartupPath + "\\data";
        }

        public Model()
        {
            this.Keys = new List<Entry>();
            this.Mails = new List<CryptedMailAdress>();
            this.CurrentFilter = string.Empty;
        }

        private Model(SerializationInfo info, StreamingContext context)
        {
            this.Keys = info.GetValue("Keys", typeof(List<Entry>)) as List<Entry>;
            this.Mails = info.GetValue("Mails", typeof(List<CryptedMailAdress>)) as List<CryptedMailAdress>;
            this.CurrentFilter = string.Empty;
        }

        public static event EventHandler ModelLoaded;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        public static Model CurrentModel
        {
            get;
            private set;
        }

        public string CurrentFilter
        {
            get
            {
                return currentFilter.ToUpper();
            }
            set
            {
                if (this.currentFilter != value)
                {
                    this.currentFilter = value;
                    this.RaisePropertyChanged("CurrentFilter");
                }
            }
        }

        public List<Entry> Keys
        {
            get;
            private set;
        }

        public List<CryptedMailAdress> Mails
        {
            get;
            private set;
        }

        internal static string Key
        {
            get;
            set;
        }

        public static void Load()
        {
            if (File.Exists(filePath))
            {
                CurrentModel = CurrentModel.DezerializeFile(filePath);
            }

            if (CurrentModel == default(Model))
            {
                CurrentModel = new Model();
            }

            RaiseModelLoaded();
        }

        public static void Save()
        {
            if (!CurrentModel.SerializeStream(filePath))
            {
                MessageBox.Show("Saving failed!");
            }
        }

        public Entry Add(string name, string content)
        {
            var entry = new Entry();

            entry.Name = name;
            entry.Content = content;

            this.Keys.Insert(0, entry);

            return entry;
        }

        public void AddMail(string mail)
        {
            this.Mails.Add(new CryptedMailAdress(mail));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Keys", this.Keys);
            info.AddValue("Mails", this.Mails);
        }

        public void RaiseSelectionChangedEvent()
        {
            this.RaiseSelectionChangedEvent(default(Entry));
        }

        public void RaiseSelectionChangedEvent(Entry entry)
        {
            var handler = this.SelectionChanged;

            if (handler != default(EventHandler<SelectionChangedEventArgs>))
            {
                handler(this, new SelectionChangedEventArgs(entry));
            }
        }

        public void Remove(Entry entry)
        {
            this.Keys.Remove(entry);
        }

        public void RemoveMail(string mail)
        {
            for (int i = 0; i < this.Mails.Count; i++)
            {
                if (this.Mails[i].Mail == mail)
                {
                    this.Mails.RemoveAt(i--);
                }
            }
        }

        private static void RaiseModelLoaded()
        {
            var handler = ModelLoaded;
            if (handler != default(EventHandler))
            {
                handler(Model.CurrentModel, new EventArgs());
            }
        }

        private void RaisePropertyChanged(string name)
        {
            var handler = PropertyChanged;

            if (handler != default(PropertyChangedEventHandler))
            {
                handler(Model.CurrentModel, new PropertyChangedEventArgs(name));
            }
        }

        //public void SetContent(int id, string content)
        //{
        //    var tmp = this.Keys.Where(s => s.Id == id).SingleOrDefault();

        //    if (tmp != default(Entry) && tmp.Content != content)
        //    {
        //        tmp.Content = content;

        //        //this.RaiseModelChangedEvent();
        //    }
        //}
        //public void Rename(int id, string name)
        //{
        //    var tmp = this.Keys.Where(s => s.Id == id).SingleOrDefault();

        //    if (tmp != default(Entry) && tmp.Name != name)
        //    {
        //        tmp.Name = name;
    }
}