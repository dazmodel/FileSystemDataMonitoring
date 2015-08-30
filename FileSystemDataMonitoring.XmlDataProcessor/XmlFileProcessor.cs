using FileSystemDataMonitoring.Contracts.DataContracts;
using FileSystemDataMonitoring.Contracts.DataProcessorContracts;
using FileSystemDataMonitoring.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileSystemDataMonitoring.XmlDataProcessor
{
    [Export(typeof(IFileDataProcessor))]
    public class XmlFileProcessor : IFileDataProcessor
    {
        #region IFileDataProcessor Implementation

        public string DisplayName { get { return "XML data file processor"; } }

        public string TargetFileExtension { get { return "XML"; } }

        public IEnumerable<IDataItem> ProcessFile(string absoluteFileName)
        {
            try
            {
                return this.ProcessFileInternal(absoluteFileName);
            }
            catch { return new List<IDataItem>(); }
        }        

        #endregion IFileDataProcessor Implementation

        #region Utilities

        private IEnumerable<IDataItem> ProcessFileInternal(string absoluteFileName)
        {
            XDocument xmlFile = XDocument.Load(absoluteFileName);
            return from valueElement in xmlFile.Descendants("value")
                   select new DataItem()
                   {
                       Date = (string)valueElement.Attribute("date"),
                       Open = (string)valueElement.Attribute("open"),
                       High = (string)valueElement.Attribute("high"),
                       Low = (string)valueElement.Attribute("low"),
                       Close = (string)valueElement.Attribute("close"),
                       Volume = (string)valueElement.Attribute("volume")
                   };
        }

        #endregion Utilities        
    }
}
