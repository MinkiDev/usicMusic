using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace usicMusic.Connection
{
    internal class HttpConnection
    {
        private string url = "http://10.80.162.221:3000";
        private string token = "";

        public bool HttpLogin(string json)
        {
            string loginUrl = url + "/api/auth/login";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loginUrl); request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";

            // turn our request string into a byte stream
            byte[] postBytes = Encoding.UTF8.GetBytes(json);

            // this is important - make sure you specify type this way
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string result;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
                var jsonResult = JObject.Parse(result);

                if (jsonResult["status"].ToString() == "200")
                {
                    token = jsonResult["token"].ToString();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}