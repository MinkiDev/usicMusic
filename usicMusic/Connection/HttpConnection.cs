﻿using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using usicMusic.View;

namespace usicMusic.Connection
{
    internal class HttpConnection
    {
        private static string url = "http://115.68.22.74";
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

            var request = new RestRequest(Method.GET);

            request.AddHeader("x-access-token", token);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            IRestResponse response2 = client.Execute(request);
            Debug.WriteLine(response2.Content.ToString());
            MusicList ml = new MusicList(response2.Content, musicNum);
            ml.Show();
        }
    }
}