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
using Microsoft.Win32;

namespace DCSBackupTool.Model
{
    class RegistryManipulator
    {
        public static bool WriteRegistry(RegistryKey baseRegistryKey, string subKey,
                string KeyName, object Value)
        {
            try
            {
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                sk1.SetValue(KeyName.ToUpper(), Value);   // Save the value
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ReadRegistry(RegistryKey baseRegistryKey, string subKey, string KeyName)
        {  
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
