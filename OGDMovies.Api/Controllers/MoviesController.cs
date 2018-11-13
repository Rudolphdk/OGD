using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json;
using OGDMovies.Api.ConnectionRepos;
using OGDMovies.Api.Models;
using OGDMovies.Common.Enums;
using OGDMovies.Common.Models;

namespace OGDMovies.Api.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly ITmdbConnection _tmdbConnection;
        private readonly IOmdbConnection _omdbConnection;
        private readonly IYoutubeConnection _youtubeConnection;

        //Constructor used for Dependency Injection
        public MoviesController(ITmdbConnection tmdbConnection, IOmdbConnection omdbConnection, IYoutubeConnection youtubeConnection)
        {
            this._tmdbConnection = tmdbConnection;
            this._omdbConnection = omdbConnection;
            this._youtubeConnection = youtubeConnection;
        }

        /// <summary>
        /// Returns the retrieved movies information by an Id
        /// </summary>
        /// <param name="dbRepo">The movie database to search from</param>
        /// <param name="id">The Movie Id</param>
        /// <returns></returns>
        public IHttpActionResult GetById(DatabaseRepo dbRepo, string id)
        {
            AggregatedModel aggregatedModel;
            switch (dbRepo)
            {
                case DatabaseRepo.Tmdb:
                    aggregatedModel = _tmdbConnection.GetMovieById(id);
                    aggregatedModel.YoutubeResults = _youtubeConnection.RetrieveData(aggregatedModel.MovieResults.FirstOrDefault()?.Title);
                    return Json(new { Results = aggregatedModel });
                case DatabaseRepo.Omdb:
                    aggregatedModel = _omdbConnection.GetMovieById(id);
                    aggregatedModel.YoutubeResults = _youtubeConnection.RetrieveData(aggregatedModel.MovieResults.FirstOrDefault()?.Title);
                    return Json(new { Results = aggregatedModel });
                default:
                    throw new Exception("Incorret DB Type");
            }
        }

        /// <summary>
        /// Returns list of movies and youtube result by Id
        /// </summary>
        /// <param name="dbRepo">The movie database to search from</param>
        /// <param name="title">The Movie Title</param>
        /// <returns>AggregatedModel</returns>
        public AggregatedModel GetByTitle(DatabaseRepo dbRepo, string title, string page = "1")
        {
            AggregatedModel aggregatedModel;
            switch (dbRepo)
            {
                case DatabaseRepo.Tmdb:
                    aggregatedModel = _tmdbConnection.GetMovieByTitle(title, page);
                    aggregatedModel.YoutubeResults = _youtubeConnection.RetrieveData(title, page);
                    aggregatedModel.RelevanceType = MovieRelevance.Search;
                    return aggregatedModel;
                case DatabaseRepo.Omdb:
                    aggregatedModel = _omdbConnection.GetMovieByTitle(title, page);
                    aggregatedModel.YoutubeResults = _youtubeConnection.RetrieveData(title, page);
                    aggregatedModel.RelevanceType = MovieRelevance.Search;
                    return aggregatedModel;
                default:
                    throw new Exception("Incorret DB Type");
            }
        }

        /// <summary>
        /// Returns list of movies and youtube result according to the selected relevance
        /// </summary>
        /// <param name="relevance">ie. Latest, Popular, Top Rated</param>
        /// <param name="page">The Result Page</param>
        /// <returns></returns>
        public AggregatedModel GetByRelevance(MovieRelevance relevance, string page = "1")
        {
            AggregatedModel aggregatedModel;
            switch (relevance)
            {
                case MovieRelevance.Trending:
                    aggregatedModel = _tmdbConnection.GetTrendingMovies(page);
                    aggregatedModel.YoutubeResults = _youtubeConnection.RetrieveData("trending");
                    aggregatedModel.RelevanceType = MovieRelevance.Trending;
                    return aggregatedModel;
                case MovieRelevance.Popular:
                    aggregatedModel = _tmdbConnection.GetPopularMovies(page);
                    aggregatedModel.YoutubeResults = _youtubeConnection.RetrieveData("most popular");
                    aggregatedModel.RelevanceType = MovieRelevance.Popular;
                    return aggregatedModel;
                case MovieRelevance.TopRated:
                    aggregatedModel = _tmdbConnection.GetTopRatedMovies(page);
                    aggregatedModel.YoutubeResults = _youtubeConnection.RetrieveData("top rated");
                    aggregatedModel.RelevanceType = MovieRelevance.TopRated;
                    return aggregatedModel;
                case MovieRelevance.Upcomming:
                    aggregatedModel = _tmdbConnection.GetUpcommingMovies(page);
                    aggregatedModel.YoutubeResults = _youtubeConnection.RetrieveData("upcomming");
                    aggregatedModel.RelevanceType = MovieRelevance.Upcomming;
                    return aggregatedModel;
                default:
                    throw new Exception("Incorret Relevance Type");
            }
        }

        public List<AutoCompleteModel> GetAutoComplete(string autocomplete)
        {
            var result = _tmdbConnection.GetTitleAutoComplete(autocomplete);
            return result;
        }
    }
}
