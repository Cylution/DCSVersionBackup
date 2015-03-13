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
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using DCSBackupTool.Model;
using DCSBackupTool.ViewModel;

namespace DCSBackupTool
{
    public partial class MainWindow : Window
    {
        private Toolbox myViewModel;

        public MainWindow()
        {
            InitializeComponent();
            myViewModel = new Toolbox();
            this.DataContext = myViewModel;          
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            myViewModel.Backup();
        }
  
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            Settings mySet = new Settings();
            mySet.Show();
        }
    }
}
