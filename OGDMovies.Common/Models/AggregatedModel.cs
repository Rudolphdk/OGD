using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using OGDMovies.Common.Enums;

namespace OGDMovies.Common.Models
{
    public class AggregatedModel
    {
        public string Page { get; set; }
        public string TotalResults { get; set; }
        public string TotalPages { get; set; }
        public MovieRelevance RelevanceType { get; set; }
        public IEnumerable<MoviesModel> MovieResults { get; set; } = new List<MoviesModel>();
        public IEnumerable<YoutubeModel> YoutubeResults { get; set; } = new List<YoutubeModel>();
    }

    public class MoviesModel
    {
        public string Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string Plot { get; set; }
        public string ImageUrl { get; set; }
        public string ImageBackdropUrl { get; set; }
        public string Runtime { get; set; }
        public string Vote { get; set; }
        public IEnumerable<Rating> Ratings { get; set; } = new List<Rating>();

        public class Rating
        {
            public string Source { get; set; }
            public string Value { get; set; }
        }
    }

    public class YoutubeModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}