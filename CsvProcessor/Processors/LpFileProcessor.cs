using CsvHelper;
using CsvProcessor.Interfaces;
using CsvProcessor.Types;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvProcessor.Processors
{
    class LpFileProcessor : IFileProcessor<LpFile>
    {
        public decimal CalculateMedian(IEnumerable<LpFile> files)
        {
            return files.Select(fileRecord => fileRecord.Value).Average();
        }

        public IEnumerable<LpFile> GetAllRecords(string file)
        {
            using (var fileStreamReader = new StreamReader(file))
            {
                using (var csvReader = new CsvReader(fileStreamReader))
                {
                    return csvReader.GetRecords<LpFile>().ToList();
                }
            }
        }
    }
}
