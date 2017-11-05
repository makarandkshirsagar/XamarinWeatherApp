using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Model;
using System.Json;
using static WeatherApp.Model.ResponseResult;
using System.Collections.Generic;

namespace WeatherApp.Business
{
    internal class DataService
    {
        #region Commented
        /* public static async Task<JContainer> getDataFromService(string queryString)
         {
             JContainer data = null;
             HttpClient client = new HttpClient();
             client.MaxResponseContentBufferSize = 256000;

             //var response = await client.GetAsync(queryString);
             //var response = await client.GetStringAsync(queryString);


             var uri = new System.Uri(string.Format(queryString, string.Empty));
             var response = await client.GetAsync(uri);//.ConfigureAwait(false);
             if (response != null)
             {
                 var content = await response.Content.ReadAsStringAsync();
                 data = (JContainer)JsonConvert.DeserializeObject(content);
             }

             //if (client.GetAsync(queryString).IsCompleted) {    
             //if (response != null)
             //    {
             //        //string json = response.Content.ReadAsStringAsync().Result;
             //        data = (JContainer)JsonConvert.DeserializeObject(response);
             //    }
            // }
             //HttpResponseMessage responseGet = await client.GetAsync(queryString).ConfigureAwait(false);
             //string response = responseGet.Content.ReadAsStringAsync().Result;

             return data;
         }
         */
#endregion

        //Method 1 with HTTPClient
        public static async Task<JContainer> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(queryString);

            JContainer data = null;
            if (response != null)
            {
               // string json = response.Content.ReadAsStringAsync().Result;
                data = (JContainer)JsonConvert.DeserializeObject(response);
            }

            return data;
        }

        //Method 2 to fetch data with HTTPWebRequest
        public static async Task<JsonValue> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    //Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }

        //Method 3 To fetch data synchronously
        public static List<RootObject> GetResult(string SearchString)
        {
            try
            {
                var client = new HttpClient();
                //var json = Task.Run(async () => { return await client.GetStringAsync(SearchString); }).Result;
                var json =  client.GetAsync(SearchString);                
                return JsonConvert.DeserializeObject<List<RootObject>>(json.ToString());
            }
            catch (System.Exception exception)
            {
                return null;
            }
        }
    }
}