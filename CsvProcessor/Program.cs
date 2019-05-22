using System;
using CsvProcessor.Interfaces;
using CsvProcessor.Processors;
using CsvProcessor.Types;
using Microsoft.Extensions.DependencyInjection;

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

            var touFileProcessor = serviceProvider.GetService<IFileProcessor<TouFile>>();
            var lpFileProcessor = serviceProvider.GetService<IFileProcessor<LpFile>>();

            var touFile = new TouFile() { MeterPointCode = 234 };
            Console.WriteLine(touFileProcessor.CalculateMedian(touFile));

            var lpFile = new LpFile() { MeterPointCode = 445 };
            Console.WriteLine(lpFileProcessor.CalculateMedian(lpFile));

            Console.WriteLine("Hello World!");
        }
    }
}
