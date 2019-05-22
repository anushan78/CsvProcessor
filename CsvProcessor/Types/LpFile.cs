using System;
using System.Collections.Generic;
using System.Text;
using CsvProcessor.Interfaces;

namespace CsvProcessor.Types
{
    public class LpFile : ICsvFile
    {
        public int value { get; set; }
    }
}
