using FileSystemDataMonitoring.Contracts.DataContracts;
using FileSystemDataMonitoring.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDataMonitoring.SimpleDataProcessors.Common
{
    public abstract class SeparatedContentProcessor
    {
        #region Protected Fields

        protected char separator;        
        protected int rowsBeforeContent;

        #endregion Protected Fields
                
        #region Protected Methods

        protected IEnumerable<IDataItem> ProcessFileInternal(string absoluteFileName)
        {
            List<IDataItem> dataItems = new List<IDataItem>();

            using (StreamReader file = new StreamReader(absoluteFileName))
            {
                int counter = 0;
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    if (counter >= this.rowsBeforeContent)
                    {
                        string[] dataItemMembers = line.Split(this.separator);
                        if (dataItemMembers.Length > 0)
                        {
                            dataItems.Add(new DataItem
                            {
                                Date = dataItemMembers.ElementAtOrDefault(0),
                                Open = dataItemMembers.ElementAtOrDefault(1),
                                High = dataItemMembers.ElementAtOrDefault(2),
                                Low = dataItemMembers.ElementAtOrDefault(3),
                                Close = dataItemMembers.ElementAtOrDefault(4),
                                Volume = dataItemMembers.ElementAtOrDefault(5)
                            });
                        }
                    }
                    counter++;
                }
            }
            return dataItems;
        }

        #endregion Protected Methods
    }
}
