using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using OGDMovies.Api.Models;
using OGDMovies.Common.Models;

namespace OGDMovies.Api.ConnectionRepos
{
    public interface ITmdbConnection : IConnectionBase
    {
        AggregatedModel GetPopularMovies(string page);
        AggregatedModel GetTrendingMovies(string page);
        AggregatedModel GetTopRatedMovies(string page);
        AggregatedModel GetUpcommingMovies(string page);
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
            HttpResponseMessage response = client.GetAsync($"{query}").Result;
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

        public AggregatedModel GetMovieById(string id)
        {
            var query = $"movie/?api_key={Key}&i={id}";
            var tmdbModel = RetrieveData(query) as TmdbModel;
            return new AggregatedModel()
            {
                MovieResults = new List<MoviesModel>
                {
                    tmdbModel?.MapToMoviesModel()
                }
            };
        }

        public AggregatedModel GetMovieByTitle(string title)
        {
            var query = $"search/movie?api_key={Key}&query={title}&include_adult=true";
            var tmdbModelList = RetrieveData(query, true) as TmdbModelList;
            return tmdbModelList?.MapToCombinedList();
        }

        public AggregatedModel GetPopularMovies(string page)
        {
            var query = $"movie/popular?api_key={Key}&page={page}";
            var tmdbModelList = RetrieveData(query, true) as TmdbModelList;
            return tmdbModelList?.MapToCombinedList();
        }

        public AggregatedModel GetTrendingMovies(string page)
        {
            var query = $"trending/movie/week?api_key={Key}&page={page}";
            var tmdbModelList = RetrieveData(query, true) as TmdbModelList;
            return tmdbModelList?.MapToCombinedList();
        }

        public AggregatedModel GetTopRatedMovies(string page)
        {
            var query = $"movie/top_rated?api_key={Key}&page={page}";
            var tmdbModelList = RetrieveData(query, true) as TmdbModelList;
            return tmdbModelList?.MapToCombinedList();
        }

        public AggregatedModel GetUpcommingMovies(string page)
        {
            var query = $"movie/upcoming?api_key={Key}&page={page}";
            var tmdbModelList = RetrieveData(query, true) as TmdbModelList;
            return tmdbModelList?.MapToCombinedList();
        }
    }
}