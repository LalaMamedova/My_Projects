using EcommerceLib.Context;
using EcommerceLib.Models.UserModel.JWT;
using EcommerceLib.Models.UserModel.Users;
using Microsoft.AspNetCore.Identity;

namespace UserApi.Services.JWTServices.Interfaces;

public interface ILoginService
{
    public  Task<string>? GiveRoleAsync(string id);
    public Task<AuthResponse> Login(AuthRequest request);
    public Task Logout(string id);



}
