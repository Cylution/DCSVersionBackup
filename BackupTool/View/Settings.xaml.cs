using System;
using System.Windows;
using Microsoft.Win32;
using DCSBackupTool.Model;
using DCSBackupTool.ViewModel;

namespace DCSBackupTool
{
    public partial class Settings : Window
    {
        private Toolbox myViewModel;
        private RegistryKey baseRegistryKey = Registry.CurrentUser;
        private string dCSBackupToolSubKey = "SOFTWARE\\DCSBackupTool\\Settings";
        private string eDPathSubKey = "SOFTWARE\\Eagle Dynamics\\DCS World";
        private string usersBackupPath;
        private string usersHomePath;
        private string usersSavedGames;
        private string usersDCSworldPath;
        private string usersHeliosPath;
        private string usersJsgmePath;

        public Settings()
        {
            InitializeComponent();
            myViewModel = new Toolbox();
            myViewModel.PopulateSettings();
            this.DataContext = myViewModel;
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

        private void CloseSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
