using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using OGDMovies.Api.Models;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface ITmdbConnection : IConnectionBase<TmdbModel, TmdbModelList>
    {
        TmdbModelList GetPopularMovies(string page);
        TmdbModel GetLatestMovie(string page);
        TmdbModelList GetTopRatedMovies(string page);
    }
    public class TmdbConnection : ITmdbConnection
    {
        public string Key { get; private set; }
        public string Url { get; private set; }

        //constructor
        public TmdbConnection()
        {
            Key = System.Configuration.ConfigurationManager.AppSettings["TMDB_API_KEY"];
            Url = System.Configuration.ConfigurationManager.AppSettings["TMDB_API_URL"];
        }

        public dynamic RetrieveData(string query, bool expectMultiple = false)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"movie/{query}").Result;
            if (response.IsSuccessStatusCode)
            {
                //var stringResult = response.Content.ReadAsStringAsync().Result;
                if (expectMultiple)
                {
                    return response.Content.ReadAsAsync<TmdbModelList>().Result;
                }
                else
                {
                    return response.Content.ReadAsAsync<TmdbModel>().Result;
                }
            }
            else
            {
                return "No movies found";
            }
        }

        public TmdbModel GetMovieById(string id)
        {
            var query = $"?api_key={Key}&i={id}";
            return RetrieveData(query);
        }

        public TmdbModelList GetMovieByTitle(string title)
        {
            var query = $"?api_key={Key}&t={title}";
            return RetrieveData(query, true);
        }

        public TmdbModelList GetPopularMovies(string page)
        {
            var query = $"popular?api_key={Key}&page={page}";
            return RetrieveData(query, true);
        }

        public TmdbModel GetLatestMovie(string page)
        {
            var query = $"latest?api_key={Key}&page={page}";
            return RetrieveData(query);
        }

        public TmdbModelList GetTopRatedMovies(string page)
        {
            var query = $"top_rated?api_key={Key}&page={page}";
            return RetrieveData(query, true);
        }
    }
}