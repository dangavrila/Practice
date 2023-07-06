using Microsoft.Extensions.Logging;

namespace ListaPrizonieriRazboi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var url = @"https://once.mapn.ro/pages/lista-mortilor-de-razboi";

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("ListaPrizonieriRazboi.Program", LogLevel.Debug)
                    .AddConsole();
            });

            ILogger logger = loggerFactory.CreateLogger<WebCrawler>();
            logger.LogInformation($"Starting downloading from:\n{url}");

            var webCraweler = new WebCrawler(url, logger);
            await webCraweler.DownloadFiles("Files");
        }
    }
}