using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestfulClient
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class RestClient
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public RestClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public RestClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public RestClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = "";
        }

        public RestClient(string endpoint, HttpVerb method, string contentType, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
        }


        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public string MakeRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            request.Headers.Add("token", Guid.Empty.ToString());
            request.KeepAlive = false;

            try
            {
                if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
                {
                    var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    if (ContentType.Contains("Json"))
                        bytes = Encoding.UTF8.GetBytes(PostData);

                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.Headers["error"] != null)
                    {
                        responseValue += response.Headers["error"];
                    }

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        responseValue += String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue += reader.ReadToEnd();
                            }
                    }

                    return responseValue;
                }
            }
            catch(WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    var responseValue = string.Empty;

                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        responseValue += reader.ReadToEnd();
                        return responseValue;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
