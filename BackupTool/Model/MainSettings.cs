//  Copyright 2015 Simon Collier
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

        public void GetSettingsValues()
        {
            this.usersBackupPath = RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.dCSBackupToolSubKey, "BackupPath");
            if (this.usersBackupPath == null)
            {
                BackupLocationText("Select a backup location");
            }
            else
            {
                BackupLocationText(this.usersBackupPath);
            }

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

        #region Raise our events when required
        public void SetSettingsText(string text)
        {
            OnRaiseSetSettingsEvent(new CustomStringEventArgs(text));
        }

        public void BackupLocationText(string text)
        {
            OnRaiseBackupLocationSettingsEvent(new CustomStringEventArgs(text));
        }

        public void SavedGamesText(string text)
        {
            OnRaiseSavedGamesSettingsEvent(new CustomStringEventArgs(text));
        }

        public void DCSWorldText(string text)
        {
            OnRaiseDCSWorldSettingsEvent(new CustomStringEventArgs(text));
        }

        public void HeliosText(string text)
        {
            OnRaiseHeliosSettingsEvent(new CustomStringEventArgs(text));
        }

        public void JsgmeText(string text)
        {
            OnRaiseJsgmeSettingsEvent(new CustomStringEventArgs(text));
        }


        #endregion

        #region events to publish
        public event EventHandler<CustomStringEventArgs> RaiseSettingsEvent;
        protected virtual void OnRaiseSettingsEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseSettingsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CustomStringEventArgs> RaiseJsgmeSettingsEvent;
        protected virtual void OnRaiseJsgmeSettingsEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseJsgmeSettingsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CustomStringEventArgs> RaiseSetSettingsEvent;
        protected virtual void OnRaiseSetSettingsEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseSetSettingsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CustomStringEventArgs> RaiseBackupLocationSettingsEvent;
        protected virtual void OnRaiseBackupLocationSettingsEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseBackupLocationSettingsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CustomStringEventArgs> RaiseSavedGamesSettingsEvent;
        protected virtual void OnRaiseSavedGamesSettingsEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseSavedGamesSettingsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CustomStringEventArgs> RaiseDCSWorldSettingsEvent;
        protected virtual void OnRaiseDCSWorldSettingsEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseDCSWorldSettingsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CustomStringEventArgs> RaiseHeliosSettingsEvent;
        protected virtual void OnRaiseHeliosSettingsEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseHeliosSettingsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion


    }
}
