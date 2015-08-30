using FileSystemDataMonitoring.Contracts.DataContracts;
using FileSystemDataMonitoring.UI.Operations.Services;
using FileSystemDataMonitoring.UI.Settings;
using FileSystemDataMonitoring.UI.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace FileSystemDataMonitoring.UI.Views.Services
{
    public class ViewService
    {        
        #region Public Methods

        public static IEnumerable<IDataItem> GetDataItemsByFileName(string fileName)
        {
            return DirectoryCrawler.ProcessedData.ContainsKey(fileName) ?
                DirectoryCrawler.ProcessedData[fileName] : new List<IDataItem>();
        }

        public static SettingsViewModel ShowSettingsWindow(Window owner)
        {
            SettingsManager settingsManagerView = new SettingsManager();
            SettingsViewModel settingsViewModel = new SettingsViewModel();
            settingsManagerView.DataContext = settingsViewModel;
            settingsManagerView.Owner = owner;
            settingsManagerView.ShowDialog();

            return settingsViewModel;
        }        

        #endregion Public Methods
    }
}
