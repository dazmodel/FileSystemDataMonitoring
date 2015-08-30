using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDataMonitoring.UI.Settings
{
    public static class SettingsManager
    {
        public static int DirectoryScanInterval
        {
            get
            {
                return Properties.Settings.Default.DirectoryScanInterval;                
            }
            set 
            {
                Properties.Settings.Default.DirectoryScanInterval = value;                 
            }
        }

        public static string DirectoryToScanPath
        {
            get { return Properties.Settings.Default.DirectoryToScanPath; }
            set 
            {
                Properties.Settings.Default.DirectoryToScanPath = value;                
            }
        }

        public static string ProcessorsCodeDirectory
        {
            get { return Properties.Settings.Default.ProcessorsCodeDirectory; } 
            set 
            {
                Properties.Settings.Default.ProcessorsCodeDirectory = value;                
            }
        }

        public static IEnumerable<string> ProcessorsToUse
        {
            get { return Properties.Settings.Default.ProcessorsToUse.Split("|".ToArray(), StringSplitOptions.RemoveEmptyEntries); }
            set 
            {
                Properties.Settings.Default.ProcessorsToUse = string.Join("|", value);               
            }
        }

        public static void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }        
    }
}
