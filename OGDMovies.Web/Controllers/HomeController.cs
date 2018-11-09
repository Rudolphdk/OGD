using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OGDMovies.Api;
using OGDMovies.Api.Controllers;
using OGDMovies.Common.Enums;
using OGDMovies.Common.Models;

namespace OGDMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        string Baseurl = "http://localhost:12006/";
        public async Task<ActionResult> Popular()
        {
            CombinedModelList popularMovies = new CombinedModelList();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("Api/movies/?relevance=popular");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    popularMovies = JsonConvert.DeserializeObject<CombinedModelList>(EmpResponse);

                }
                //returning the employee list to view  
                return PartialView("_VerticalList", popularMovies);
            }
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