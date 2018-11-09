using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OGDMovies.Api;
using OGDMovies.Api.Controllers;
using OGDMovies.Common.Enums;

namespace OGDMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Popular()
        {
            return PartialView("_VerticalList");
        }
        public ActionResult TopRated()
        {
            return View();
        }
        public ActionResult LatestRelease()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
    }
}