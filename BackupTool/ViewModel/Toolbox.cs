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
        private string text;
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
        #endregion

        #region event handler methods
        void HandleChangeStateTextEvent(object sender, CustomStringEventArgs e)
        {
            //bind & update the text on the UI
            CopyOutText = e.Message;
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
