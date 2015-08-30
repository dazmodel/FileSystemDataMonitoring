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
    public class TxtFileProcessor : SeparatedContentProcessor, IFileDataProcessor
    {
        #region Constants

        private const char CONTENT_COLUMNS_SEPARATOR = ';';
        private const int NUMBER_OF_NON_CONTENT_ROWS = 1;

        #endregion Constants

        #region Constructors

        public TxtFileProcessor()            
        {
            this.separator = CONTENT_COLUMNS_SEPARATOR;
            this.rowsBeforeContent = NUMBER_OF_NON_CONTENT_ROWS;
        }

        #endregion Constructors

        #region IFileDataProcessor Implementation

        public string DisplayName { get { return "Text Files Data Processor"; } }

        public string TargetFileExtension { get { return "TXT"; } }
        
        public IEnumerable<IDataItem> ProcessFile(string absoluteFileName)
        {
            return ProcessFileInternal(absoluteFileName);
        }

        #endregion IFileDataProcessor Implementation
    }
}
