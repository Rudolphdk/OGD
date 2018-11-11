using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OGDMovies.Web.Enums;
using OGDMovies.Common.Models;

namespace OGDMovies.Web.Models
{
    public class ThumbnailViewModel
    {
        public ThumbnailViewModel(string id, string title, string imageUrl, string description, ThumbnailType type)
        {
            this.Id = id;
            this.Title = title;
            this.ImageUrl = imageUrl;
            this.Description = description;
            this.Type = type;
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public ThumbnailType Type { get; set; }
    }
}