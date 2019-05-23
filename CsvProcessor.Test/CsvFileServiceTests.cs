using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Moq;
using CsvProcessor.Interfaces;
using CsvProcessor.Processors;
using CsvProcessor.Services;
using CsvProcessor.Types;
using CsvProcessor.Configurations;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.Extensions.Options;

namespace CsvProcessor.Test
{
    public class CsvFileServiceTests
    {
        Mock<IFileProcessor<TouFile>> _touFileProcessor;
        Mock<IFileProcessor<LpFile>> _lpFileProcessor;
        Mock<IOptions<CsvSettings>> _csvSettings;

        public CsvFileServiceTests()
        {
            _touFileProcessor = new Mock<IFileProcessor<TouFile>>();
            _lpFileProcessor = new Mock<IFileProcessor<LpFile>>();
            _csvSettings = new Mock<IOptions<CsvSettings>>();
        }

        [Fact]
        public void Process_WhenFilesExistAndPositiveRecordsFound()
        {
            List<TouFile> fileRecords = new List<TouFile>()
            {
                new TouFile() { Energy = 1.44M },
                new TouFile() {Energy = 2.46M }
            };

            var testFilesPath = $"{Directory.GetCurrentDirectory()}\\Test";
            Directory.CreateDirectory(testFilesPath);

            var options = Options.Create(new CsvSettings() {CsvFilePath = testFilesPath});
            var fileName = CreateRandomFile(testFilesPath, "TOU_");

            _touFileProcessor.Setup(p => p.GetAllRecords($"{testFilesPath}\\{fileName}")).Returns(fileRecords);
            _touFileProcessor.Setup(p => p.CalculateMedian(fileRecords)).Returns(1.95M);

            using (var outFile = new FileUtility("outFile.txt"))
            {
                var csvFileService = new CsvFileService(_touFileProcessor.Object, _lpFileProcessor.Object, options);
                csvFileService.Process();
            }

            var lines = File.ReadAllLines("outFile.txt");
            Assert.Equal(2, lines.Length);
        }

        private string CreateRandomFile(string filePath, string prefix)
        {
            var path = $"{filePath}\\{prefix}{Guid.NewGuid()}.csv";
            var c = new StreamWriter(@path, true)
            {
                AutoFlush = true
            };

            c.Dispose();
            return Path.GetFileName(path);
        }
    }
}
