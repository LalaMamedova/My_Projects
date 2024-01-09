using EcommerceLib.Models.UserModel.JWT;

namespace EcommerceLib.Models.ProductModel;

public class Review
{

    public int Id { get; set; }
    public float Rating { get; set; }

    public AppUser AppUser { get; set; }
    public string AppUserId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

}
