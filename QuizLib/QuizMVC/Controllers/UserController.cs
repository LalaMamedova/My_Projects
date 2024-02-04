using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizApp.Repository.ModelRepository;
using QuizLib.Dto.Request;
using QuizLib.Dto.Response;
using LoginRequest = QuizLib.Dto.Request.LoginRequest;

namespace QuizMVC.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult Signin()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Signout()
        {
            return View();
        }

        [HttpPost("/{role:alpha}")]
        public async Task<IActionResult> Signin(string role)
        {
            await _userRepository.AddRoleAsync(role);
            ViewData["Info"] = "Sucsses";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SigninAction(LoginRequest loginRequest) 
        {
            try
            {
                var user = await _userRepository.LoginAsync(loginRequest);
                HttpContext.Response.Cookies.Append("User", JsonConvert.SerializeObject(user));
                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                ViewData["Info"] = ex.Message;
            }

            return Redirect("Signin");  
        }

        [HttpPost]
        public async Task<IActionResult> SignupAction(ReqisterRequest registerRequest )
        {
            try
            {
                await _userRepository.SignupAsync(registerRequest);
                return Redirect("Signin");
            }
            catch (Exception ex)
            {
                ViewData["Info"] = ex.Message;
            }
            return Redirect("Signup");
        }
    }
}
