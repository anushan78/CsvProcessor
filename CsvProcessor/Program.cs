using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvProcessor.Interfaces;
using CsvProcessor.Processors;
using CsvProcessor.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CsvProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileProcessor<TouFile>, TouFileProcessor>()
                .AddSingleton<IFileProcessor<LpFile>, LpFileProcessor>()
                .BuildServiceProvider();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var filePath = config["csvFilePath"];
            Console.WriteLine(filePath);

            var filesList = Directory.GetFiles(filePath);

            if (filesList.Length > 0)
            {
                foreach (var file in filesList)
                {
                    // Move this to a filehelper class
                    ProcessCsvFile(file);
                }
            }

            var touFileProcessor = serviceProvider.GetService<IFileProcessor<TouFile>>();
            var lpFileProcessor = serviceProvider.GetService<IFileProcessor<LpFile>>();

            var touFile = new TouFile() {MeterPointCode = 234};
           // Console.WriteLine(touFileProcessor.CalculateMedian(touFile));

            var lpFile = new LpFile() {MeterPointCode = 445};
            //Console.WriteLine(lpFileProcessor.CalculateMedian(lpFile));

            Console.WriteLine("Hello World!");
        }

        public static void ProcessCsvFile(string file)
        {
            using (var fileStreamReader = new StreamReader(file))
            {
                using (var csvReader = new CsvReader(fileStreamReader))
                {
                    if (Path.GetFileName(file).StartsWith("TOU_"))
                    {
                        var records = csvReader.GetRecords<TouFile>();
                    }
                    else if (Path.GetFileName(file).StartsWith("TOU_"))
                    {

                    }
                }
            }
        }

        public static void ProcessTouFile(IEnumerable<TouFile> touFileRecords)
        {

        }
    }
}
