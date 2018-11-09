using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using OGDMovies.Api.ConnectionRepos;
using OGDMovies.Api.Models;
using OGDMovies.Common.Enums;

namespace OGDMovies.Api.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly ITmdbConnection _tmdbConnection;
        private readonly IOmdbConnection _omdbConnection;

        //Constructor used for Dependency Injection
        public MoviesController(ITmdbConnection tmdbConnection, IOmdbConnection omdbConnection)
        {
            this._tmdbConnection = tmdbConnection;
            this._omdbConnection = omdbConnection;
        }
        
        /// <summary>
        /// Returns the retrieved movies information by an Id
        /// </summary>
        /// <param name="dbRepo">The movie database to search from</param>
        /// <param name="id">The Movie Id</param>
        /// <returns></returns>
        public IHttpActionResult GetById(DatabaseRepo dbRepo, string id)
        {
            switch (dbRepo)
            {
                case DatabaseRepo.Tmdb:
                    var tmdbResult = _tmdbConnection.GetMovieById(id);
                    return Json(new { result = tmdbResult });
                case DatabaseRepo.Omdb:
                    var omdbResult = _omdbConnection.GetMovieById(id);
                    return Json(new { result = omdbResult });
                default:
                    throw new Exception("Incorret DB Type");
            }
        }

        /// <summary>
        /// Returns the retrieved movies information by an Id
        /// </summary>
        /// <param name="dbRepo">The movie database to search from</param>
        /// <param name="title">The Movie Title</param>
        /// <returns></returns>
        public IHttpActionResult GetByTitle(DatabaseRepo dbRepo, string title)
        {
            switch (dbRepo)
            {
                case DatabaseRepo.Tmdb:
                    var tmdbResult = _tmdbConnection.GetMovieByTitle(title);
                    return Json(new { result = tmdbResult });
                case DatabaseRepo.Omdb:
                    var omdbResult = _omdbConnection.GetMovieByTitle(title);
                    return Json(new { result = omdbResult });
                default:
                    throw new Exception("Incorret DB Type");
            }
        }

        /// <summary>
        /// Returns the retrieved resuls according to the selected relevance
        /// </summary>
        /// <param name="relevance">ie. Latest, Popular, Top Rated</param>
        /// <param name="page">The Result Page</param>
        /// <returns></returns>
        public IHttpActionResult GetByRelevance(MovieRelevance relevance, string page = "1")
        {
            switch (relevance)
            {
                case MovieRelevance.Latest:
                    var latestMovie = _tmdbConnection.GetLatestMovie(page);
                    return Json(new { result = latestMovie });
                case MovieRelevance.Popular:
                    var popularMovies = _tmdbConnection.GetPopularMovies(page);
                    return Json(new { result = popularMovies });
                case MovieRelevance.TopRated:
                    var topRated = _tmdbConnection.GetTopRatedMovies(page);
                    return Json(new { result = topRated });
                default:
                    throw new Exception("Incorret Relevance Type");
            }
        }

        // POST api/movies
        public void Post([FromBody]string value)
        {
        }

    }
}
