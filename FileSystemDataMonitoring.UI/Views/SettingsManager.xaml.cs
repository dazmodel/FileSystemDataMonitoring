using FileSystemDataMonitoring.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FileSystemDataMonitoring.UI.Views
{
    /// <summary>
    /// Interaction logic for SettingsManager.xaml
    /// </summary>
    public partial class SettingsManager : Window
    {
        public SettingsManager()
        {
            InitializeComponent();
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (SettingsViewModel)e.NewValue;
            viewModel.ViewShouldBeClosed += viewModel_ViewShouldBeClosed;
        }

        void viewModel_ViewShouldBeClosed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
