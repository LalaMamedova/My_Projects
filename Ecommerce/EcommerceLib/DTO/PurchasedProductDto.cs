using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.JWT;

namespace EcommerceLib.DTO;

public class PurchasedProductDto
{
    public int Id { get; set; }
    public string AppUserId { get; set; }
    public int ProductId { get; set; }
    public float TotalSum { get; set; }

}
