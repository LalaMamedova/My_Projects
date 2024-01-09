namespace EcommerceLib.Models.ProductModel;


public class SubCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Product>? Products { get; set; }
    public ICollection<BrandAndSubCategory>? BrandAndSubCategories { get; set; }
    public ICollection<Characteristic>? Characteristics { get; set; }

}
