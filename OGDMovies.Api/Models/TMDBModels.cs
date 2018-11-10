using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OGDMovies.Common.Models;

namespace OGDMovies.Api.Models
{
    public class TmdbModelList
    {
        public string page { get; set; }
        public string total_results { get; set; }
        public string total_pages { get; set; }
        public IEnumerable<TmdbModel> results { get; set; } = new List<TmdbModel>();

        public AggregatedModel MapToCombinedList()
        {
            return new AggregatedModel()
            {
                Page = this.page,
                TotalResults = this.total_results,
                TotalPages = this.total_pages,
                MovieResults = this.results.Select(s => s.MapToMoviesModel())
            };
        }
    }

    /// <summary>
    /// This is the result model received from TMDB
    /// </summary>
    public class TmdbModel
    {
        public string adult { get; set; }
        public string backdrop_path { get; set; }
        public string belongs_to_collection { get; set; }
        public string budget { get; set; }
        //public string genres { get; set; }
        public string homepage { get; set; }
        public string id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public string popularity { get; set; }
        public string poster_path { get; set; }
        //public string production_companies { get; set; }
        //public string production_countries { get; set; }
        public string release_date { get; set; }
        public string revenue { get; set; }
        public string runtime { get; set; }
        //public string spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public string video { get; set; }
        public string vote_average { get; set; }
        public string vote_count { get; set; }

        public MoviesModel MapToMoviesModel()
        {
            return new MoviesModel()
            {
                Id = this.id,
                ImdbId = this.imdb_id,
                Title = this.title,
                ReleaseDate = this.release_date,
                Plot = this.overview,
                ImageUrl = $"{System.Configuration.ConfigurationManager.AppSettings["TMDB_IMAGE_URL"]}{this.poster_path}",
                Runtime = this.runtime,
                Ratings = new List<MoviesModel.Rating>()
                {
                    new MoviesModel.Rating() {Source = "IMDB", Value = this.vote_average}
                }
            };
        }
    }
}