using System;
using System.Collections.Generic;
using System.Text;
using CsvProcessor.Interfaces;

namespace CsvProcessor.Types
{
    public class TouFile : ICsvFile
    {
        public int Energy { get; set; }
    }
}
