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

            Console.WriteLine(touFileProcessor.CalculateMedian(new TouFile()));
            Console.WriteLine(lpFileProcessor.CalculateMedian(new LpFile()));

            Console.WriteLine("Hello World!");
        }
    }
}
