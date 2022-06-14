using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public static class WebLoad
    {
        public static (int? WebLength, string Url, bool Succes) LoadUrl(string url, IProgress<string> progress = null)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var content = httpClient.GetStringAsync(url).Result;
                progress?.Report(url);

                return (content.Length, url,true);
            }
            catch (Exception ex)
            {
                File.AppendAllText("errors.txt",
                    $"URL: {url} - {DateTime.Now} : {ex.Message}{Environment.NewLine}");
                progress?.Report("ERROR + " + url);
                return (null,url,false);
            }
        }
    }
}
