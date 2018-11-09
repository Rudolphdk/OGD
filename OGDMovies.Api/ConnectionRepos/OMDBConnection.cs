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
    public interface IOmdbConnection : IConnectionBase<CombinedModel, CombinedModel>
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
                return response.Content.ReadAsAsync<OmdbModel>().Result;
            }
            else
            {
                return "No movies found";
            }
        }

        /// <summary>
        /// Returns a single search result
        /// </summary>
        /// <param name="id">The Movie Id</param>
        /// <returns></returns>
        public CombinedModel GetMovieById(string id)
        {
            var query = $"i={id}";
            var omdbModel = RetrieveData(query) as OmdbModel;
            return omdbModel?.MapToCombined();
        }

        /// <summary>
        /// Returns a single search result
        /// </summary>
        /// <param name="title">The Movie Title</param>
        /// <returns></returns>
        public CombinedModel GetMovieByTitle(string title)
        {
            var query = $"t={title}";
            var omdbModel = RetrieveData(query) as OmdbModel;
            return omdbModel?.MapToCombined();
        }

    }
}