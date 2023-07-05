using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ListaPrizonieriRazboi
{
    public class WebCrawler
    {
        private readonly string baseURL;
        private static readonly HttpClient httpClient = new HttpClient();

        public WebCrawler(string baseURL)
        {
            this.baseURL = baseURL;
            httpClient.BaseAddress = new Uri(baseURL);
            httpClient.DefaultRequestHeaders.Add("User-Agent", @"PostmanRuntime/7.32.3");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        }

        public async Task<string> DownloadHtml()
        {
            using HttpResponseMessage responseMessage = await httpClient.GetAsync(baseURL);
            

            responseMessage.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task DownloadFiles(string destinationFolder)
        {
            var htmlContent = await ExtractLinks();

            string pattern = "(?<=<a href=(?:'|\"))[^'\"]*?(?=(?:'|\"))";

            var regex = new Regex(pattern);

            ISet<string> linksSet = new HashSet<string>();
            foreach (var match in regex.Matches(htmlContent))
            {
                var urlEncodedLink = match.ToString()!;
                if (!linksSet.Contains(urlEncodedLink))
                    linksSet.Add(urlEncodedLink);
            }

            var di = new DirectoryInfo(destinationFolder);
            if (!di.Exists)
                di.Create();

            foreach (var link in linksSet)
            {
                await Task.Delay(5000);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(link);
                var linkUri = new Uri(link);
                var linkUrlDecoded = HttpUtility.UrlDecode(link);
                var fileName = linkUrlDecoded.Substring(linkUrlDecoded.LastIndexOf('/') + 1).Trim();
                var filePath = Path.Combine(di.FullName, fileName);
                Console.WriteLine($"Saving to disk {filePath}");
                try
                {
                    await using Stream responseStream = await httpClient.GetStreamAsync(linkUri);
                    using var fs = new FileStream(filePath, FileMode.Create);
                    await responseStream.CopyToAsync(fs);
                    fs.Close();
                    Console.WriteLine();
                }
                catch(HttpRequestException) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Could not download file URI {linkUri}");
                }
            }
        }

        private async Task<string> ExtractLinks()
        {
            string path = @"links.html"!;

            var fi = new FileInfo(path);

            if (fi.Exists)
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    fs.Position = 0;
                    fs.Seek(0, SeekOrigin.Begin);
                    byte[] b = new byte[fi.Length];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    var resultRead = await fs.ReadAsync(b, 0, (int)fi.Length, CancellationToken.None);
                    if (resultRead != 0)
                    {
                        return temp.GetString(b);
                    }
                }
            }

            return string.Empty;
        }
    }
}
