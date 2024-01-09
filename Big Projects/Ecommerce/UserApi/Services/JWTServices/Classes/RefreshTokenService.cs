using System.Security.Cryptography;
using UserApi.Services.JWTServices.Interfaces;

namespace UserApi.Services.JWTServices.Classes
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
