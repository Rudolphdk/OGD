using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using OGDMovies.Common.Models;
using OGDMovies.Web.Cache;
using RestSharp;

namespace OGDMovies.Web.ApiCaller
{
    //Not used - Old way
    //This is a manual implementation of how to create a connection and call the API
    #region Old API Caller
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
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource using HttpClient  
                HttpResponseMessage response = await client.GetAsync($"Api/movies/?{serviceResource}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var aggregatedModelJson = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the AggregatedModel list  
                    aggregatedModel = JsonConvert.DeserializeObject<AggregatedModel>(aggregatedModelJson);
                }
            }
            return aggregatedModel;
        }
    }
    #endregion

    //Used - New way
    //This is a quicker way using RestSharp (Nuget) package
    public interface IApiClient : IRestClient
    {
        Task<AggregatedModel> RetrieveData(string serviceResource);
        Task<AggregatedModel> RetrieveDataCached(string serviceResource);
    }

    public sealed class ApiClient : RestClient, IApiClient
    {
        private ICacheService _cache;

        //Use Dependency Injeciton to inject ICacheService here
        public ApiClient(ICacheService cache)
        {
            _cache = cache;
            BaseUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["API_URL"]);
        }

        //Call the cached method for cached result
        public async Task<AggregatedModel> RetrieveDataCached(string serviceResource)
        {
            // I use the serviceResource string as the cacheKey, 
            // which is perfect as it contains what you are retrieving and the page number.
            //This works well for caching individual result pages too.
            return await _cache.GetOrSet(serviceResource, () => RetrieveData(serviceResource));
        }

        //Call the non cached method for freash result from API
        public async Task<AggregatedModel> RetrieveData(string serviceResource)
        {
            RestRequest request = new RestRequest($"Api/movies/?{serviceResource}", Method.GET);
            var taskCompletionSource = new TaskCompletionSource<AggregatedModel>();
            this.ExecuteAsync<AggregatedModel>(request, (response) => taskCompletionSource.SetResult(response.Data));
            return await taskCompletionSource.Task;
        }
    }
}