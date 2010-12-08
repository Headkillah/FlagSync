﻿using System;
using System.Windows;
using System.Windows.Controls;
using FlagSync.Core;

namespace FlagSync.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.progressBar.Maximum = 1;
            this.progressBar.Value = 0;
        }

        /// <summary>
        /// Handles the Click event of the exitMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Handles the Click event of the newJobButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void newJobButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainViewModel.JobSettingsViewModel.AddNewJobSetting();
        }

        /// <summary>
        /// Handles the Checked event of the jobSettingsBackupModeRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void jobSettingsBackupModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.mainViewModel.JobSettingsViewModel.SelectedJobSetting.SyncMode = SyncMode.Backup;
        }

        /// <summary>
        /// Handles the Checked event of the jobSettingsSyncModeRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void jobSettingsSyncModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.mainViewModel.JobSettingsViewModel.SelectedJobSetting.SyncMode = SyncMode.Synchronization;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the jobListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void jobListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.mainViewModel.JobSettingsViewModel.SelectedJobSetting == null)
                return;

            switch (this.mainViewModel.JobSettingsViewModel.SelectedJobSetting.SyncMode)
            {
                case SyncMode.Backup:
                    this.jobSettingsBackupModeRadioButton.IsChecked = true;
                    break;

                case SyncMode.Synchronization:
                    this.jobSettingsSyncModeRadioButton.IsChecked = true;
                    break;
            }
        }

        /// <summary>
        /// Handles the Click event of the deleteJobButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void deleteJobButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainViewModel.JobSettingsViewModel.DeleteSelectedJobSetting();
        }

        /// <summary>
        /// Handles the Click event of the previewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainViewModel.JobWorkerViewModel.ResetJobWorker();

            this.mainViewModel.JobWorkerViewModel.StartJobWorker(this.mainViewModel.JobSettingsViewModel.InternJobSettings, true);
        }

        /// <summary>
        /// Handles the Click event of the loadJobsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void loadJobsButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.Filter = "|*.xml";
                dialog.InitialDirectory = this.mainViewModel.AppDataFolderPath;
                dialog.Multiselect = false;
                dialog.ShowDialog();
                this.mainViewModel.JobSettingsViewModel.LoadJobSettings(dialog.FileName);
            }
        }

        /// <summary>
        /// Handles the Click event of the saveJobsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void saveJobsButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = ".xml";
                dialog.FileName = "NewJobSettings";
                dialog.InitialDirectory = this.mainViewModel.AppDataFolderPath;
                dialog.ShowDialog();
                this.mainViewModel.JobSettingsViewModel.SaveJobSettings(dialog.FileName);
            }
        }

        /// <summary>
        /// Handles the Click event of the directoryAButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void directoryAButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainViewModel.JobSettingsViewModel.SelectedJobSetting.DirectoryA = this.ShowFolderDialog();
        }

        /// <summary>
        /// Handles the Click event of the directoryBButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void directoryBButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainViewModel.JobSettingsViewModel.SelectedJobSetting.DirectoryB = this.ShowFolderDialog();
        }

        /// <summary>
        /// Shows a folder dialog.
        /// </summary>
        /// <returns>The selected folder</returns>
        private string ShowFolderDialog()
        {
            string selectedFolder = String.Empty;

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                selectedFolder = dialog.SelectedPath;
            }

            return selectedFolder;
        }
    }
}