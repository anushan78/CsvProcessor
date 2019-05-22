using CsvProcessor.Services;
using System;

namespace CsvProcessor
{
    public class App
    {
        private readonly ICsvFileService _csvFileService;

        public App(ICsvFileService csvFileService)
        {
            _csvFileService = csvFileService;
        }

        public void Run()
        {
            _csvFileService.Process();
            Console.ReadKey();
        }
    }
}
