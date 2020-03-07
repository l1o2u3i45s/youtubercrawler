using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace youtubeana
{
    public static class HttpMethod
    {
        public static string GetMethod(string url) {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            try
            {
                WebResponse wr = req.GetResponse();
                using (var reader = new StreamReader(wr.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch {
                return string.Empty; 
            } 
        }
    }
}
