using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using OGDMovies.Common.Enums;
using OGDMovies.Common.Models;
using OGDMovies.Web.ApiCaller;
using RestSharp;

namespace OGDMovies.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<ActionResult> Relevance(MovieRelevance relevance, string page = "1")
        {
            var relevanceName = CustomEnumHelper.DisplayNameFor(relevance);
            ViewBag.Title = $"{relevanceName} Movies";
            return await CallApiAndPopulateView<AggregatedModel>($"relevance={relevance}&page={page}", "Content");
        }

        public async Task<ActionResult> Index()
        {
            return await CallApiAndPopulateView<AggregatedModel>($"relevance=popular", "Index");
        }

        public async Task<ActionResult> Content()
        {
            return await Index();
        }

        public async Task<ActionResult> Search(string query, string page = "1")
        {
            ViewBag.Title = $"Search Results: {query}";
            ViewBag.Query = query;
            return await CallApiAndPopulateView<AggregatedModel>($"dbRepo={DatabaseRepo.Tmdb}&title={query}&page={page}", "Content");
        }

        public async Task<string> AutoCompleteMovieTitles(string query)
        {
            var result = await CallApiAutoComplete<List<string>>($"autocomplete={query}");
            return new JavaScriptSerializer().Serialize(result);
        }
    }
}