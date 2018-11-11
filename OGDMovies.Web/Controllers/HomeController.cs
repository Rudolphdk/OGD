using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OGDMovies.Common.Enums;
using OGDMovies.Common.Models;
using OGDMovies.Web.ApiCaller;

namespace OGDMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        AggregatedModel _aggregatedModel;
        readonly ApiConnection _apiConnection = new ApiConnection();

        public async Task<ActionResult> Index()
        {
            _aggregatedModel = await _apiConnection.RetrieveData("relevance=popular");

            return View("Index", _aggregatedModel);
        }

        
        public async Task<ActionResult> Popular()
        {
            ViewBag.Title = "Popular Movies";
            _aggregatedModel = await _apiConnection.RetrieveData("relevance=popular");

            return View("Content", _aggregatedModel);
        }
        public async Task<ActionResult> TopRated()
        {
            ViewBag.Title = "Top Rated Movies";
            _aggregatedModel = await _apiConnection.RetrieveData("relevance=toprated");

            return View("Content", _aggregatedModel);
        }
        public async Task<ActionResult> Trending()
        {
            ViewBag.Title = "Trending Movies";
            _aggregatedModel = await _apiConnection.RetrieveData("relevance=trending");

            return View("Content", _aggregatedModel);
        }
        public async Task<ActionResult> Upcomming()
        {
            ViewBag.Title = "Upcomming Movies";
            _aggregatedModel = await _apiConnection.RetrieveData("relevance=upcomming");

            return View("Content", _aggregatedModel);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string query)
        {
            ViewBag.Title = "Search Results";
            _aggregatedModel = await _apiConnection.RetrieveData($"dbRepo={DatabaseRepo.Tmdb}&title={query}");

            return View("Content", _aggregatedModel);
        }
    }
}