using FileSystemDataMonitoring.UI.Operations.Services;
using FileSystemDataMonitoring.UI.ViewModels;
using FileSystemDataMonitoring.UI.Views;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace FileSystemDataMonitoring.UI.Operations.Commands
{
    public class ScheduleSourceFolderScan : ICommand
    {
        #region Private Fields

        private MainWindowViewModel viewModel;
        private Timer scanTimer;

        #endregion Private Fields

        #region Constructors

        public ScheduleSourceFolderScan(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        #endregion Constructors

        #region ICommand Implementation
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.SetUpScheduledScan(Settings.SettingsManager.DirectoryScanInterval);
        }

        #endregion ICommand Implementation

        #region Utilities

        private void SetUpScheduledScan(int interval)
        {
            this.scanTimer = new Timer(interval);
            scanTimer.Elapsed += scanTimer_Elapsed;
            scanTimer.Start();            
        }

        void scanTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            scanTimer.Stop();
            this.viewModel.FolderScanCancellationToken = new System.Threading.CancellationTokenSource();
            Task.Factory.StartNew(()=>DirectoryCrawler.Crawl(Settings.SettingsManager.DirectoryToScanPath, this.viewModel), this.viewModel.FolderScanCancellationToken.Token);
            scanTimer.Start();
        }

        #endregion Utilities
    }
}
