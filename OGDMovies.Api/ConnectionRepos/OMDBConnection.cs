using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using OGDMovies.Api.Models;
using OGDMovies.Common.Models;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface IOmdbConnection : IConnectionBase
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

        public dynamic RetrieveData(string query, bool expectMultiple = false, bool adult = false)
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

        public AggregatedModel GetMovieById(string id)
        {
            var query = $"i={id}";
            var omdbModel = RetrieveData(query) as OmdbModel;
            return new AggregatedModel()
            {
                MovieResults = new List<MoviesModel>
                {
                    omdbModel?.MapToCombined()
                }
            };
        }

        public AggregatedModel GetMovieByTitle(string title, string page = "1", bool adult = false)
        {
            var query = $"t={title}";
            var omdbModel = RetrieveData(query) as OmdbModel;
            return new AggregatedModel()
            {
                MovieResults = new List<MoviesModel>
                {
                    omdbModel?.MapToCombined()
                }
            };
        }

        public List<AutoCompleteModel> GetTitleAutoComplete(string title, bool adult = false)
        {
            throw new NotImplementedException();
        }
    }
}