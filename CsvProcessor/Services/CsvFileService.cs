using CsvHelper;
using CsvProcessor.Configurations;
using CsvProcessor.Interfaces;
using CsvProcessor.Types;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

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
            var filesList = Directory.GetFiles(_csvSettings.CsvFilePath);

            if (filesList.Length > 0)
            {
                foreach (var file in filesList)
                {
                    using (var fileStreamReader = new StreamReader(file))
                    {
                        using (var csvReader = new CsvReader(fileStreamReader))
                        {
                            if (Path.GetFileName(file).StartsWith("TOU_"))
                            {
                                var touRecords = csvReader.GetRecords<TouFile>().ToList();
                                var touMedian = _touFileProcessor.CalculateMedian(touRecords);
                                foreach (var record in touRecords)
                                {
                                    if (record.Energy > (touMedian / 5))
                                        this.PrintRecord(Path.GetFileName(file), record.DateTime, record.Energy, touMedian);
                                }
                            }
                            else if (Path.GetFileName(file).StartsWith("LP_"))
                            {
                                var lpRecords = csvReader.GetRecords<LpFile>().ToList();
                                var lpMedian = _lpFileProcessor.CalculateMedian(lpRecords);
                                foreach (var record in lpRecords)
                                {
                                    if (record.Value > lpMedian / 5)
                                        this.PrintRecord(Path.GetFileName(file), record.DateTime, record.Value, lpMedian);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PrintRecord(string fileName, DateTime recordDateTime, decimal value, decimal medianValue)
        {
            Console.WriteLine($"{fileName} {recordDateTime} {value} {medianValue}");
        }
    }
}
