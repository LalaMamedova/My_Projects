using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizLib.Model.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizApp.Services.JwtServices;

public class JwtCreate
{
    private readonly IConfigurationRoot _configure;
    private readonly UserManager _userManager;
    public JwtCreate(UserManager userManager)
    {
        _configure = new ConfigurationBuilder()
           .AddUserSecrets("6e06f21a-caf4-4c75-841b-e2fcb24ce88a")
           .Build();
        _userManager = userManager;
    }


    public async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name,user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var role = await _userManager.GetRoleAsync(user.Id);
        var roleClaim = role.Select(x => new Claim(ClaimTypes.Role, x));
        claims.AddRange(roleClaim);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configure["JWT:IssuerKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expire = DateTime.Now.AddMinutes(30);

        var token = new JwtSecurityToken(
            issuer: _configure["JWT:Issuer"],
            audience: _configure["JWT:Audience"],
            claims: claims,
            expires: expire,
            signingCredentials: creds
        );
     
        return token;
        
    }
}
