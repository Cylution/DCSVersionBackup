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
        private MainSettings mySettings;
        private string backupLocationText;
        private string savedGamesLocationText;
        private string dcsWorldLocationText;
        private string heliosLocationText;
        private string jsgmeLocationText;
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

        public string BackupLocationText
        {
            get { return this.backupLocationText; }
            set
            {
                if (value == this.backupLocationText)
                    return;
                this.backupLocationText = value;
                OnPropertyChanged("BackupLocationText");
            }
        }

        public string SavedGamesLocationText
        {
            get { return this.savedGamesLocationText; }
            set
            {
                if (value == this.savedGamesLocationText)
                    return;
                this.savedGamesLocationText = value;
                OnPropertyChanged("SavedGamesLocationText");
            }
        }
        
        public string DcsWorldLocationText
        {
            get { return this.dcsWorldLocationText; }
            set
            {
                if (value == this.dcsWorldLocationText)
                    return;
                this.dcsWorldLocationText = value;
                OnPropertyChanged("DcsWorldLocationText");
            }
        }
        
        public string HeliosLocationText
        {
            get { return this.heliosLocationText; }
            set
            {
                if (value == this.heliosLocationText)
                    return;
                this.heliosLocationText = value;
                OnPropertyChanged("HeliosLocationText");
            }
        }
        
        public string JsgmeLocationText
        {
            get { return this.jsgmeLocationText; }
            set
            {
                if (value == this.jsgmeLocationText)
                    return;
                this.jsgmeLocationText = value;
                OnPropertyChanged("JsgmeLocationText");
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

        public void PopulateSettings()
        {
            mySettings = new MainSettings();
            mySettings.RaiseBackupLocationSettingsEvent += HandleBackupLocationEvent;
            mySettings.RaiseSavedGamesSettingsEvent += HandleSavedGamesLocationEvent;
            mySettings.RaiseDCSWorldSettingsEvent += HandleDcsWorldLocationEvent;
            mySettings.RaiseHeliosSettingsEvent += HandleHeliosLocationEvent;
            mySettings.RaiseJsgmeSettingsEvent += HandleJsgmeLocationEvent;
            mySettings.GetSettingsValues();
        }

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

        void HandleBackupLocationEvent(object sender, CustomStringEventArgs e)
        {
            BackupLocationText = e.Message;
        }

        void HandleSavedGamesLocationEvent(object sender, CustomStringEventArgs e)
        {
           SavedGamesLocationText = e.Message;
        }

        void HandleDcsWorldLocationEvent(object sender, CustomStringEventArgs e)
        {
            DcsWorldLocationText = e.Message;
        }

        void HandleHeliosLocationEvent(object sender, CustomStringEventArgs e)
        {
            HeliosLocationText = e.Message;
        }

        void HandleJsgmeLocationEvent(object sender, CustomStringEventArgs e)
        {
            JsgmeLocationText = e.Message;
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
