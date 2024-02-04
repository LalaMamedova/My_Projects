using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizApp.Repository.ModelRepository;
using QuizLib.Dto.Response;
using QuizLib.Model;
using QuizMVC.Models;
using System.Diagnostics;

namespace QuizMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string? userCookieValue = HttpContext.Request.Cookies["User"];

            if (userCookieValue != null)
            {
                var user = JsonConvert.DeserializeObject<LoginResponse>(userCookieValue);
                HttpContext.Session.SetString("User", user.Id);
            }
            return View();
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
