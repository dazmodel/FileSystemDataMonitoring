using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDataMonitoring.UI.ViewModels
{
    public class DataProcessorViewModel : ViewModelBase
    {
        #region Private Fields

        private string name;
        private bool isEnabled;
        private string extension;

        #endregion Private Fields

        #region Public Properties

        public string Name 
        {
            get { return this.name; }
            set
            {
                this.name = value;
                base.RaisePropertyChangedEvent("Name");
            }
        }
        public bool IsEnabled 
        {
            get { return this.isEnabled; }
            set
            {
                this.isEnabled = value;
                base.RaisePropertyChangedEvent("IsEnabled");
            }
        }
        public string Extension
        {
            get { return this.extension; }
            set
            {
                this.extension = value;
                base.RaisePropertyChangedEvent("Extension");
            }
        }

        #endregion Public Properties
    }
}
