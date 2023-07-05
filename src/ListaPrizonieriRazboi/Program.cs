namespace ListaPrizonieriRazboi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var url = @"https://once.mapn.ro/pages/lista-mortilor-de-razboi";

            var webCraweler = new WebCrawler(url);
            await webCraweler.DownloadFiles("Files");
        }
    }
}