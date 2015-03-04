using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DCSBackupTool.Model;

namespace DCSBackupTool.ViewModel
{
    class Toolbox : INotifyPropertyChanged
    {
        #region fields
        private string text = "Choose options above";
        private bool progress = false;
        #endregion

        #region properties
        public string CopyOutText
        {
            get { return this.text; }
            set
            {
                if (value == this.text)
                    return;
                this.text = value;
                OnPropertyChanged("CopyOutText");
            }
        }

        public bool Progress
        {
            get { return this.progress; }
            set
            {
                if (value == this.progress)
                    return;
                this.progress = value;
                OnPropertyChanged("Progress");
            }
        }
        #endregion

        public void Backup()
        {
            FolderCopier myCopier = new FolderCopier();
            myCopier.RaiseCopyEvent += HandleCopyTextEvent;
            myCopier.RaiseProgressEvent += HandleProgressEvent;
            myCopier.BackupDCS();
        }


        #region event handler methods
        void HandleCopyTextEvent(object sender, CustomStringEventArgs e)
        {
            CopyOutText = e.Message;
        }

        void HandleProgressEvent(object sender, CustomBoolEventArgs e)
        {
            Progress = e.BoolBack;
        }
        #endregion

        #region OnPropertyChanged for our UI bindings
        //fields and properties used declared at the top
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion



    }
}
