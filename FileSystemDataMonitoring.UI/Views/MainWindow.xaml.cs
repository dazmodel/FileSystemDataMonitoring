using FileSystemDataMonitoring.Contracts.DataContracts;
using FileSystemDataMonitoring.UI.ViewModels;
using FileSystemDataMonitoring.UI.Views.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FileSystemDataMonitoring.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors       

        #region Main Window View Event Handlers

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)e.NewValue;
            viewModel.ScheduleFolderScan.Execute(new object());
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<IDataItem> dataItems = ViewService.GetDataItemsByFileName(e.AddedItems[0].ToString());
            this.DataGrid.DataContext = dataItems;
            (this.DataContext as MainWindowViewModel).CurrentFileItemsCount = dataItems.Count();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewService.ShowSettingsWindow(this);
        }        

        #endregion Main Window View Event Handlers         
    }
}
