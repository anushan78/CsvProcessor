using CsvProcessor.Configurations;
using CsvProcessor.Interfaces;
using CsvProcessor.Services;
using CsvProcessor.Types;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CsvProcessor.Test
{
    public class CsvFileServiceTests : IDisposable
    {
        private Mock<IFileProcessor<TouFile>> _touFileProcessor;
        private Mock<IFileProcessor<LpFile>> _lpFileProcessor;
        private Mock<IOptions<CsvSettings>> _csvSettings;
        private string _testFilesPath;

        public CsvFileServiceTests()
        {
            _touFileProcessor = new Mock<IFileProcessor<TouFile>>();
            _lpFileProcessor = new Mock<IFileProcessor<LpFile>>();
            _csvSettings = new Mock<IOptions<CsvSettings>>();
            _testFilesPath = $"{Directory.GetCurrentDirectory()}\\Test";
        }

        [Fact]
        public void Process_WhenFilesExistAndPositiveRecordsFound()
        {
            var fileRecords = new List<TouFile>()
            {
                new TouFile() { Energy = 1.44M },
                new TouFile() { Energy = 2.46M }
            };

            Directory.CreateDirectory(_testFilesPath);

            var options = Options.Create(new CsvSettings() {CsvFilePath = _testFilesPath});
            var fileName = CreateRandomFile(_testFilesPath, "TOU_");

            _touFileProcessor.Setup(p => p.GetAllRecords($"{_testFilesPath}\\{fileName}")).Returns(fileRecords);
            _touFileProcessor.Setup(p => p.CalculateMedian(fileRecords)).Returns(1.95M);

            using (var outFile = new FileUtility("outFile.txt"))
            {
                var csvFileService = new CsvFileService(_touFileProcessor.Object, _lpFileProcessor.Object, options);
                csvFileService.Process();
            }

            var lines = File.ReadAllLines("outFile.txt");
            Assert.Equal(2, lines.Length);
            Assert.True(lines[0].IndexOf("1.95") > 0);
        }

        [Fact]
        public void Process_WhenFilesExistAndOneRecordOnlyFound()
        {
            var fileRecords = new List<LpFile>()
            {
                new LpFile() { Value = 1.4M },
                new LpFile() { Value = 0.04M }
            };

            Directory.CreateDirectory(_testFilesPath);

            var options = Options.Create(new CsvSettings() { CsvFilePath = _testFilesPath });
            var fileName = CreateRandomFile(_testFilesPath, "LP_");

            _lpFileProcessor.Setup(p => p.GetAllRecords($"{_testFilesPath}\\{fileName}")).Returns(fileRecords);
            _lpFileProcessor.Setup(p => p.CalculateMedian(fileRecords)).Returns(0.72M);

            using (var outFile = new FileUtility("outFileSingleRecord.txt"))
            {
                var csvFileService = new CsvFileService(_touFileProcessor.Object, _lpFileProcessor.Object, options);
                csvFileService.Process();
            }

            var lines = File.ReadAllLines("outFileSingleRecord.txt");
            Assert.Single(lines);
        }

        [Fact]
        public void Process_WheninvalidFileThenNoRecordFound()
        {
            Directory.CreateDirectory(_testFilesPath);

            var options = Options.Create(new CsvSettings() { CsvFilePath = _testFilesPath });
            var fileName = CreateRandomFile(_testFilesPath, "PDS_");

            using (var outFile = new FileUtility("outFileNoRecord.txt"))
            {
                var csvFileService = new CsvFileService(_touFileProcessor.Object, _lpFileProcessor.Object, options);
                csvFileService.Process();
            }

            var lines = File.ReadAllLines("outFileNoRecord.txt");
            Assert.Empty(lines);
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

        public void Dispose()
        {
            foreach (var file in Directory.GetFiles(_testFilesPath))
            {
                File.Delete(file);
            }
        }
    }
}
