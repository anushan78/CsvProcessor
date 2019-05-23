using CsvProcessor.Configurations;
using CsvProcessor.Interfaces;
using CsvProcessor.Processors;
using CsvProcessor.Services;
using CsvProcessor.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace CsvProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Entry to run our csv processor service
            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Build configurations from appsettings
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection.AddOptions();
            serviceCollection.Configure<CsvSettings>(configuration.GetSection("CsvSettings"));

            // Inserts services into service collection
            serviceCollection.AddSingleton<ICsvFileService, CsvFileService>();
            serviceCollection.AddSingleton<IFileProcessor<TouFile>, TouFileProcessor>();
            serviceCollection.AddSingleton<IFileProcessor<LpFile>, LpFileProcessor>();

            // Includes the app.
            serviceCollection.AddTransient<App>();
        }
    }
}
