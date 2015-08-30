using FileSystemDataMonitoring.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDataMonitoring.Domain.Models
{
    public class DataItem : IDataItem
    {
        #region IDataItem Implementation

        public string Date { get; set; }

        public string Open { get; set; }

        public string High { get; set; }

        public string Low { get; set; }

        public string Close { get; set; }

        public string Volume { get; set; }

        #endregion IDataItem Implementation
    }
}
