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
        public MoviesController(ITmdbConnection tmdbConnection, IOmdbConnection omdbConnection)
        {
            this._tmdbConnection = tmdbConnection;
            this._omdbConnection = omdbConnection;
        }

        // GET api/movies
        public IEnumerable<string> GetAll()
        {
            return new string[] { "TMDB", "IMDB" };
        }
        
        public string GetAllMoviesByDbRepo(DatabaseRepo dbRepo, string page = "1")
        {
            int.TryParse(page, out var pageNr);

            //JObject json = JObject.Parse("hello");
            switch (dbRepo)
            {
                case DatabaseRepo.All:
                    _tmdbConnection.Page = pageNr;
                    _omdbConnection.Page = pageNr;
                    var tmdbResults = _tmdbConnection.RetrieveData();
                    var omdbResults = _omdbConnection.RetrieveData();
                    //Todo: combine results
                    return "";
                case DatabaseRepo.Tmdb:
                    _tmdbConnection.Page = pageNr;
                    return _tmdbConnection.RetrieveData();
                case DatabaseRepo.Omdb:
                    _omdbConnection.Page = pageNr;
                    var resultString = _omdbConnection.RetrieveData();
                    var obj   = JsonConvert.DeserializeObject<OmdbModel>(resultString);
                    return resultString;
                default:
                    throw new Exception("Incorret DB Type");
            }
           
        }

        // GET api/movies/5
        public string Get(int movieId, DatabaseRepo dbRepo)
        {
            return "movie5";
        }

        // POST api/movies
        public void Post([FromBody]string value)
        {
        }

    }
}
