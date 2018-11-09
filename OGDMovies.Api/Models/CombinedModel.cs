using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OGDMovies.Api.Models
{
    public class CombinedModelList
    {
        public string Page { get; set; }
        public string TotalResults { get; set; }
        public string TotalPages { get; set; }
        public IEnumerable<CombinedModel> Results { get; set; } = new List<CombinedModel>();
    }

    public class CombinedModel
    {
        public string Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string Plot { get; set; }
        public string ImageUrl { get; set; }
        public string Runtime { get; set; }
        public IEnumerable<Rating> Ratings { get; set; } = new List<Rating>();

        public class Rating
        {
            public string Source { get; set; }
            public string Value { get; set; }
        }
    }
}