﻿//  Copyright 2015 Simon Collier
//    
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Windows;
using Microsoft.Win32;
using DCSBackupTool.Model;
using DCSBackupTool.ViewModel;

namespace DCSBackupTool
{
    public partial class Settings : Window
    {
        #region fields
        private Toolbox myViewModel;
        private RegistryKey baseRegistryKey = Registry.CurrentUser;
        private string usersBackupPath;
        private string usersSavedGames;
        private string usersDCSworldPath;
        private string usersHeliosPath;
        private string usersJsgmePath;
        #endregion

        #region constructor
        public Settings()
        {
            InitializeComponent();
            myViewModel = new Toolbox();
            myViewModel.PopulateSettings();
            this.DataContext = myViewModel;
        }
        #endregion

        //may split this out further
        #region buttons
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
            RegistryManipulator.WriteRegistry(baseRegistryKey, MainSettings.dCSBackupToolSubKey, "BackupPath", this.usersBackupPath);
            RegistryManipulator.WriteRegistry(baseRegistryKey, MainSettings.dCSBackupToolSubKey, "SavedGames", this.usersSavedGames);
            RegistryManipulator.WriteRegistry(baseRegistryKey, MainSettings.dCSBackupToolSubKey, "DCS World", this.usersDCSworldPath);
            RegistryManipulator.WriteRegistry(baseRegistryKey, MainSettings.dCSBackupToolSubKey, "Helios", this.usersHeliosPath);
            RegistryManipulator.WriteRegistry(baseRegistryKey, MainSettings.dCSBackupToolSubKey, "Jsgme", this.usersJsgmePath);
            this.Close();
        }

        private void CloseSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Helper methods
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
        #endregion
    }
}
