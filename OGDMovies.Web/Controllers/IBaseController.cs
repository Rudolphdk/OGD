using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using OGDMovies.Common.Models;
using OGDMovies.Web.ApiCaller;

namespace OGDMovies.Web.Controllers
{
    public interface IBaseController : IController
    {
        Task<ActionResult> CallApiAndPopulateView<T>(string resource, string view) where T : new();
        Task<T> CallApiAutoComplete<T>(string query) where T : new();
    }

    public abstract class BaseController : Controller, IBaseController
    {
        private IApiClient _apiClient;

        //Constructor for Dependency Injection
        protected BaseController(IApiClient apiClient)
        {
            this._apiClient = apiClient;
        }

        public async Task<ActionResult> CallApiAndPopulateView<T>(string resource, string view) where T : new()
        {
            var data = await _apiClient.RetrieveDataCached<T>(resource);
            return View(view, data);
        }

        public async Task<T> CallApiAutoComplete<T>(string query) where T : new()
        {
            var data = await _apiClient.RetrieveData<T>(query);
            return data;
        }
    }
}
