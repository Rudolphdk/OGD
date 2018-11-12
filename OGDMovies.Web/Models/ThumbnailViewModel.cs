using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using OGDMovies.Web.Enums;
using OGDMovies.Common.Models;
using HtmlHelper = System.Web.WebPages.Html.HtmlHelper;

namespace OGDMovies.Web.Models
{
    public class ThumbnailViewModel
    {
        public ThumbnailViewModel(string id, string title, string imageUrl, string description, string releaseDate, ThumbnailType type)
        {
            this.Id = id;
            this.Title = title;
            this.ImageUrl = imageUrl;
            this.Description = description;
            this.ReleaseDate = releaseDate;
            this.Runtime = Runtime;
            this.Type = type;
        }

        public ThumbnailViewModel(MoviesModel movieModel)
        {
            this.Id = movieModel.Id;
            this.Title = movieModel.Title;
            this.ImageUrl = movieModel.ImageUrl;
            this.Description = movieModel.Plot;
            this.ReleaseDate = movieModel.ReleaseDate;
            this.Runtime = movieModel.Runtime;
            this.Type = ThumbnailType.Movie;
        }

        public ThumbnailViewModel(YoutubeModel youtubeModel)
        {
            this.Id = youtubeModel.Id;
            this.Title = youtubeModel.Title;
            this.ImageUrl = youtubeModel.ImageUrl;
            this.Description = youtubeModel.Description;
            this.Type = ThumbnailType.Youtube;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public string Runtime { get; set; }
        public ThumbnailType Type { get; set; }

        public string ToJson()
        {
            this.Title = this.Title.Replace("\"", "");
            this.Description = this.Description.Replace("\"","");
            return Json.Encode(this);
        }
    }
}