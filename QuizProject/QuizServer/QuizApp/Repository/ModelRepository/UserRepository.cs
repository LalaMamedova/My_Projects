using Microsoft.AspNetCore.Identity;
using QuizApp.Services;
using QuizApp.Services.JwtServices;
using QuizLib.Dto.Request;
using QuizLib.Dto.Response;
using QuizLib.Model;
using QuizLib.Model.User;
using SharpCompress.Common;
using System.IdentityModel.Tokens.Jwt;

namespace QuizApp.Repository.ModelRepository;

public class UserRepository
{
    private readonly JwtCreate _jwtCreate;
    private readonly UserManager _userManager;
    public UserRepository(UserManager userManager,
                          JwtCreate jwtCreate)
    {

        _jwtCreate = jwtCreate;
        _userManager = userManager;
    }

    public async Task<LoginResponse> SigninAsync(LoginRequest loginRequest)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            PasswordHasher<AppUser> hasher = new();
            var passCheck = hasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);

            if (passCheck != PasswordVerificationResult.Success)
            {
                throw new InvalidFormatException("Wrong password");
            }

            var token = await _jwtCreate.CreateJwtToken(user);

            user.Token = new()
            {
                AcssesToken = new JwtSecurityTokenHandler().WriteToken(token),
            };

            await _userManager.UpdateAsync(user);

            return new LoginResponse
            {
                Email = user.Email,
                Id = user.Id.ToString(),
                UserQuizes = user.UserQuizes,
                Username = user.UserName,
                UserToken = new Token()
                {
                    AcssesToken = new JwtSecurityTokenHandler().WriteToken(token),
                }
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message) ;
        }
        
      
    }

    public async Task<AppUser> GoogleSigninAsync(string tokenString) {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);
            string emailClaim = token.Claims.FirstOrDefault(claim => claim.Type == "email").Value;

            var user = await _userManager.FindByEmailAsync(emailClaim);
            user.Token = new() { AcssesToken = tokenString };
            await _userManager.UpdateAsync(user);

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }        
    }
 
    public async Task SignupAsync(ReqisterRequest reqisterRequest)
    {   
        var newUser = new AppUser()
        {
            Email = reqisterRequest.Email,
            UserName = reqisterRequest.Username,
            Password = reqisterRequest.Password,
            UserQuizes = new(),
        };

        try
        {
            await _userManager.CreateAsync(newUser,"User");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task SignOutAsync(string userId)
    {
        AppUser user = await _userManager.FindByEmailAsync(userId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("This user not found");
        }
        if(user.Token == null)
        {
            throw new UnauthorizedAccessException("This user alredy log out");
        }
        user.Token = null;
        await _userManager.UpdateAsync(user);
    }


}
