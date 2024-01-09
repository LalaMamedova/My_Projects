using EcommerceLib.Models.UserModel.JWT;
using Microsoft.AspNetCore.Identity;

namespace UserApi.Services.JWTServices.Interfaces;

public interface ICreateTokenService
{
    public MyToken CreateToken(IdentityUser user, string userRole = "user");
}
