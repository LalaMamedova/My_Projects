namespace EcommerceLib.Models.ProductModel;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
    public ICollection<BrandAndSubCategory> BrandAndSubCategories { get; set; }
}
