using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration.Attributes;
using CsvProcessor.Interfaces;

namespace CsvProcessor.Types
{
    public class LpFile : CsvFileBase, ICsvFile
    {
        [Name("Data Value")]
        public decimal Value { get; set; }
    }
}
