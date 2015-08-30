using FileSystemDataMonitoring.UI.DataProcessors;
using FileSystemDataMonitoring.UI.ViewModels;
using FileSystemDataMonitoring.UI.Views;
using System.Windows;

namespace FileSystemDataMonitoring.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            FileProcessor.InitializeAvailableProcessors(Settings.SettingsManager.ProcessorsCodeDirectory);
            var mainWindow = new MainWindow();
            var mainWindowViewModel = new MainWindowViewModel();            
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.SettingsManager.SaveSettings();
        }
    }
}
