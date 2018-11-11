using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OGDMovies.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "API Loaded";
        }
    }
}