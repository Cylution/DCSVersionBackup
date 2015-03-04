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
