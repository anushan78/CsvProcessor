using CsvProcessor.Services;
using System;

namespace CsvProcessor
{
    /// <summary>
    /// Initiator for the csv processor.
    /// </summary>
    public class App
    {
        private readonly ICsvFileService _csvFileService;

        public App(ICsvFileService csvFileService)
        {
            _csvFileService = csvFileService;
        }

        /// <summary>
        /// Initiates csv processing.
        /// </summary>
        public void Run()
        {
            _csvFileService.Process();
            Console.ReadKey();
        }
    }
}
