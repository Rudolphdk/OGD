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

        public async Task<ActionResult> Content()
        {
            _aggregatedModel = await _apiConnection.RetrieveData("relevance=popular");
            return View("Index", _aggregatedModel);
        }
        
        public async Task<ActionResult> Popular(string page = "1")
        {
            ViewBag.Title = "Popular Movies";
            _aggregatedModel = await _apiConnection.RetrieveData($"relevance=popular&page={page}");
            return View("Content", _aggregatedModel);
        }
        public async Task<ActionResult> TopRated(string page = "1")
        {
            ViewBag.Title = "Top Rated Movies";
            _aggregatedModel = await _apiConnection.RetrieveData($"relevance=toprated&page={page}");
            return View("Content", _aggregatedModel);
        }
        public async Task<ActionResult> Trending(string page = "1")
        {
            ViewBag.Title = "Trending Movies";
            _aggregatedModel = await _apiConnection.RetrieveData($"relevance=trending&page={page}");
            return View("Content", _aggregatedModel);
        }
        public async Task<ActionResult> Upcomming(string page = "1")
        {
            ViewBag.Title = "Upcomming Movies";
            _aggregatedModel = await _apiConnection.RetrieveData($"relevance=upcomming&page={page}");
            return View("Content", _aggregatedModel);
        }

        
        public async Task<ActionResult> Search(string query, string page = "1")
        {
            ViewBag.Title = $"Search Results: {query}";
            ViewBag.Query = query;
            _aggregatedModel = await _apiConnection.RetrieveData($"dbRepo={DatabaseRepo.Tmdb}&title={query}&page={page}");
            return View("Content", _aggregatedModel);
        }
    }
}