using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
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

        //Get top 10 auto complete results
        public List<AutoCompleteModel> MapToAutoCompleteList()
        {
            var autoCompleteList = results.Select(s => new AutoCompleteModel() { Title = s.title, ImageUrl = s.backdrop_path }).Take(10).ToList();
            return autoCompleteList;
        }
    }

    /// <summary>
    /// This is the result model received from TMDB
    /// </summary>
    public class TmdbModel
    {
        public string adult { get; set; }
        private string _backdrop_path;
        public string backdrop_path {
            get
            {
                if (string.IsNullOrEmpty(_backdrop_path))
                {
                    return "../Content/blank_bd.jpg";
                }
                return $"{System.Configuration.ConfigurationManager.AppSettings["TMDB_IMAGE_URL"]}{_backdrop_path}";
            }

            set => _backdrop_path = value;
        }
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

        private string _poster_path;
        public string poster_path
        {
            get
            {
                if (string.IsNullOrEmpty(_poster_path))
                {
                    return "../Content/blank_poster.jpg";
                }
                return $"{System.Configuration.ConfigurationManager.AppSettings["TMDB_IMAGE_URL"]}{_poster_path}";
            }

            set => _poster_path = value;
        }
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
                ImageUrl = this.poster_path,
                ImageBackdropUrl = this.backdrop_path,
                Runtime = this.runtime,
                Vote = this.vote_average,
                Ratings = new List<MoviesModel.Rating>()
                {
                    new MoviesModel.Rating() {Source = "IMDB", Value = this.vote_average}
                }
            };
        }
    }
}