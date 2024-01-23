using EcommerceLib.Models.UserModel.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Services.JWTServices.Interfaces;

namespace UserApi.Services.JWTServices.Classes;

public class TokenManager : ITokenManager
{
    readonly ICreateTokenService _createTokenService;
    readonly IRefreshTokenService _refreshTokenService;
    readonly IConfiguration _configuration;

    public TokenManager(ICreateTokenService createTokenService, 
        IRefreshTokenService refreshTokenService, IConfiguration configuration)
    {
        _createTokenService = createTokenService;
        _refreshTokenService = refreshTokenService;
        _configuration = configuration;
    }

    public MyToken CreateToken(AppUser user, string userRole = "user")
    {
       return _createTokenService.CreateToken(user,userRole);
    }

    public string RefreshToken()
    {
        return _refreshTokenService.GenerateRefreshToken();
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:TokenKey"]))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
