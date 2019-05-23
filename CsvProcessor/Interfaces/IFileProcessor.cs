using System.Collections.Generic;

namespace CsvProcessor.Interfaces
{
    /// <summary>
    /// Defines behaviours for file processor.
    /// </summary>
    /// <typeparam name="T">Type to be used in the processing.</typeparam>
    public interface IFileProcessor<T> where T : ICsvFile
    {
        /// <summary>
        /// Calculates the median of the specified list.
        /// </summary>
        /// <param name="type">The list.</param>
        /// <returns>calculated median value.</returns>
        decimal CalculateMedian(IEnumerable<T> type);

        /// <summary>
        /// Returns list of records in the file casted to the specified type.
        /// </summary>
        /// <param name="file">The full path for the file.</param>
        /// <returns>List of records casted to the specified type.</returns>
        IEnumerable<T> GetAllRecords(string file);
    }
}
