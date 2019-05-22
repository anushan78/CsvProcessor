using System;
using System.Collections.Generic;
using System.Text;

namespace CsvProcessor.Interfaces
{
    public interface IFileProcessor<T> where T : ICsvFile
    {
        int CalculateMedian(IEnumerable<T> type);
    }
}
