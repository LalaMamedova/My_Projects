using Azure.Core;
using EcommerceLib.Context;
using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.JWT;
using EcommerceLib.Models.UserModel.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.SecurityTokenService;
using UserApi.Services.JWTServices.Interfaces;

namespace UserApi.Services.JWTServices.Classes
{
    public class LoginService:ILoginService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserDbContext _userDbContext;
        private readonly ITokenManager _tokenService;
        
        public LoginService(UserManager<AppUser> userManager, UserDbContext userDbContext, 
                            ITokenManager tokenService)
        {
            _userManager = userManager;
            _userDbContext = userDbContext;
            _tokenService = tokenService;
        }

        public async Task<string>? GiveRoleAsync(string id)
        {
            var role = await _userDbContext.UserRoles.Where(x => x.UserId == id).FirstOrDefaultAsync();
            var roleName = await _userDbContext.Roles.Where(x => x.Id == role.RoleId).FirstOrDefaultAsync();
            return roleName.Name;
        }

        public async Task Logout(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            if(user.RefreshToken == null)
            {
                throw new BadRequestException("User does not have a refresh token.");
            }
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
            await _userDbContext.SaveChangesAsync();

        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var managedUser = await _userManager.FindByEmailAsync(request.Email);

            if (managedUser == null)
            {
                throw new ArgumentException("Wrong email");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPasswordValid)
            {
                throw new ArgumentException("Wrong password");
            }

            var userInDb = _userDbContext.Users
              .Include(u => u.FavoriteProducts)
              .FirstOrDefault(u => u.Email == request.Email);


            if (userInDb is null)
            {
                throw new UnauthorizedAccessException();
            }

            var roleName = await GiveRoleAsync(userInDb.Id);
            var authToken = _tokenService.CreateToken(userInDb, roleName);

            if(userInDb.RefreshToken == null)
                userInDb.RefreshToken = _tokenService.RefreshToken();

            authToken.RefreshToken = userInDb.RefreshToken;
            
            await _userManager.UpdateAsync(userInDb);

            AuthResponse response = new()
            {
                Id = userInDb.Id,
                Role = roleName,
                Token = authToken,
                UserName = userInDb.UserName,
                Email = userInDb.Email,
                Products = userInDb.FavoriteProducts,
            };

            return response;
        }

     
    }
}
