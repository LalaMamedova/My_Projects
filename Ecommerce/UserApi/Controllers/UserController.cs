using EcommerceLib.Models.UserModel.JWT;
using EcommerceLib.Models.UserModel.Users;
using EcommerceLib.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserApi.Services.JWTServices.Interfaces;
using UserApi.Services.JWTServices.Classes;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public  class UserController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILoginService _loginService;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserController(UserManager<AppUser> userManager,
            ILoginService loginService,
            RoleManager<IdentityRole> roleManager)

    {
        _userManager = userManager;
        _loginService = loginService;
        _roleManager = roleManager;
    }

    [HttpPost("Register/{role:alpha}")]
    public async Task<IActionResult> Register(string role,[FromBody] RegistrationRequest request)
    {
        var user = new AppUser { UserName = request.Username, Email = request.Email};
        IdentityResult? result  = await _userManager.CreateAsync(user, request.Password);

        //await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
        //await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        role = char.ToUpper(role[0]) + role.Substring(1);
        await _userManager.AddToRoleAsync(user, role);
       
        return Ok(new { Description = "Succeed registration", Status = 200 });
    }



    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
      
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _loginService.Login(request);
            return Ok(response);
        }
        catch (FileNotFoundException ex)
        {
            return BadRequest(new { Description = ex.Message, Status = 400 });
        }
        catch(UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

       
    }

    //[HttpPost("RefreshToken")]
    //public async Task<IActionResult> RefreshToken([FromBody] MyToken request)
    //{
    //    if (request is null)
    //    {
    //        return BadRequest("Invalid client request");
    //    }

    //    var principal = _tokenManger.GetPrincipalFromExpiredToken(request.AcssesToken);

    //    if (principal == null)
    //    {
    //        return BadRequest("Invalid client request");
    //    }

    //    var email = principal.Claims.First(x => x.Type == ClaimTypes.Email);
    //    var user = await _userManager.FindByEmailAsync(email.Value);

    //    if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
    //    {
    //        return BadRequest("Invalid access token or refresh token");
    //    }

    //    var roleName = await _loginService.GiveRoleAsync(user.Id);
    //    var response = _tokenManger.CreateToken(user, roleName);
    //    response.RefreshToken = _tokenManger.RefreshToken();

    //    user.RefreshToken = response.RefreshToken;
    //    await _userManager.UpdateAsync(user);
        
    //    return Ok(response);
    //}

    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Unauthorized(new { Description = "User not found", Status = 404 });
        }
        if (user.EmailConfirmed)
        {
            return BadRequest(new { Description = "Email is alredy is confirmed", Status = 400 });
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { Description = "Email is confirmed", Status = 200 });
    }

    [HttpPost("Logout/{userId}")]
    public async Task<IActionResult> Logout(string userId)
    {
        try
        {
            await _loginService.Logout(userId);
            return Ok(false);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}
