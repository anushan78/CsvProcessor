using System;
using System.Collections.Generic;
using System.Text;
using CsvProcessor.Interfaces;
using CsvProcessor.Types;

namespace CsvProcessor.Processors
{
    public class TouFileProcessor : IFileProcessor<TouFile>
    {
        public int CalculateMedian(TouFile file)
        {
            return file.MeterPointCode;
        }
    }
}
