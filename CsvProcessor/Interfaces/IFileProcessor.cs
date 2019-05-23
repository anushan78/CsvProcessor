using System.Collections.Generic;

namespace CsvProcessor.Interfaces
{
    public interface IFileProcessor<T> where T : ICsvFile
    {
        decimal CalculateMedian(IEnumerable<T> type);
        IEnumerable<T> GetAllRecords(string file);
    }
}
