using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface ITmdbConnection : IConnectionBase
    {
    }
    public class TmdbConnection : ITmdbConnection
    {
        public string Key { get; private set; }
        public string Url { get; private set; }
        public int Page { get; set; } = 1;

        //constructor
        public TmdbConnection()
        {
            Key = System.Configuration.ConfigurationManager.AppSettings["TMDB_API_KEY"];
            Url = System.Configuration.ConfigurationManager.AppSettings["TMDB_API_URL"];
        }

        public string RetrieveData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"movie/popular?api_key={Key}&page={Page}").Result;
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


    }
}