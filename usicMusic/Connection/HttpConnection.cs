using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Text;
using usicMusic.View;

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

        public void getMusicList(int musicNum)
        {
            var client = new RestClient(url + @"/api/music");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest(Method.GET);

            request.AddHeader("x-access-token", token);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            IRestResponse response2 = client.Execute(request);

            MusicList ml = new MusicList(response2.Content, musicNum);
            ml.Show();

            //var name = response2.Data.Name;

            //// easy async support
            //client.ExecuteAsync(request, response => {
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response => {
            //    Console.WriteLine(response.Data.Name);
            //});

            //// abort the request on demand
            //asyncHandle.Abort();
        }

        public void UploadMultipart(string filepath, string filename, string contentType)
        {
            var webClient = new WebClient();
            string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
            webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            Byte[] file = null;
            try { file = File.ReadAllBytes(filepath); }
            catch { }
            var fileData = webClient.Encoding.GetString(file);
            var package = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", boundary, filename, contentType, fileData);
            var nfile = webClient.Encoding.GetBytes(package);
            byte[] resp = webClient.UploadData(url + @"/api/music", "POST", nfile);
        }
    }
}