using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using ServiceStack;

namespace Way2Teste02.Models
{
    public static class HttpUtils
    {
        [ThreadStatic]
        public static IHttpResultsFilter ResultsFilter;
        public static string GetJsonFromUrl(this string url, Action<HttpWebRequest> requestFilter = null, Action<HttpWebResponse> responseFilter = null,string urlParameters = null)
        {
            if(urlParameters != null) return url.GetStringFromUrl("text/json", requestFilter, responseFilter,urlParameters:urlParameters);
            else return url.GetStringFromUrl("text/json", requestFilter, responseFilter);

        }
        public static string GetStringFromUrl(this string url, string accept = "*/*", Action<HttpWebRequest> requestFilter = null, Action<HttpWebResponse> responseFilter = null,string urlParameters = null)
        {
            if (urlParameters != null) return SendStringToUrl(url, urlParameters);
            return SendStringToUrl(url, accept: accept, requestFilter: requestFilter, responseFilter: responseFilter);
             
        }
        public static string SendStringToUrl(this string url, string method = null,
            string requestBody = null, string contentType = null, string accept = "*/*",
            Action<HttpWebRequest> requestFilter = null, Action<HttpWebResponse> responseFilter = null)
        {
            var webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Method = WebRequestMethods.Http.Get;
            webReq.ContentLength = Int32.MaxValue;
            AddHeaders(ref webReq);
           

            using (var webRes = webReq.GetResponse())
            {
                using (var stream = webRes.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    if (responseFilter != null)
                    {
                        responseFilter((HttpWebResponse) webRes);
                    }
                    return reader.ReadToEnd();
                }
            }
        }
        public static string SendStringToUrl(this string url,string parameters)
        {
            //-----------------------------
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            HttpResponseMessage response = new HttpResponseMessage();

            var result = client.GetAsync("search/repositories?q=malwares")
                    .Result.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);
            return response.Content.ReadAsStringAsync().ToString();
            //------------------------------
        }

        private static void AddHeaders(ref HttpWebRequest request)
        {
            try
            {
                request.ContentLength = 0;
                request.ContentType = "application/json";
                request.Accept = "application/vnd.github.v3+json";
                //request.ContentLength = request.ContentType.Length;
                request.Accept = "*/*";
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us");
                request.UserAgent =
                   "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0;" +
                   ".NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; InfoPath.2;" +
                   ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
                //request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.UseDefaultCredentials = true;
                request.PreAuthenticate = true;
                request.AllowAutoRedirect = false;
            }
            catch (WebException e)
            {
                Console.WriteLine("\nMain Exception raised!");
                Console.WriteLine("\nMessage:{0}", e.Message);
                Console.WriteLine("\nStatus:{0}", e.Status);
                Console.WriteLine("Press any key to continue..........");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMain Exception raised!");
                Console.WriteLine("Source :{0} ", e.Source);
                Console.WriteLine("Message :{0} ", e.Message);
                Console.WriteLine("Press any key to continue..........");
                Console.Read();
            }
        }

    }
}