using System;
using System.Collections.Generic;
using System.Text;
using CsvProcessor.Services;

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
