using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDbGenericRepository;
using QuizApp.Repository.Generic;
using QuizApp.Services.JwtServices;
using QuizLib.Dto.Request;
using QuizLib.Dto.Response;
using QuizLib.Model;
using QuizLib.Model.User;
using System.IdentityModel.Tokens.Jwt;
using IMongoDbContext = QuizLib.DatabaseContext.IMongoDbContext;

namespace QuizApp.Repository.ModelRepository;

public class UserRepository
{
    private readonly JwtCreate _jwtCreate;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppUserRole> _appUserRole;
    public UserRepository(UserManager<AppUser> userManager,
                          RoleManager<AppUserRole> appUserRole,
                          JwtCreate jwtCreate)
    {

        _jwtCreate = jwtCreate;
        _userManager = userManager;
        _appUserRole = appUserRole;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest reqisterRequest)
    {
        var user = await _userManager.FindByEmailAsync(reqisterRequest.Email);
        
        if(user == null)
        {
            throw new ArgumentNullException("Invalid email");
        }

        var token = await _jwtCreate.CreateJwtToken(user);
       
        if(user.UserQuizes == null)
        {
            user.UserQuizes = new List<Quiz>();
        }

        return new LoginResponse
        {
            Email = user.Email,
            Id = user.Id.ToString(),
            UserQuizes = user.UserQuizes,
            UserToken = new Token()
            {
                AcssesToken = new JwtSecurityTokenHandler().WriteToken(token),
            }
        }; 
      
    }
    public async Task<bool>  AddRoleAsync(string role)
    {
        AppUserRole appUserRole = new() { Name = role };
        await _appUserRole.CreateAsync(appUserRole);
        return true;
  
    }
    public async Task SignupAsync(ReqisterRequest reqisterRequest)
    {   
        if(reqisterRequest.ConfirmPassword != reqisterRequest.Password)
        {
            throw new Exception("Password and confirm password not the same");
        }

        var userExist = _userManager.FindByEmailAsync(reqisterRequest.Email);

        if (userExist.Result != null) {
            throw new Exception($"{reqisterRequest.Email} is alredy userd. Please, try another email") ;
        }

        var newUser = new AppUser()
        {
            Email = reqisterRequest.Email,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            UserName = reqisterRequest.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        try
        {
            await _userManager.CreateAsync(newUser);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
