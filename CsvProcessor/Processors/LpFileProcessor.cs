using CsvProcessor.Interfaces;
using CsvProcessor.Types;
using System.Collections.Generic;
using System.Linq;

namespace CsvProcessor.Processors
{
    class LpFileProcessor : IFileProcessor<LpFile>
    {
        public decimal CalculateMedian(IEnumerable<LpFile> files)
        {
            return files.Select(fileRecord => fileRecord.Value).Average();
        }
    }
}
