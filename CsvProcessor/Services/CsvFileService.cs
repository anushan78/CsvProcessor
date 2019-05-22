using CsvProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CsvProcessor.Configurations;
using CsvProcessor.Types;
using Microsoft.Extensions.Options;

namespace CsvProcessor.Services
{
    public class CsvFileService : ICsvFileService
    {
        private readonly IFileProcessor<TouFile> _touFileProcessor;
        private readonly IFileProcessor<LpFile> _lpFileProcessor;
        private readonly CsvSettings _csvSettings;

        public CsvFileService(IFileProcessor<TouFile> touFileProcessor, IFileProcessor<LpFile> lpFileProcessor, IOptions<CsvSettings> csvSettings)
        {
            _touFileProcessor = touFileProcessor;
            _lpFileProcessor = lpFileProcessor;
            _csvSettings = csvSettings.Value;
        }

        public void Process()
        {
            var filePath = _csvSettings.CsvFilePath;

            var touFiles = new List<TouFile>();
            touFiles.Add(new TouFile() { Energy = 2 });

            Console.WriteLine(_touFileProcessor.CalculateMedian(touFiles));
        }
    }
}
