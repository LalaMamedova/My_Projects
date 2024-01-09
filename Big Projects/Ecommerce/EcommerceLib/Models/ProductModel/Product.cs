using EcommerceLib.Models.UserModel.Users;

namespace EcommerceLib.Models.ProductModel;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public int Count { get; set; } = 1;

    public DateTime AddedDate { get; set; } = DateTime.Now;
    public float ProductRating { get=> Reviews!=null? Reviews.Sum(r=>r.Rating/ Reviews.Count):0; }

    public int BrandId { get; set; }
    public Brand? Brand { get; set; }

    public SubCategory? SubCategory { get; set; }
    public int SubCategoryId { get; set; }

    public ICollection<Review>? Reviews { get; set; }
    public ICollection<UserLikedProduct>? UserProducts { get; set; }
    public ICollection<ProductsImg>? ProductsImg { get; set; }
    public ICollection<ProductOriginalImg>? OriginalImgs { get; set; }
    public ICollection<ProductСharacteristic> ProductСharacteristics { get; set; }
    public ICollection<PurchasedProduct> PurchasedProducts { get; set;}
}
