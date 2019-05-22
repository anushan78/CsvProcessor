using CsvProcessor.Interfaces;
using CsvProcessor.Types;
using System.Collections.Generic;
using System.Linq;

namespace CsvProcessor.Processors
{
    public class TouFileProcessor : IFileProcessor<TouFile>
    {
        public decimal CalculateMedian(IEnumerable<TouFile> files)
        {
            return files.Select(fileRecord => fileRecord.Energy).Average();
        }
    }
}
