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
        private string dCSBackupToolSubKey = "SOFTWARE\\DCSBackupTool\\Settings";
        private RegistryKey baseRegistryKey = Registry.CurrentUser;

        public void BackupDCS()
        {
            Thread trd = new Thread(new ThreadStart(ThreadBackupFolders));
            trd.IsBackground = true;
            trd.Start();
        }
        private void ThreadBackupFolders()
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
                        this.dCSBackupToolSubKey, "BackupPath");

                if (!Directory.Exists(backupPath))
                {
                    SetProgressBar(false);
                    throw new ApplicationException("Backup location " + backupPath + " does not exist");
                }

                List<string> foldersToBackup = new List<string>();
                foldersToBackup.Add(RegistryManipulator.ReadRegistry(baseRegistryKey, dCSBackupToolSubKey, "SavedGames"));
                foldersToBackup.Add(RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.dCSBackupToolSubKey, "DCS World"));
                foldersToBackup.Add(RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.dCSBackupToolSubKey, "Helios"));
                foldersToBackup.Add(RegistryManipulator.ReadRegistry(this.baseRegistryKey, this.dCSBackupToolSubKey, "Jsgme"));

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
        // Declare my event using EventHandler<T> 
        public event EventHandler<CustomStringEventArgs> RaiseCopyEvent;
        //event is raised to send text back to UI
        protected virtual void OnRaiseCopyEvent(CustomStringEventArgs e)
        {
            EventHandler<CustomStringEventArgs> handler = RaiseCopyEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // Declare my event using EventHandler<T> 
        public event EventHandler<CustomBoolEventArgs> RaiseProgressEvent;
        //event is raised to send text back to UI
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
