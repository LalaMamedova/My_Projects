using EcommerceLib.Models.UserModel.JWT;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace UserApi.Services.JWTServices.Interfaces;

public interface ITokenManager
{
    public MyToken CreateToken(AppUser user, string userRole = "user");
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    public string RefreshToken();

}
