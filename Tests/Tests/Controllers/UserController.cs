using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestsApp.Repository.ModelReposotry;
using TestsApp.Services.HttpRequests;
using TestsLib.Dto;
using TestsLib.Models.UserModels;

namespace Tests.Controllers;

public class UserController : Controller
{
    private UserRequest _userRequest;
    private UserRepository _userRepository;
    public UserController(UserRequest userRequest, UserRepository userRepository)
    {
        _userRequest = userRequest;
        _userRepository = userRepository;
    }

    public IActionResult Signin()
    {
        return View();
    }
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost()]
    public async Task<IActionResult> SignInAction([FromBody] RequestUser userRequest)
    {
        User? user = await _userRepository.GetUser(userRequest);

        if (user!=null)
        {
            HttpContext.Response.Cookies.Append("User", JsonConvert.SerializeObject(user));
            return Redirect("/Home/Index");
        }
        return View();
    }

    [HttpPost()]
    public async Task<IActionResult> SignUpAction(UserDto userRequest)
    {
        await _userRepository.CreateAsync(userRequest);
        return Redirect("Signin");

    }
}
