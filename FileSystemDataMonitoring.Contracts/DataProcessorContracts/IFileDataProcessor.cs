using FileSystemDataMonitoring.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDataMonitoring.Contracts.DataProcessorContracts
{
    public interface IFileDataProcessor
    {
        string DisplayName { get; }
        string TargetFileExtension { get; }
        IEnumerable<IDataItem> ProcessFile(string absoluteFileName);
    }
}
