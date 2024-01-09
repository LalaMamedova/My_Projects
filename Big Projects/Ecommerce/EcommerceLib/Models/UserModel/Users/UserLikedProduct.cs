using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcommerceLib.Models.UserModel.Users;

public class UserLikedProduct
{
    public int Id { get; set; } 

    public int ProductId { get; set; }
    public Product Product { get; set; }

    [JsonIgnore]
    public AppUser AppUser { get; set; }
    public string AppUserId { get; set; }
}
