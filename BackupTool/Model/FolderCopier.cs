using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCSBackupTool.Model
{
    class FolderCopier
    {



        #region Raise our events when required
        public void SetCopyText(string text)
        {
            OnRaiseCopyEvent(new CustomStringEventArgs(text));
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
        #endregion

    }
}
