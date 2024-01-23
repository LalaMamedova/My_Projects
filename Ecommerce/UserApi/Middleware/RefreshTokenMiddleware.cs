using EcommerceLib.Context;
using EcommerceLib.Models.UserModel.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserApi.Services.JWTServices.Classes;
using UserApi.Services.JWTServices.Interfaces;

namespace UserApi.Middleware;

public class RefreshTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;

    public RefreshTokenMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;     
        _scopeFactory = scopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        using (var scope = _scopeFactory.CreateScope())
        {
            if (accessToken != null)
            {
                var serviceProvider = scope.ServiceProvider;
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                var tokenManager = serviceProvider.GetRequiredService<ITokenManager>();
                var loginService = serviceProvider.GetRequiredService<ILoginService>();

                var principal = tokenManager.GetPrincipalFromExpiredToken(accessToken);

                if (principal != null)
                {

                    var email = principal.Claims.First(x => x.Type == ClaimTypes.Email);
                    var user = await userManager.FindByEmailAsync(email.Value);

                    if (user != null && user.RefreshToken != null && user.RefreshTokenExpiryTime > DateTime.Now)
                    {
                        var roleName = await loginService.GiveRoleAsync(user.Id);
                        var response = tokenManager.CreateToken(user, roleName);
                        response.RefreshToken = tokenManager.RefreshToken();

                        user.RefreshToken = response.RefreshToken;
                        await userManager.UpdateAsync(user);
                    }
                }
            }
        }

        await _next.Invoke(context);

    }
}
