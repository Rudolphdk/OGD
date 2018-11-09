using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using OGDMovies.Api.Models;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface ITmdbConnection : IConnectionBase<CombinedModel, CombinedModelList>
    {
        CombinedModelList GetPopularMovies(string page);
        CombinedModel GetLatestMovie(string page);
        CombinedModelList GetTopRatedMovies(string page);
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

        public CombinedModel GetMovieById(string id)
        {
            var query = $"?api_key={Key}&i={id}";
            var tmdbModel = RetrieveData(query) as TmdbModel;
            return tmdbModel?.MapToCombined();
        }

        public CombinedModelList GetMovieByTitle(string title)
        {
            var query = $"?api_key={Key}&t={title}";
            var tmdbModelList = RetrieveData(query, true) as TmdbModelList;
            return tmdbModelList?.MapToCombinedList();
        }

        public CombinedModelList GetPopularMovies(string page)
        {
            var query = $"popular?api_key={Key}&page={page}";
            var tmdbModelList = RetrieveData(query, true) as TmdbModelList;
            return tmdbModelList?.MapToCombinedList();
        }

        public CombinedModel GetLatestMovie(string page)
        {
            var query = $"latest?api_key={Key}&page={page}";
            var tmdbModel = RetrieveData(query) as TmdbModel;
            return tmdbModel?.MapToCombined();
        }

        public CombinedModelList GetTopRatedMovies(string page)
        {
            var query = $"top_rated?api_key={Key}&page={page}";

            var tmdbModelList = RetrieveData(query, true) as TmdbModelList;
            return tmdbModelList?.MapToCombinedList();
        }
    }
}