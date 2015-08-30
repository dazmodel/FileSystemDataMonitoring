using FileSystemDataMonitoring.Contracts.DataContracts;
using FileSystemDataMonitoring.Contracts.DataProcessorContracts;
using FileSystemDataMonitoring.SimpleDataProcessors.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDataMonitoring.SimpleDataProcessors.SeparatedValuesProcessors
{
    [Export(typeof(IFileDataProcessor))]
    public class CsvFileProcessor : SeparatedContentProcessor, IFileDataProcessor
    {
        #region Constants

        private const char CSV_DEFAULT_SEPARATOR = ',';
        private const int NUMBER_OF_NON_CONTENT_ROWS = 0;

        #endregion Constants

        #region Constructors
        public CsvFileProcessor()            
        {
            this.separator = CSV_DEFAULT_SEPARATOR;
            this.rowsBeforeContent = NUMBER_OF_NON_CONTENT_ROWS;
        }

        #endregion Constructors

        #region IFileDataProcessor Implementation

        public string DisplayName { get { return "CSV Files Data Processor"; } }

        public string TargetFileExtension { get { return "CSV"; } }

        public IEnumerable<IDataItem> ProcessFile(string absoluteFileName)
        {
            return this.ProcessFileInternal(absoluteFileName);
        }

        #endregion IFileDataProcessor Implementation
    }
}
