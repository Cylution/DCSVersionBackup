using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DCSBackupTool.Model
{
    class MainSettings
    {
        private RegistryKey baseRegistryKey = Registry.CurrentUser;
        private string dCSBackupToolSubKey = "SOFTWARE\\DCSBackupTool\\Settings";
        private string eDPathSubKey = "SOFTWARE\\Eagle Dynamics\\DCS World";
        private string usersBackupPath;
        private string usersHomePath;
        private string usersSavedGames;
        private string usersDCSworldPath;
        private string usersHeliosPath;
        private string usersJsgmePath;

        public MainSettings()
        {
            GetSettingsValues();
        }

        private void GetSettingsValues()
        {
            //get backup location
            this.usersBackupPath = RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.dCSBackupToolSubKey, "BackupPath");
            if (this.usersBackupPath == null)
            {
                BackupLocationText("Select a backup location");
            }
            else
            {
                BackupLocationText(this.usersBackupPath);
            }

            //get saved games
            this.usersSavedGames = RegistryManipulator.ReadRegistry(baseRegistryKey, dCSBackupToolSubKey, "SavedGames");
            if (this.usersSavedGames == null)
            {
                this.usersHomePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string dcsSavedGames = this.usersHomePath + "\\Saved Games\\DCS";
                this.usersSavedGames = dcsSavedGames;
                SavedGamesText(dcsSavedGames);
            }
            else
            {
                SavedGamesText(this.usersSavedGames);
            }

            //get dcsWorld location my setting first if in registry
            this.usersDCSworldPath = RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.dCSBackupToolSubKey, "DCS World");
            if (this.usersDCSworldPath == null)
            {
                //get eagle dynamics setting
                this.usersDCSworldPath = RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.eDPathSubKey, "Path");
            }
            if (this.usersDCSworldPath != null)
            {
                DCSWorldText(this.usersDCSworldPath);
            }
            else
            {
                DCSWorldText("Can not find DCS. Enter path to DCS");
            }

            //get helios path
            this.usersHeliosPath = RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.dCSBackupToolSubKey, "Helios");
            if (this.usersHeliosPath == null)
            {
                //usual helios path
                HeliosText("If installed choose location");
                HeliosText("Usual path is " + this.usersHomePath + "\\Documents\\Helios");
            }
            else
            { 
                HeliosText(this.usersHeliosPath);
            }
            //get jsgme path
            this.usersJsgmePath = RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.dCSBackupToolSubKey, "Jsgme");
            if (this.usersJsgmePath == null)
            {
                JsgmeText("Select path for JSGME folder if your using one");
            }
            else 
            {
                JsgmeText(this.usersJsgmePath);
            }
        }

        private void BackupLocation_Button_Click(object sender, RoutedEventArgs e)
        {
            this.usersBackupPath = GetPathFromUser();
            if(this.usersBackupPath != "")
            { 
                BackupLocationText.Text = this.usersBackupPath;
            }
        }

        private void Helios_Click(object sender, RoutedEventArgs e)
        {
            this.usersHeliosPath = GetPathFromUser();
            if (this.usersHeliosPath != "")
            {
                HeliosText.Text = this.usersHeliosPath;
            }
        }

        private string GetPathFromUser()
        {
            string folderPath = "";
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result.ToString() == "OK")
            {
                folderPath = dialog.SelectedPath;
            }
            return folderPath;
        }

        private void SavedGames_Click(object sender, RoutedEventArgs e)
        {
            this.usersSavedGames = GetPathFromUser();
            if (this.usersSavedGames != "")
            {
                savedGamesText.Text = this.usersSavedGames;
            }
        }

        private void DCSWorld_Click(object sender, RoutedEventArgs e)
        {
            this.usersDCSworldPath = GetPathFromUser();
            if (this.usersDCSworldPath != "")
            {
                DCSWorldText.Text = this.usersDCSworldPath;
            }
        }

        private void Jsgme_Click(object sender, RoutedEventArgs e)
        {
            this.usersJsgmePath = GetPathFromUser();
            if (this.usersJsgmePath != "")
            {
                JsgmeText.Text = this.usersJsgmePath;
            }
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            RegistryManipulator.WriteRegistry(baseRegistryKey, dCSBackupToolSubKey, "BackupPath", this.usersBackupPath);
            RegistryManipulator.WriteRegistry(baseRegistryKey, dCSBackupToolSubKey, "SavedGames", this.usersSavedGames);
            RegistryManipulator.WriteRegistry(baseRegistryKey, dCSBackupToolSubKey, "DCS World", this.usersDCSworldPath);
            RegistryManipulator.WriteRegistry(baseRegistryKey, dCSBackupToolSubKey, "Helios", this.usersHeliosPath);
            RegistryManipulator.WriteRegistry(baseRegistryKey, dCSBackupToolSubKey, "Jsgme", this.usersJsgmePath);
            this.Close();
        }


        #region Raise our events when required
        public void SetSettingsText(string text)
        {
            OnRaiseSettingsEvent(new CustomStringEventArgs(text));
        }

        public void BackupLocationText(string text)
        {
            OnRaiseSettingsEvent(new CustomStringEventArgs(text));
        }

        public void SavedGamesText(string text)
        {
            OnRaiseSettingsEvent(new CustomStringEventArgs(text));
        }

        public void DCSWorldText(string text)
        {
            OnRaiseSettingsEvent(new CustomStringEventArgs(text));
        }

        public void HeliosText(string text)
        {
            OnRaiseSettingsEvent(new CustomStringEventArgs(text));
        }

        public void JsgmeText(string text)
        {
            OnRaiseSettingsEvent(new CustomStringEventArgs(text));
        }


        #endregion

        #region events to publish
        // Declare my event using EventHandler<T> 
        public event EventHandler<CustomStringEventArgs> RaiseSettingsEvent;
        //event is raised to send text back to UI
        protected virtual void OnRaiseSettingsEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseSettingsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion


    }
}
