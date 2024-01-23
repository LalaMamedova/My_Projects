namespace EcommerceLib.Models.ProductModel;

public class Characteristic
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string? Description { get; set; }

    public SubCategory SubCategory { get; set; }
    public int SubCategoryId { get; set; }

}
