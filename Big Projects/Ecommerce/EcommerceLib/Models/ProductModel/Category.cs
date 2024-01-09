namespace EcommerceLib.Models.ProductModel;

public class Category
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Icon { get; set; }
    public ICollection<SubCategory> SubCategories { get; set; }

}
