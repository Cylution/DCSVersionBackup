using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCSBackupTool.Model
{
        // A class to hold custom event info that im passing back from the publisher
        class CustomStringEventArgs : EventArgs
        {
            private string message;

            public CustomStringEventArgs(string s)
            {
                this.message = s;
            }

            public string Message
            {
                get { return this.message; }
                set { this.message = value; }
            }
        }
}
