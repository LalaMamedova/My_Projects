using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Tests.Models;
using TestsApp.Services.HttpRequests;
using TestsLib.Models.UserModels;

namespace Tests.Controllers
{
    public class HomeController : Controller
    {
        private TestRequests _testRequests;
        public HomeController(TestRequests testRequests)
        {
            _testRequests = testRequests;
        }

        public async Task<IActionResult> Index()
        {
            string? userCookieValue = HttpContext.Request.Cookies["User"];

            if(userCookieValue != null)
            {
                HttpContext.Session.SetInt32("User", 1);
            }
            IEnumerable<string> response = await _testRequests.GetPopularTags();

            if(response == null)
            {
                return View("Error");
            }
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
