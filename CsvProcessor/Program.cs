using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvProcessor.Configurations;
using CsvProcessor.Interfaces;
using CsvProcessor.Processors;
using CsvProcessor.Services;
using CsvProcessor.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CsvProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Entry to runn app
            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection.AddOptions();
            serviceCollection.Configure<CsvSettings>(configuration.GetSection("CsvSettings"));

            // add services
            serviceCollection.AddSingleton<ICsvFileService, CsvFileService>();
            serviceCollection.AddSingleton<IFileProcessor<TouFile>, TouFileProcessor>();
            serviceCollection.AddSingleton<IFileProcessor<LpFile>, LpFileProcessor>();

            // add app
            serviceCollection.AddTransient<App>();
        }
    }
}
