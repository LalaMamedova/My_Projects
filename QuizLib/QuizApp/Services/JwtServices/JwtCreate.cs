using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Repository.ModelRepository;
using QuizLib.Dto.Response;
using QuizLib.Model;
using QuizLib.Model.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Services.JwtServices;

public class JwtCreate
{
    private readonly IConfigurationRoot _configure;
    private readonly UserManager<AppUser> _userManager;
    public JwtCreate( UserManager<AppUser> userManager)
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
            new(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaim = roles.Select(x => new Claim(ClaimTypes.Role, x));
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
