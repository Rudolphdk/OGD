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
        Task<ActionResult> CallApiAndPopulateView(string resource, string view);
    }

    public class BaseController : Controller, IBaseController
    {
        private IApiClient _apiClient;

        //Constructor for Dependency Injection
        public BaseController(IApiClient apiClient)
        {
            this._apiClient = apiClient;
        }

        public async Task<ActionResult> CallApiAndPopulateView(string resource, string view)
        {
            var data = await _apiClient.RetrieveDataCached(resource);
            return View(view, data);
        }
    }
}
