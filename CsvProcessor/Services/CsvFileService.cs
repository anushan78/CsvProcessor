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
    /// <summary>
    /// Defines Csv file processing service routines.
    /// </summary>
    public class CsvFileService : ICsvFileService
    {
        private readonly IFileProcessor<TouFile> _touFileProcessor;
        private readonly IFileProcessor<LpFile> _lpFileProcessor;
        private readonly CsvSettings _csvSettings;

        /// <summary>
        /// Constructor for csv service.
        /// </summary>
        /// <param name="touFileProcessor">TOU File processor.</param>
        /// <param name="lpFileProcessor">LP File processor.</param>
        /// <param name="csvSettings">Csv settings.</param>
        public CsvFileService(IFileProcessor<TouFile> touFileProcessor, IFileProcessor<LpFile> lpFileProcessor, IOptions<CsvSettings> csvSettings)
        {
            _touFileProcessor = touFileProcessor;
            _lpFileProcessor = lpFileProcessor;
            _csvSettings = csvSettings.Value;
        }

        /// <summary>
        /// Process the specified csv file.
        /// </summary>
        public void Process()
        {
            var filesList = Directory.GetFiles(_csvSettings.CsvFilePath);

            if (filesList.Length > 0)
            {
                foreach (var file in filesList)
                {

                    if (Path.GetFileName(file).StartsWith("TOU_"))
                    {
                        var touRecords = _touFileProcessor.GetAllRecords(file);
                        var touMedian = _touFileProcessor.CalculateMedian(touRecords);
                        foreach (var record in touRecords)
                        {
                            if (record.Energy > (touMedian / 5))
                                this.PrintRecord(Path.GetFileName(file), record.DateTime, record.Energy, touMedian);
                        }
                    }
                    else if (Path.GetFileName(file).StartsWith("LP_"))
                    {
                        var lpRecords = _lpFileProcessor.GetAllRecords(file);
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

        /// <summary>
        /// Prints a record to the console with specified strings.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="recordDateTime">Record Date Time.</param>
        /// <param name="value">The value.</param>
        /// <param name="medianValue">The median value.</param>
        private void PrintRecord(string fileName, DateTime recordDateTime, decimal value, decimal medianValue)
        {
            Console.WriteLine($"{fileName} {recordDateTime} {value} {medianValue}");
        }
    }
}
