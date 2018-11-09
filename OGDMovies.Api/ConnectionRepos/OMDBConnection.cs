using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using OGDMovies.Api.Models;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface IOmdbConnection : IConnectionBase<OmdbModel, OmdbModelList>
    {
    }
    public class OmdbConnection : IOmdbConnection
    {
        public string Key { get; private set; }
        public string Url { get; private set; }

        //constructor
        public OmdbConnection()
        {
            Key = System.Configuration.ConfigurationManager.AppSettings["OMDB_API_KEY"];
            Url = System.Configuration.ConfigurationManager.AppSettings["OMDB_API_URL"];
        }

        public dynamic RetrieveData(string query, bool expectMultiple = false)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"?apikey={Key}&{query}").Result;
            if (response.IsSuccessStatusCode)
            {

                string responseString = response.Content.ReadAsStringAsync().Result;
                return responseString;
                //var modelObject = response.Content.ReadAsAsync<Student>().Result;

                //var data = response.Content.ReadAsAsync<IEnumerable<YourClass>>().Result;
                //foreach (var x in data)
                //{
                //    //Call your store method and pass in your own object
                //}
            }
            else
            {
                return "No movies found";
            }
        }

        public OmdbModel GetMovieById(string id)
        {
            var query = $"i={id}";
            return RetrieveData(query);
        }

        public OmdbModelList GetMovieByTitle(string title)
        {
            var query = $"t={title}";
            return RetrieveData(query);
        }

    }
}