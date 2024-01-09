using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace UserApi.Services.JWTServices.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
    }
}
