using CsvHelper;
using CsvProcessor.Interfaces;
using CsvProcessor.Types;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvProcessor.Processors
{
    /// <summary>
    /// Implements file processing utilities for the Tou type files.
    /// </summary>
    public class TouFileProcessor : IFileProcessor<TouFile>
    {
        /// <summary>
        /// Calculates the median of the specified list.
        /// </summary>
        /// <param name="files">TOUFiles list.</param>
        /// <returns>calculated median value.</returns>
        public decimal CalculateMedian(IEnumerable<TouFile> files)
        {
            return files.Select(fileRecord => fileRecord.Energy).Average();
        }

        /// <summary>
        /// Returns list of records in the file casted to the specified type.
        /// </summary>
        /// <param name="file">The full bath for the file.</param>
        /// <returns>>List of records casted to the specified type.</returns>
        public IEnumerable<TouFile> GetAllRecords(string file)
        {
            using (var fileStreamReader = new StreamReader(file))
            {
                using (var csvReader = new CsvReader(fileStreamReader))
                {
                    return csvReader.GetRecords<TouFile>().ToList();
                }
            }
        }
    }
}
