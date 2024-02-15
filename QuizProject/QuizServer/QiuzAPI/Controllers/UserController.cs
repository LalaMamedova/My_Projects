using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Repository.ModelRepository;
using QuizApp.Services;
using QuizLib.Dto.Request;
using QuizLib.Model.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QiuzAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private UserRepository _userRepository;
    public UserController(UserRepository userRepository, UserManager userManager)
    {
        _userRepository = userRepository;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Signin([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var user = await _userRepository.SigninAsync(loginRequest);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new {Error = ex.Message });
        }
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] ReqisterRequest registerRequest)
    {
        try
        {
            await _userRepository.SignupAsync(registerRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }


    [HttpGet("google-auth")]
    public async Task<IActionResult> GoogleSignin()
    {
        var tokenString = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");
        try
        {
            var user = await _userRepository.GoogleSigninAsync(tokenString);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("sign-out/{email}")]
    [Authorize]
    public async Task<IActionResult> Signout(string email)
    {

        try
        {
            await _userRepository.SignOutAsync(email);
            return Ok();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}
