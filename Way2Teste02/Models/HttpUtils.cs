using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using ServiceStack;

namespace Way2Teste02.Models
{
    public static class HttpUtils
    {
        [ThreadStatic]
        public static IHttpResultsFilter ResultsFilter;
        public static string GetJsonFromUrl(this string url, Action<HttpWebRequest> requestFilter = null, Action<HttpWebResponse> responseFilter = null)
        {
            return url.GetStringFromUrl("text/json", requestFilter, responseFilter);
        }
        public static string GetStringFromUrl(this string url, string accept = "*/*", Action<HttpWebRequest> requestFilter = null, Action<HttpWebResponse> responseFilter = null)
        {
            return SendStringToUrl(url, accept: accept, requestFilter: requestFilter, responseFilter: responseFilter);
        }
        public static string SendStringToUrl(this string url, string method = null,
            string requestBody = null, string contentType = null, string accept = "*/*",
            Action<HttpWebRequest> requestFilter = null, Action<HttpWebResponse> responseFilter = null)
        {
            var webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.ProtocolVersion = HttpVersion.Version10;
            if (method != null)
                webReq.Method = method;
            if (contentType != null)
                webReq.ContentType = contentType;

            webReq.Accept = accept;
            PclExport.Instance.AddCompression(webReq);

            if (requestFilter != null)
            {
                requestFilter(webReq);
            }

            if (ResultsFilter != null)
            {
                return ResultsFilter.GetString(webReq);
            }

            if (requestBody != null)
            {
                using (var reqStream = PclExport.Instance.GetRequestStream(webReq))
                using (var writer = new StreamWriter(reqStream))
                {
                    writer.Write(requestBody);
                }
            }

            using (var webRes = PclExport.Instance.GetResponse(webReq))
            using (var stream = webRes.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                if (responseFilter != null)
                {
                    responseFilter((HttpWebResponse)webRes);
                }
                return reader.ReadToEnd();
            }
        }
    }
}