namespace EcommerceLib.Models.ProductModel;

public class BrandAndSubCategory
{
    public int Id { get; set; }

    public int BrandId { get; set; }
    public Brand Brand { get; set; }

    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
}
