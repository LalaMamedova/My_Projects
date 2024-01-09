using EcommerceLib.Models.UserModel.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Services.JWTServices.Interfaces;

namespace UserApi.Services.JWTServices.Classes;

public class CreateTokenService : ICreateTokenService
{
    private const int ExpirationMinutes = 30;
    private readonly IConfiguration _configuration;

    public CreateTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MyToken CreateToken(IdentityUser user, string userRole ="user")
    {
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["JWT:Audience"]),
                new Claim(ClaimTypes.Role,userRole),
        };

        var signingCredentials = new SigningCredentials(
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:TokenKey"])),
                           SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
               issuer: _configuration["JWT:Issuer"],
               audience: _configuration["JWT:Audience"],
               claims: claims,
               expires: DateTime.Now.AddMinutes(ExpirationMinutes),
               signingCredentials: signingCredentials);

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        MyToken myToken = new() { AcssesToken = tokenString };
        return myToken;
    }

}
