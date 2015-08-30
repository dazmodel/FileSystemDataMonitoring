using FileSystemDataMonitoring.Contracts;
using FileSystemDataMonitoring.Contracts.DataProcessorContracts;
using FileSystemDataMonitoring.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDataMonitoring.UI.DataProcessors
{
    public class FileProcessor
    {
        private static Dictionary<string, IFileDataProcessor> availableProcessors = new Dictionary<string, IFileDataProcessor>();
        private static IEnumerable<IFileDataProcessor> loadedProcessors;

        public static void InitializeAvailableProcessors(string processorsCodeFolder)
        {
            DataProcessorsInstancesLoader<IFileDataProcessor> processorInstancesLoader = new 
                DataProcessorsInstancesLoader<IFileDataProcessor>(Settings.SettingsManager.ProcessorsCodeDirectory);
            loadedProcessors = processorInstancesLoader.Plugins;
            foreach (IFileDataProcessor fp in loadedProcessors)
            {
                if (!availableProcessors.ContainsKey(fp.TargetFileExtension))
                {
                    availableProcessors.Add(fp.TargetFileExtension, fp);
                }
            }
        }

        public static ObservableCollection<DataProcessorViewModel> GetDataProcessorsAvailabilityList(IEnumerable<string> enabledExtensions)
        {
            ObservableCollection<DataProcessorViewModel> results = new ObservableCollection<DataProcessorViewModel>();
            loadedProcessors.ToList().ForEach(processor => results.Add(new DataProcessorViewModel()
                      {
                          Name = processor.DisplayName,
                          IsEnabled = enabledExtensions.Contains(processor.TargetFileExtension),
                          Extension = processor.TargetFileExtension
                      }));
            return results;
        }

        public static IFileDataProcessor GetDataProcessor(string absoluteFileName)
        {
            string fileExtension = Path.GetExtension(absoluteFileName).ToUpper().Trim('.');

            if (availableProcessors.ContainsKey(fileExtension) && 
                Settings.SettingsManager.ProcessorsToUse.Contains(fileExtension))
            {
                return availableProcessors[fileExtension];
            }

            return null;
        }
    }
}
