using FileSystemDataMonitoring.Contracts.DataContracts;
using FileSystemDataMonitoring.Contracts.DataProcessorContracts;
using FileSystemDataMonitoring.UI.DataProcessors;
using FileSystemDataMonitoring.UI.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FileSystemDataMonitoring.UI.Operations.Services
{
    public static class DirectoryCrawler
    {
        #region Private Members

        private static ConcurrentDictionary<string, IEnumerable<IDataItem>> alreadyProcessedData = new ConcurrentDictionary<string, IEnumerable<IDataItem>>();

        #endregion Private Members

        public static ConcurrentDictionary<string, IEnumerable<IDataItem>> ProcessedData
        {
            get { return alreadyProcessedData; }
        }

        #region Public Methods

        public static void Crawl(string directoryAbsolutePath, MainWindowViewModel viewModel)
        {
            if(Directory.Exists(directoryAbsolutePath))
            {
                DirectoryInfo di = new DirectoryInfo(directoryAbsolutePath);
                IEnumerable<FileInfo> allFiles = di.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);
                ProcessFoundedFiles(allFiles, viewModel);                
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static void ProcessFoundedFiles(IEnumerable<FileInfo> fileList, MainWindowViewModel viewModel)
        {
            Task<IEnumerable<FileInfo>> getOnlyNewFiles = Task<IEnumerable<FileInfo>>.Factory.StartNew(() => GetOnlyNewFiles(fileList));
            viewModel.ProcessFileCancellationToken = new System.Threading.CancellationTokenSource();

            var processFilesList = getOnlyNewFiles.ContinueWith((t) => ProcessFileList(t.Result, viewModel));            
        }

        private static IEnumerable<FileInfo> GetOnlyNewFiles(IEnumerable<FileInfo> fileList)
        {
            return from fi in fileList
                   where !alreadyProcessedData.ContainsKey(fi.Name)
                   select fi;
        }

        private static void ProcessFileList(IEnumerable<FileInfo> filesList, MainWindowViewModel viewModel)
        {
            // Get a cancellation token
            ParallelOptions loopOptions = new ParallelOptions();
            loopOptions.CancellationToken = viewModel.ProcessFileCancellationToken.Token;

            /* If the user cancels this task while in progress, the cancellation token passed
             * in will cause an OperationCanceledException to be thrown. We trap the exception
             * and set the Progress dialog view model to display a cancellation message. */

            // Process work items in parallel
            try
            {
                Parallel.ForEach(filesList, loopOptions, t => ProcessFile(t, viewModel));
            }
            catch (OperationCanceledException)
            {
                //var ShowCancellationMessage = new Action(viewModel.ShowCancellationMessage);
                //Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, ShowCancellationMessage);
            }            
        }

        private static void ProcessFile(FileInfo fi, MainWindowViewModel viewModel)
        {
            IFileDataProcessor dataProcessor = FileProcessor.GetDataProcessor(fi.FullName);
            if (dataProcessor != null)
            {
                IEnumerable<IDataItem> processedItems = dataProcessor.ProcessFile(fi.FullName);
                if (alreadyProcessedData.TryAdd(fi.Name, processedItems))
                {
                    UpdateViewModel(viewModel, fi.Name, processedItems.Count());
                }
            }
        }

        private static void UpdateViewModel(MainWindowViewModel viewModel, string fileName, int processedItemsCount)
        {
            Application.Current.Dispatcher.Invoke(() =>
                               viewModel.UpdateFilesFoundList(fileName), DispatcherPriority.Normal);
            Application.Current.Dispatcher.Invoke(() =>
                viewModel.UpdateFilesCount(), DispatcherPriority.Normal);
            Application.Current.Dispatcher.Invoke(() =>
                viewModel.UpdateTotalProcessedItemsCount(processedItemsCount), DispatcherPriority.Normal);
        }

        #endregion Private Methods
    }
}
