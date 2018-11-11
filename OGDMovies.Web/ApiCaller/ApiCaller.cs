using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using OGDMovies.Common.Models;

namespace OGDMovies.Web.ApiCaller
{
    public class ApiConnection
    {
        internal string BaseUrl { get; private set; }

        public ApiConnection()
        {
            BaseUrl = System.Configuration.ConfigurationManager.AppSettings["API_URL"];
        }

        public async Task<AggregatedModel> RetrieveData(string serviceResource)
        {
            AggregatedModel aggregatedModel = new AggregatedModel();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource using HttpClient  
                HttpResponseMessage Res = await client.GetAsync($"Api/movies/?{serviceResource}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var aggregatedModelJson = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the AggregatedModel list  
                    aggregatedModel = JsonConvert.DeserializeObject<AggregatedModel>(aggregatedModelJson);
                }
            }
            return aggregatedModel;
        }
    }
}