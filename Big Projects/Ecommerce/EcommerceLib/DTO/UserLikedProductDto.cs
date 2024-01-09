using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceLib.DTO;

public class UserLikedProductDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string AppUserId { get; set; }
}
