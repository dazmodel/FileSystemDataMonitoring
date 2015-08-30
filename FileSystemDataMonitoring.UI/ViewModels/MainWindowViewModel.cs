using FileSystemDataMonitoring.UI.Operations.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileSystemDataMonitoring.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields

        private int filesFoundCount;
        private ObservableCollection<string> filesFoundList;
        private int currentFileItemsCount;
        private int totalProcessedItemsCount;
        
        #endregion Private Fields

        #region Constructors

        public MainWindowViewModel()
        {
            this.Initialize();
        }

        #endregion Constructors

        #region Commands

        public ICommand ScheduleFolderScan { get; set; }

        #endregion Commands        

        #region Data Properties

        public int FilesFoundCount 
        {
            get { return this.filesFoundCount; }
            set
            {                
                this.filesFoundCount = value;
                base.RaisePropertyChangedEvent("FilesFoundCount");
            }
        }

        public ObservableCollection<string> FilesFoundList
        {
            get { return this.filesFoundList; }
            set
            {                
                this.filesFoundList = value;
                base.RaisePropertyChangedEvent("FilesFoundList");
            }
        }

        public int CurrentFileItemsCount
        {
            get { return this.currentFileItemsCount; }
            set
            {
                this.currentFileItemsCount = value;
                base.RaisePropertyChangedEvent("CurrentFileItemsCount");
            }
        }

        public int TotalProcessedItemsCount
        {
            get { return this.totalProcessedItemsCount; }
            set
            {
                this.totalProcessedItemsCount = value;
                base.RaisePropertyChangedEvent("TotalProcessedItemsCount");
            }
        }

        #endregion Data Properties

        #region Cancellation Token Sources

        internal CancellationTokenSource FolderScanCancellationToken { get; set; }

        internal CancellationTokenSource ProcessNewFilesCancellationToken { get; set; }

        internal CancellationTokenSource ProcessFileCancellationToken { get; set; }

        #endregion Cancellation Token Sources

        #region Private Methods

        private void Initialize()
        {
            this.ScheduleFolderScan = new ScheduleSourceFolderScan(this);
            this.filesFoundList = new ObservableCollection<string>();
        }

        #endregion Private Methods

        #region Internal Methods

        internal void UpdateFilesCount()
        {
            this.FilesFoundCount++;
        }

        internal void UpdateTotalProcessedItemsCount(int processedItemsCount)
        {
            this.TotalProcessedItemsCount += processedItemsCount;
        }

        internal void UpdateFilesFoundList(string newFileName)
        {
            this.FilesFoundList.Add(newFileName);
        }        

        #endregion Internal Methods
    }
}
