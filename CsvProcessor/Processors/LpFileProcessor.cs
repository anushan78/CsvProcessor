using System;
using System.Collections.Generic;
using System.Text;
using CsvProcessor.Interfaces;
using CsvProcessor.Types;

namespace CsvProcessor.Processors
{
    class LpFileProcessor : IFileProcessor<LpFile>
    {
        public int CalculateMedian(LpFile file)
        {
            return file.MeterPointCode;
        }
    }
}
