using FileSystemDataMonitoring.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileSystemDataMonitoring.UI.Operations.Commands
{
    public class SaveSettings : ICommand
    {
        #region Private Fields

        private SettingsViewModel viewModel;
        
        #endregion Private Fields

        #region Constructors

        public SaveSettings(SettingsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        #endregion Constructors

        #region ICommand Implementation

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {            
            Settings.SettingsManager.DirectoryScanInterval = this.viewModel.DirectoryScanInterval;
            Settings.SettingsManager.DirectoryToScanPath = this.viewModel.DirectoryToScanPath;
            Settings.SettingsManager.ProcessorsCodeDirectory = this.viewModel.ProcessorsCodeDirectory;
            Settings.SettingsManager.ProcessorsToUse = this.viewModel.ProcessorsToUse.Where(i => i.IsEnabled).Select(i => i.Extension);

            this.viewModel.RaiseViewShouldBeClosed();
        }

        #endregion ICommand Implementation
    }
}
