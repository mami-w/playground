using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", optional : false, reloadOnChange: true)
                .Build();

            //setup our DI
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IClientService, ClientService>()
            .Configure<ClientServiceOptions>(configuration.GetSection("ClientServiceOptions"))
            .BuildServiceProvider();

        //configure console logging
        serviceProvider
            .GetService<ILoggerFactory>()
            .AddConsole(LogLevel.Debug);

        var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();
        logger.LogDebug("Starting application");

        //do the actual work here
        var client = serviceProvider.GetService<IClientService>();
        var data = client.GetSomeData().Result;

        logger.LogDebug("All done!");

        }
    }
}
