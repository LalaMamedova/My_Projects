using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestsApp.Services.HttpRequests;
using TestsLib.Models.UserModels;

namespace Tests.Controllers;

public class UserController : Controller
{
    public UserRequest _userRequest;

    public UserController(UserRequest userRequest)
    {
        _userRequest = userRequest;
    }

    public IActionResult Signin()
    {
        return View();
    }

    [HttpPost()]
    public async Task<IActionResult> SignInAction(RequestUser userRequest)
    {
        User user = await _userRequest.SinginRequest(userRequest);
        HttpContext.Response.Cookies.Append("User", JsonConvert.SerializeObject(user));
        return Redirect("/Home/Index");
    }
}
