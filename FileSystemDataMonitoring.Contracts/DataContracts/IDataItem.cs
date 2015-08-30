using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDataMonitoring.Contracts.DataContracts
{
    public interface IDataItem
    {
        string Date { get; set; }
        string Open { get; set; }
        string High { get; set; }
        string Low { get; set; }
        string Close { get; set; }
        string Volume { get; set; }
    }
}
