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
using System.IO;
using System.Threading;
using Microsoft.Win32;
using DCSBackupTool.ViewModel;

namespace DCSBackupTool.Model
{
    class FolderCopier
    {
        private RegistryKey baseRegistryKey = Registry.CurrentUser;

        public void BackupDCS()
        {
            //start time
            string backupLocations = null;
            int result = Environment.TickCount & Int32.MaxValue;
            StringBuilder textOut = new StringBuilder();
            textOut.Append("Starting Backup\n");
            SetProgressBar(true);

            try
            {
                string backupPath = RegistryManipulator.ReadRegistry(this.baseRegistryKey,
                        MainSettings.dCSBackupToolSubKey, "BackupPath");

                if (!Directory.Exists(backupPath))
                {
                    SetProgressBar(false);
                    throw new ApplicationException("Backup location " + backupPath + " not found");
                }

                List<string> foldersToBackup = new List<string>();
                foldersToBackup.Add(RegistryManipulator.ReadRegistry(baseRegistryKey, MainSettings.dCSBackupToolSubKey, "SavedGames"));
                foldersToBackup.Add(RegistryManipulator.ReadRegistry(this.baseRegistryKey, MainSettings.dCSBackupToolSubKey, "DCS World"));
                foldersToBackup.Add(RegistryManipulator.ReadRegistry(this.baseRegistryKey, MainSettings.dCSBackupToolSubKey, "Helios"));
                foldersToBackup.Add(RegistryManipulator.ReadRegistry(this.baseRegistryKey, MainSettings.dCSBackupToolSubKey, "Jsgme"));

                foreach (string fol in foldersToBackup)
                {
                    if (fol != null)
                    {
                        //check each folder exists before copying
                        if (Directory.Exists(fol))
                        {
                            //get the name of the folder from the original path
                            string dirName = new DirectoryInfo(fol).Name;
                            //append the directory name to the backuplocation
                            backupLocations = backupPath + "\\" + dirName;
                            //check for existing backup folder if so delete
                            if (Directory.Exists(backupLocations))
                            {
                                //If you have the specified directory open in File Explorer 
                                //the Delete method may not be able to delete it
                                Directory.Delete(backupLocations, true);
                                textOut.Append(backupLocations + " deleted\n");
                                SetCopyText(textOut.ToString());
                            }
                            CopyDirectory(fol, backupLocations, true);
                            textOut.Append(fol + " backed up\n");
                            SetCopyText(textOut.ToString());
                        }
                        else
                        {
                            SetProgressBar(false);
                            throw new ApplicationException(fol + " does not exist");
                        }
                    }
                }

                //finish time
                SetProgressBar(false);
                int result2 = Environment.TickCount & Int32.MaxValue;
                string timeTaken = ((result2 - result) / 1000).ToString();
                textOut.Append("Copy took " + timeTaken + " seconds");
                SetCopyText(textOut.ToString());
            }
            catch (Exception oError)
            {
                SetCopyText(oError.Message);
            }
        }

        private static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = true;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    foreach (string file in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(file);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string directory in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(directory);
                        if (CopyDirectory(directory, DestinationPath + dirInfo.Name, overwriteexisting) == false)
                        {
                            ret = false;
                        }
                    }
                }
                else
                {
                    ret = false;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        #region Raise our events when required
        public void SetCopyText(string text)
        {
            OnRaiseCopyEvent(new CustomStringEventArgs(text));
        }

        private void SetProgressBar(bool b)
        {
            OnRaiseProgressEvent(new CustomBoolEventArgs(b));
        }
        #endregion

        #region events to publish
        public event EventHandler<CustomStringEventArgs> RaiseCopyEvent;

        protected virtual void OnRaiseCopyEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseCopyEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<CustomBoolEventArgs> RaiseProgressEvent;

        protected virtual void OnRaiseProgressEvent(CustomBoolEventArgs e)
        {
            EventHandler<CustomBoolEventArgs> handler = RaiseProgressEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

    }
}
