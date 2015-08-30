using FileSystemDataMonitoring.UI.DataProcessors;
using FileSystemDataMonitoring.UI.Operations.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileSystemDataMonitoring.UI.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Private Fields

        private int directoryScanInterval;
        private string directoryToScanPath;
        private string processorsCodeDirectory;
        private ObservableCollection<DataProcessorViewModel> processorsToUse;

        #endregion Private Fields

        #region Events

        public event EventHandler ViewShouldBeClosed;

        #endregion Events

        #region Constructors

        public SettingsViewModel()
        {
            this.Initialize();
        }

        #endregion Constructors

        #region Data Properties

        public int DirectoryScanInterval
        {
            get { return this.directoryScanInterval; }
            set
            {
                this.directoryScanInterval = value;
                base.RaisePropertyChangedEvent("DirectoryScanInterval");
            }
        }

        public string DirectoryToScanPath
        {
            get { return this.directoryToScanPath; }
            set
            {
                this.directoryToScanPath = value;
                base.RaisePropertyChangedEvent("DirectoryToScanPath");
            }
        }

        public string ProcessorsCodeDirectory
        {
            get { return this.processorsCodeDirectory; }
            set
            {
                this.processorsCodeDirectory = value;
                base.RaisePropertyChangedEvent("ProcessorsCodeDirectory");
            }
        }

        public ObservableCollection<DataProcessorViewModel> ProcessorsToUse
        {
            get { return this.processorsToUse; }
            set
            {
                this.processorsToUse = value;
                base.RaisePropertyChangedEvent("ProcessorsToUse");
            }
        }

        #endregion Data Properties

        #region Commands

        public ICommand SaveSettings { get; set; }

        #endregion Commands

        #region Private Methods

        private void Initialize()
        {
            this.DirectoryScanInterval = Settings.SettingsManager.DirectoryScanInterval;
            this.DirectoryToScanPath = Settings.SettingsManager.DirectoryToScanPath;
            this.ProcessorsCodeDirectory = Settings.SettingsManager.ProcessorsCodeDirectory;
            this.ProcessorsToUse = FileProcessor.GetDataProcessorsAvailabilityList(Settings.SettingsManager.ProcessorsToUse);
            this.SaveSettings = new SaveSettings(this);
        }

        #endregion Private Methods

        #region Internal Methods

        internal void RaiseViewShouldBeClosed()
        {
            if (this.ViewShouldBeClosed != null)
                this.ViewShouldBeClosed(this, new EventArgs());
        }

        #endregion Internal Methods
    }
}
