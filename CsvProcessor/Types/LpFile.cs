using CsvHelper.Configuration.Attributes;
using CsvProcessor.Interfaces;

namespace CsvProcessor.Types
{
    /// <summary>
    ///  Defines properties specific for LP type csv files.
    /// </summary>
    public class LpFile : CsvFileBase, ICsvFile
    {
        [Name("Data Value")]
        public decimal Value { get; set; }
    }
}
