using CsvHelper;
using CsvProcessor.Interfaces;
using CsvProcessor.Types;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvProcessor.Processors
{
    public class TouFileProcessor : IFileProcessor<TouFile>
    {
        public decimal CalculateMedian(IEnumerable<TouFile> files)
        {
            return files.Select(fileRecord => fileRecord.Energy).Average();
        }

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
