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

        public string ToJson()
        {
            this.Title = this.Title.Replace("\"", "");
            this.Description = this.Description.Replace("\"","");
            return Json.Encode(this);
        }
    }
}