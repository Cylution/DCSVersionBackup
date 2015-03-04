using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCSBackupTool.Model
{
        class CustomBoolEventArgs : EventArgs
        {
            private bool boolBack;

            public CustomBoolEventArgs(bool b)
            {
                this.boolBack = b;
            }

            public bool BoolBack
            {
                get { return this.boolBack; }
                set { this.boolBack = value; }
            }
        }
}
