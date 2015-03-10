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
