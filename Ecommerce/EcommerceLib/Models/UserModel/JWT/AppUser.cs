using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.Users;
using Microsoft.AspNetCore.Identity;
namespace EcommerceLib.Models.UserModel.JWT;
public class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public ICollection<Review>? Reviews { get; set; }
    public ICollection<UserLikedProduct>? FavoriteProducts { get; set; }
    public ICollection<PurchasedProduct>? PurchasedProducts { get; set; }
}
