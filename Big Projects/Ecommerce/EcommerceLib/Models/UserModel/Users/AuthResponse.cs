using EcommerceLib.Models.UserModel.JWT;
namespace EcommerceLib.Models.UserModel.Users;

public class AuthResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
    public ICollection<UserLikedProduct> Products { get; set; }
    public MyToken? Token { get; set; }  

}
