using System;
using CsvHelper.Configuration.Attributes;

namespace CsvProcessor.Types
{
    public abstract class CsvFileBase
    {
        [Name("MeterPoint Code")]
        public int MeterPointCode { get; set; }
        [Name("Serial Number")]
        public int SerialNumber { get; set; }
        [Name("Plant Code")] 
        public string PlantCode { get; set; }    
        [Name("Date/Time")]
        public DateTime DateTime { get; set; }
        [Name("Data Type")]
        public string DataType { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
    }
}
